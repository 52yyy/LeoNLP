using BasicUnit;

namespace Nugget
{
	/// <summary>
	///		Nugget抽取组件实体
	/// </summary>
	public class Nugget:IPosition
	{
		public Nugget()
		{
		}

		public Nugget(int start, int end, char leftSign, char rightSign, string content)
		{
			this.Start = start;
			this.End = end;
			this.LeftSign = leftSign;
			this.RightSign = rightSign;
			this.Content = content;
		}

		/// <summary>
		///		Nugget的起始位置
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		///		Nugget的结束位置
		/// </summary>
		public int End
		{
			get
			{
				return Start + Content.Length;
			}
			set
			{
			}
		}

		public char LeftSign { get; set; }

		public char RightSign { get; set; }

		/// <summary>
		///		Nugget内容
		/// </summary>
		public string Content { get; set; }
	}
}