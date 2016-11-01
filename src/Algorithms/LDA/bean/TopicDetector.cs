using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LDA
{
	[Serializable]
	public class TopicDetector
	{
		public LdaTrainedResult Model { get; set; }

		public TopicDetector(string modelPath)
		{
			try
			{
				IFormatter formatter = new BinaryFormatter();
				Stream destream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
				this.Model = (LdaTrainedResult)formatter.Deserialize(destream);
				destream.Close();
				this.Docs = new List<Doc>();
				this.BlackList = new List<string>();
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public TopicDetector(LdaTrainedResult model)
		{
			this.Model = model;
			this.Docs = new List<Doc>();
			this.BlackList = new List<string>();
		}

		public TopicDetector()
		{
			this.Docs = new List<Doc>();
		}

		public int DocCount { get; set; }

		public List<Doc> Docs { get; set; }

		public List<TopicDescription> TopicWordsList { get; set; }

		public List<string> BlackList { get; set; } 

		public void DescribeTopic(int topNum)
		{
			this.TopicWordsList = new List<TopicDescription>();
			for (int i = 0; i < this.Model.Parameter.TopicNum; i++)
			{
				var myQ = new Queue();
				var list = new List<VecotrEntry>();
				var scores = new List<double>();
				for (int k = 0; k < this.Model.Phi.GetLength(1); k++)
				{
					scores.Add(this.Model.Phi[i, k]);
				}
				for (int j = 0; j < this.Model.WordCount; j++)
				{
					var ve = new VecotrEntry(j, scores[j]);
					list.Add(ve);
				}
				list = list.OrderByDescending(x => x.Score).ToList();
				foreach (VecotrEntry t in list)
				{
					myQ.Enqueue(t);
				}

				var tmpWords = new List<TopicWord>();
				for (int j = 0; j < topNum; j++)
				{
					if (myQ.Count == 0)
					{
						break;
					}
					var pollFirst = (VecotrEntry)myQ.Dequeue();

					string keyword = this.Model.VectorMap.GetKey(pollFirst.Id).ToString();
					double wordvalue = pollFirst.Score;

					tmpWords.Add(new TopicWord(keyword, wordvalue));
				}
				this.TopicWordsList.Add(new TopicDescription(i, tmpWords, 0));
			}
		}


		/// <summary>
		///		使用了最小二乘方法过滤掉高频词，效果较好
		/// </summary>
		/// <param name="topNum"></param>
		public void DescribeTopicByLeastSquareMethod(int topNum)
		{
			this.TopicWordsList = new List<TopicDescription>();
			double[,] phi = new double[this.Model.Phi.GetLength(0), this.Model.Phi.GetLength(1)];
			double[] vectorSums = new double[this.Model.Phi.GetLength(1)];
			for (int i = 0; i < this.Model.Phi.GetLength(1); i++)
			{
				for (int j = 0; j < this.Model.Phi.GetLength(0); j++)
				{
					vectorSums[i] += this.Model.Phi[j, i];
				}
			}

			for (int i = 0; i < phi.GetLength(1); i++)
			{
				for (int j = 0; j < phi.GetLength(0); j++)
				{
					phi[j, i] = this.Model.Phi[j, i] / vectorSums[i];
				}
			}

			double[] topicSums = new double[phi.GetLength(0)];
			for (int j = 0; j < phi.GetLength(0); j++)
			{
				for (int i = 0; i < phi.GetLength(1); i++)
				{
					topicSums[j] += phi[j, i];
				}
			}

			List<int> blackList = new List<int>();
			for (int i = 0; i < phi.GetLength(1); i++)
			{
				double sum = 0;
				for (int j = 0; j < phi.GetLength(0); j++)
				{
					sum += Math.Pow(x: phi[j, i] - 1.0 / topNum, y: 2);
				}
				if (sum < (topNum - 1.0) / (topNum * 5))
				{
					blackList.Add(i);
					this.BlackList.Add(this.Model.VectorMap.GetKey(i).ToString());
				}
			}

			for (int i = 0; i < this.Model.Parameter.TopicNum; i++)
			{
				var myQ = new Queue();
				var list = new List<VecotrEntry>();
				var scores = new List<double>();
				for (int k = 0; k < this.Model.Phi.GetLength(1); k++)
				{
					scores.Add(this.Model.Phi[i, k]);
				}
				for (int j = 0; j < this.Model.WordCount; j++)
				{
					if (blackList.Contains(j))
					{
						continue;
					}
					var ve = new VecotrEntry(j, scores[j]);
					list.Add(ve);
				}
				list = list.OrderByDescending(x => x.Score).ToList();
				foreach (VecotrEntry t in list)
				{
					myQ.Enqueue(t);
				}

				var tmpWords = new List<TopicWord>();
				for (int j = 0; j < topNum; j++)
				{
					if (myQ.Count == 0)
					{
						break;
					}
					var pollFirst = (VecotrEntry)myQ.Dequeue();

					string keyword = this.Model.VectorMap.GetKey(pollFirst.Id).ToString();
					double wordvalue = pollFirst.Score;

					tmpWords.Add(new TopicWord(keyword, wordvalue));
				}
				this.TopicWordsList.Add(new TopicDescription(i, tmpWords, topicSums[i]));
			}
		}


		public List<TopicDescription> GetTopicWords(int n)
		{
			if (this.TopicWordsList == null || this.TopicWordsList.Count == 0)
			{
				this.DescribeTopicByLeastSquareMethod(n);
			}
			return this.TopicWordsList;
		}

		public void AddDoc(string name, ICollection<object> words)
		{
			var doc = new Doc(name, this.Model.Parameter.TopicNum);
			this.DocCount++;
			foreach (object word in words)
			{
				int id;
				if (!this.Model.VectorMap.ContainsKey(word.ToString()))
				{
					continue;
				}
				id = this.Model.VectorMap.GetValue(word.ToString());
				int topicId = this.Model.Parameter.Rdm.Next(0, this.Model.Parameter.TopicNum);

				// 文档增加向量
				doc.AddVector(new Vector(id, topicId));
			}
			this.Docs.Add(doc);
		}

		public void LoadDocByFile(string path, Encoding encoding)
		{
			Utils Utils = new Utils();
			if (String.IsNullOrEmpty(path))
			{
				throw new ArgumentException();
			}
			var sr = new StreamReader(new FileStream(path, FileMode.Open), encoding);
			String temp;
			var i = 0;
			string doc = "";
			while ((temp = sr.ReadLine()) != null)
			{
				if (string.IsNullOrEmpty(doc))
				{
					doc = temp;
				}
				var words = Utils.WordFilter(temp.Split(Utils.Spliter, StringSplitOptions.RemoveEmptyEntries));
				this.AddDoc(Convert.ToString(++i), words);
			}
			sr.Close();
		}

		public void Predict()
		{
			// 迭代收敛
			for (int i = 0; i < this.Model.Parameter.Iteration; i++)
			{
				foreach (Doc doc in this.Docs)
				{
					foreach (Vector vector in doc.Vectors)
					{
						this.SampleTopic(doc, vector);
					}
				}
			}
		}

		private void SampleTopic(Doc doc, Vector vector)
		{
			doc.RemoveVector(vector);

			var p = new double[this.Model.Parameter.TopicNum];

			for (int k = 0; k < this.Model.Parameter.TopicNum; k++)
			{
				p[k] = this.Model.Phi[k, vector.Id] * 
					(doc.TopicArray[k] + this.Model.Parameter.Alpha) / (doc.Vectors.Count - 1 + this.Model.Parameter.TopicNum * this.Model.Parameter.Alpha);
			}

			// 累计使得p[k]是前面所有topic可能性的和
			for (int k = 1; k < this.Model.Parameter.TopicNum; k++)
			{
				p[k] += p[k - 1];
			}

			double u = this.Model.Parameter.Rdm.NextDouble() * p[this.Model.Parameter.TopicNum - 1];

			int newTopic;
			for (newTopic = 0; newTopic < this.Model.Parameter.TopicNum; newTopic++)
			{
				if (u < p[newTopic])
				{
					break;
				}
			}

			vector.TopicId = newTopic;

			doc.UpdateVecotr(vector);
		}
	}
}