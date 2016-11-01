using BasicUnit;

using NestedSequenceLabelingFramework;

namespace PublicOpinion.Tools
{
	public abstract class LayerConvertor
	{
		protected string _layerDesc;

		protected LayerConvertor(string desc)
		{
			this._layerDesc = desc;
		}

		public abstract Sentence GetLayer(Sentence sentence, PomModel pom);

		public SequenceLayer GetSequenceLayer(Sentence sentence, PomModel pom)
		{
			Sentence newSentence = this.GetLayer(sentence, pom);
			SequenceLayer layer = new SequenceLayer(_layerDesc,new SequenceCellCollection(newSentence.Words.Count) );
			for (int i = 0; i < newSentence.Words.Count; i++)
			{
				layer.Cells.Cells[i] = new SequenceCell(newSentence.Words[i].Pos);
			}
			return layer;
		}
	}
}