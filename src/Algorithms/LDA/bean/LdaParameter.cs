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
		///		��������
		/// </summary>
		public int Iteration { get; set; }

		/// <summary>
		///		ƽ��ϵ��
		/// </summary>
		public double Alpha { get; set; }

		/// <summary>
		///		ƽ��ϵ��
		/// </summary>
		public double Beta { get; set; }

		/// <summary>
		///		�����������
		/// </summary>
		public Random Rdm { get; set; }

		/// <summary>
		///		������
		/// </summary>
		public int TopicNum { get; set; }
	}
}