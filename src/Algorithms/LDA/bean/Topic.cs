using System.Diagnostics;

namespace LDA
{
	/// <summary>
	///		话题
	/// </summary>
	[DebuggerDisplay("VCount = {VCount}")]
	public class Topic
	{
		public Topic(int vCount)
		{
			this.VectorIdArray = new int[vCount];
		}

		/// <summary>
		///		话题总词数
		/// </summary>
		public int VCount { get; set; }

		/// <summary>
		///		话题*词数组，值为话题下词出现次数
		/// </summary>
		public int[] VectorIdArray { get; set; }

		public void AddVector(Vector vector)
		{
			this.VCount++;
			this.VectorIdArray[vector.Id]++;
		}

		public void RemoveVector(Vector vector)
		{
			this.VCount--;
			this.VectorIdArray[vector.Id]--;
		}
	}
}