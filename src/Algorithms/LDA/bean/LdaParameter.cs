using System;

namespace LDA
{
		[Serializable]
	public class LdaParameter
	{
		public LdaParameter(int iteration, double alpha, double beta, int topicNum)
		{
			this.Iteration = iteration;
			this.Alpha = alpha;
			this.Beta = beta;
			this.Rdm = new Random();
			this.TopicNum = topicNum;
		}

		/// <summary>
		///		迭代次数
		/// </summary>
		public int Iteration { get; set; }

		/// <summary>
		///		平滑系数
		/// </summary>
		public double Alpha { get; set; }

		/// <summary>
		///		平滑系数
		/// </summary>
		public double Beta { get; set; }

		/// <summary>
		///		随机数生成器
		/// </summary>
		public Random Rdm { get; set; }

		/// <summary>
		///		主题数
		/// </summary>
		public int TopicNum { get; set; }
	}
}