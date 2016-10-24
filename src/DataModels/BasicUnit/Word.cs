using System;
using System.Linq;
using System.Xml;

namespace BasicUnit
{
	/// <summary>
	///		词
	/// </summary>
	public class Word
	{
		public Word()
		{
		}

		public Word(string name)
		{
			this.Name = name;
		}

		/// <summary>
		///		词形
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		词性
		/// </summary>
		public string Pos { get; set; }

		/// <summary>
		///		起始位置
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		///		结束位置
		/// </summary>
		public int End { get; set; }

		public override string ToString()
		{
			return this.Name;
		}

		public string ToWordString()
		{
			return Name + "/" + Pos;
		}
	}
}
