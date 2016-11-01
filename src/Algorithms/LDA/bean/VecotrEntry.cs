using System;

namespace LDA
{
	/**
 * tmd 排序类
 * 
 * @author ansj
 * 
 */
	public class VecotrEntry : IComparable<VecotrEntry>
	{
		public VecotrEntry(int id, double score)
		{
			this.Id = id;
			this.Score = score;
		}
		public int Id { get; private set; }

		public double Score { get; private set; }

		public int CompareTo(VecotrEntry o)
		{
			if (this.Score > o.Score)
			{
				return -1;
			}
			return 1;
		}
	}
}