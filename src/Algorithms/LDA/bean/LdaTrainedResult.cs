using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LDA
{
	[Serializable]
	public class TopicDescription
	{
		public TopicDescription(int topicId, List<TopicWord> topicWordsList, double importance)
		{
			this.TopicId = topicId;
			this.TopicWordsList = topicWordsList;
			this.Importance = importance;
		}

		public int TopicId { get; set; }

		public List<TopicWord> TopicWordsList { get; set; }

		public double Importance { get; set; }

		public void Display()
		{
			Console.WriteLine("Topic Id is {0}, Importance are {1}", TopicId, Importance);
			Console.Write("Description Words are ");
			foreach (TopicWord topicWord in TopicWordsList)
			{
				Console.Write(" ");
				Console.Write(topicWord.Word);
			}
			Console.WriteLine();
		}
	}

	[Serializable]
	public class LdaTrainedResult
	{
		public LdaTrainedResult()
		{
		}

		public LdaTrainedResult(double[,] phi, BiMap<string, int> vectorMap, LdaParameter parameter)
		{
			this.Phi = phi;
			this.VectorMap = vectorMap;
			this.Parameter = parameter;
			this.WordCount = vectorMap.Count();
		}

		public Double[,] Phi { get; set; }

		public BiMap<string, int> VectorMap { get; set; }

		public int WordCount { get; set; }

		public LdaParameter Parameter { get; set; }


		public bool SavePhiToDisk(string path)
		{
			try
			{
				var writer = new StreamWriter(path, false, Encoding.UTF8);
				for (int j = 0; j < Phi.GetLength(1); j++)
				{
					StringBuilder str = new StringBuilder();
					for (int i = 0; i < Phi.GetLength(0); i++)
					{
						str.Append(Phi[i, j] + "\t");
					}
					writer.WriteLine(str);
				}
				writer.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool SaveBimapToDisk(string path)
		{
			try
			{
				int count = this.VectorMap.Count();
				var writer = new StreamWriter(path, false, Encoding.UTF8);
				for (int i = 0; i < count; i++)
				{
					string name = (string)this.VectorMap.GetKey(i);
					writer.WriteLine("{0}\t{1}", i, name);
				}
				writer.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool SaveToDisk(string path)
		{
			try
			{
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, this);
				stream.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}