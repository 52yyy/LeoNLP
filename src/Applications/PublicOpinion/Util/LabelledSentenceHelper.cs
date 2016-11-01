using NestedSequenceLabelingFramework;

using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.Util
{
	internal static class LabelledSentenceHelper
	{
		/// <summary>
		///		将序列标注格式的句子转换成标记单元的句子
		/// </summary>
		/// <param name="sequenceSentence"></param>
		/// <returns></returns>
		public static LabelledSentence ConvertToLabelledSentence(SequenceSentence sequenceSentence)
		{
			LabelledWord[] words = new LabelledWord[sequenceSentence.Length];
			SequenceLayer wordLayer = sequenceSentence.GetLayer("word");
			SequenceLayer posLayer = sequenceSentence.GetLayer("pos");
			SequenceLayer neLayer = sequenceSentence.GetLayer("ne");
			SequenceLayer chunkLayer = sequenceSentence.GetLayer("chunk");

			int index = 0;
			for (int i = 0; i < words.Length; i++)
			{
				string name = wordLayer.Cells.Cells[i].Content;
				string postag = posLayer.Cells.Cells[i].Content;
				string netag = neLayer.Cells.Cells[i].Content;
				string chunktag = chunkLayer.Cells.Cells[i].Content;
				words[i] = new LabelledWord(name, postag, netag, chunktag, index, index + name.Length);
				index += name.Length;
			}
			return new LabelledSentence(words);
		}
	}
}