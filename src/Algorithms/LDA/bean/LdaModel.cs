using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LDA
{
	public abstract class LdaModel
	{
		public List<List<string>> FileList;

		public List<List<string>> TopicWordsList;

		protected double Alpha = 0.5;

		//开始的保存的迭代次数,默认不保存
		protected int BeginSaveIters = int.MaxValue;

		protected double Beta = 0.1;

		// WordCount:词数 DocCount:文档数
		protected int DocCount;

		//文档-词矩阵
		protected List<Doc> Docs = new List<Doc>();

		protected int Iteration = 100;

		protected Random Rdm = new Random();

		protected int SaveStep = int.MaxValue;

		protected int TopicNum;

		/**
         * 主题-词矩阵
         */
		protected Topic[] Topics = null;

		//词和id双向map
		protected BiMap<string, int> VectorMap = new BiMap<string, int>();

		protected int WordCount;

		private const string Lines = "\n";

		/**
         * @param alpha
         * @param beta
         * @param iteration迭代次数
         * @param saveStep每多少步保存一次
         * @param beginSaveIters开始的保存的迭代次数
         */
		protected LdaModel(int topicNum, double alpha, double beta, int iteration, int saveStep, int beginSaveIters)
		{
			this.TopicNum = topicNum;
			this.Alpha = alpha;
			this.Beta = beta;
			this.Iteration = iteration;
			this.SaveStep = saveStep;
			this.BeginSaveIters = beginSaveIters;
			this.TopicWordsList = new List<List<string>>();
			this.FileList = new List<List<string>>();
			this.model = new LdaTrainedResult();
		}

		public LdaTrainedResult model { get; set; }

		public abstract void SampleTopic(Doc doc, Vector vector);

		//更新估计参数
		public abstract void UpdateEstimatedParameters(double[,] phi, double[,] theta);

		/**
         * 增加文章
         * @param name 
         * @param words
         */
		public void AddDoc(string name, ICollection<object> words)
		{
			var doc = new Doc(name, this.TopicNum);
			this.DocCount++;
			foreach (object word in words)
			{
				int id;
				if (!this.VectorMap.ContainsKey(word.ToString()))
				{
					id = this.WordCount;
					this.VectorMap.Add(word.ToString(), this.WordCount);
					this.WordCount++;
				}
				else
				{
					id = this.VectorMap.GetValue(word.ToString());
				}
				int topicId = this.Rdm.Next(0, this.TopicNum);

				// 文档增加向量
				doc.AddVector(new Vector(id, topicId));
			}
			this.Docs.Add(doc);
		}

		/// <summary>
		///     参数估计
		/// </summary>
		public void ParameterEstimation()
		{
			var phi = new double[this.TopicNum, this.WordCount];
			var theta = new double[this.DocCount, this.TopicNum];
			this.UpdateEstimatedParameters(phi, theta);
			this.model = new LdaTrainedResult(
				phi,
				this.VectorMap,
				new LdaParameter(this.Iteration, this.Alpha, this.Beta, this.TopicNum));
		}

		public LdaTrainedResult Train()
		{
			// 填满topic-vector矩阵
			this.FullTopicVector();
			Console.WriteLine("insert model ok! ");

			// 迭代收敛
			for (int i = 0; i < this.Iteration; i++)
			{
				Console.WriteLine("iteration:\t" + i);
				foreach (Doc doc in this.Docs)
				{
					foreach (Vector vector in doc.Vectors)
					{
						// 对观测数据采样
						this.SampleTopic(doc, vector);
					}
				}
			}

			// 收敛之后的参数估计
			this.ParameterEstimation();
			return this.model;
		}

		/// <summary>
		///     开始训练
		///     @throws IOException
		/// </summary>
		/// <param name="modelPath">输出模型到磁盘的路径</param>
		/// <param name="encoding">UTF-8</param>
		public void TrainAndSave(string modelPath, Encoding encoding)
		{
			this.FullTopicVector();
			Console.WriteLine("insert model ok! ");

			// 迭代收敛
			for (int i = 0; i < this.Iteration; i++)
			{
				if ((i >= this.BeginSaveIters) && (((i - this.BeginSaveIters) % this.SaveStep) == 0))
				{
					this.SaveModel(i + "", modelPath, encoding);
				}
				Console.WriteLine("iteration:\t" + i);
				foreach (Doc doc in this.Docs)
				{
					foreach (Vector vector in doc.Vectors)
					{
						this.SampleTopic(doc, vector);
					}
				}
			}
			this.SaveModel("result", modelPath, Encoding.Default);
			this.ParameterEstimation();
		}

		private void FullTopicVector()
		{
			this.Topics = new Topic[this.TopicNum];
			for (int i = 0; i < this.Topics.Length; i++)
			{
				this.Topics[i] = new Topic(this.WordCount);
			}

			foreach (Doc doc in this.Docs)
			{
				foreach (Vector vector in doc.Vectors)
				{
					this.Topics[vector.TopicId].AddVector(vector);
				}
			}
		}

		private void SaveModel(string iters, string modelPath, Encoding encoding)
		{
			var phi = new double[this.TopicNum, this.WordCount];

			var theta = new double[this.DocCount, this.TopicNum];

			this.UpdateEstimatedParameters(phi, theta);

			this.SaveModel(iters, phi, theta, modelPath, encoding);
		}

		private void SaveModel(string iters, double[,] phi, double[,] theta, string modelPath, Encoding encoding)
		{
			string modelName = "lda_" + iters;
			if (!Directory.Exists(modelPath))
			{
				Directory.CreateDirectory(modelPath);
			}

			/**
             * 配置信息
             */
			var sb = new StringBuilder();
			sb.Append("alpha = " + this.Alpha + Lines);
			sb.Append("topicNum = " + this.TopicNum + Lines);
			sb.Append("docNum = " + this.DocCount + Lines);
			sb.Append("termNum = " + this.WordCount + Lines);
			sb.Append("iterations = " + this.Iteration + Lines);
			sb.Append("saveStep = " + this.SaveStep + Lines);
			sb.Append("beginSaveIters = " + this.BeginSaveIters);

			string modelDir = modelPath + "/";
			var sw = new StreamWriter(modelDir + modelName + ".params", false, encoding);
			sw.WriteLine(sb.ToString());
			sw.Close();

			//phi K*V
			var writer = new StreamWriter(modelDir + modelName + ".phi", false, encoding);
			for (int j = 0; j < phi.GetLength(1); j++)
			{
				StringBuilder str = new StringBuilder();
				for (int i = 0; i < this.TopicNum; i++)
				{
					str.Append(phi[i, j] + "\t");
				}
				writer.WriteLine(str);
			}
			writer.Close();

			// theta M*K
			writer = new StreamWriter(modelDir + modelName + ".theta", false, Encoding.Default);
			for (int i = 0; i < this.DocCount; i++)
			{
				writer.Write(this.Docs[i].GetName() + "\t");
				var thetai = new double[theta.GetLength(1)];
				for (int j = 0; j < theta.GetLength(1); j++)
				{
					thetai[j] = theta[i, j];
				}
				writer.WriteLine(string.Join("\t", thetai));
			}
			writer.Close();

			// tassign
			writer = new StreamWriter(modelDir + modelName + ".tassign", false, Encoding.Default);
			for (int m = 0; m < this.DocCount; m++)
			{
				Doc doc = this.Docs[m];
				writer.Write(doc.GetName() + "\t");
				foreach (Vector vector in doc.Vectors)
				{
					writer.Write(vector.Id + ":" + vector.TopicId + "\t");
				}
				writer.WriteLine();
			}
			writer.Close();

			// twords phi[,] K*V
			writer = new StreamWriter(modelDir + modelName + ".twords", false, Encoding.Default);
			const int topNum = 20;
			for (int i = 0; i < this.TopicNum; i++)
			{
				writer.WriteLine("topic{0}\t:", i);
				var myQ = new Queue();
				var list = new List<VecotrEntry>();
				var scores = new List<double>();
				for (int k = 0; k < phi.GetLength(1); k++)
				{
					//bug
					scores.Add(phi[i, k]);
				}
				for (int j = 0; j < this.WordCount; j++)
				{
					Console.WriteLine("j:{0}\t" + "scores[j]:{1}", j, scores[j]);
					var ve = new VecotrEntry(j, scores[j]);
					list.Add(ve);
				}
				list = list.OrderByDescending(x => x.Score).ToList();
				foreach (VecotrEntry t in list)
				{
					myQ.Enqueue(t);
				}

				var tmpWords = new List<string>();
				for (int j = 0; j < topNum; j++)
				{
					if (myQ.Count == 0)
					{
						break;
					}
					var pollFirst = (VecotrEntry)myQ.Dequeue();

					//Console.WriteLine("pollFirst.Id:" + pollFirst.Id);
					string keyword = this.VectorMap.GetKey(pollFirst.Id).ToString();
					double wordvalue = pollFirst.Score;

					//writer.WriteLine("\t" + VectorMap.GetKey(pollFirst.Id) + " " + pollFirst.Score);
					writer.WriteLine("\t{0} {1}", keyword, wordvalue);
					tmpWords.Add(keyword);
				}
				this.TopicWordsList.Add(tmpWords);
				writer.WriteLine();
			}
			writer.Close();
		}

		//填充topic array矩阵
	}
}