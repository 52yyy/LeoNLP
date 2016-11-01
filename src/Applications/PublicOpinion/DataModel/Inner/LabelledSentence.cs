using System.Text;

namespace PublicOpinion.DataModel.Inner
{
	/// <summary>
	///		序列标记的句子
	/// </summary>
	internal class LabelledSentence
	{
		public LabelledWord[] Words { get; set; }

		public LabelledSentence(LabelledWord[] words)
		{
			this.Words = words;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (LabelledWord word in this.Words)
			{
				sb.Append(word);
				sb.Append("\n");
			}
			return sb.ToString();
		}
	}
}