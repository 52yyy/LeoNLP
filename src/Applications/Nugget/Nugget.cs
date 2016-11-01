using BasicUnit;

namespace Nugget
{
	/// <summary>
	///		Nugget��ȡ���ʵ��
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
		///		Nugget����ʼλ��
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		///		Nugget�Ľ���λ��
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
		///		Nugget����
		/// </summary>
		public string Content { get; set; }
	}
}