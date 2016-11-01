namespace Nugget
{
	public class PuncPairInfo
	{
		public PuncPairInfo(int pos, char ch, char rCh, int type)
		{
			this.Pos = pos;
			this.Word = ch;
			this.rWord = rCh;
			this.Type = type;
		}

		public int Pos { get; set; }

		public char Word { get; set; }

		public char rWord { get; set; }

		public int Type { get; set; } //0:左侧括号，1：右侧括号
	}
}