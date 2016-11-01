using System;
using System.Collections.Generic;

using BasicUnit;

using NestedSequenceLabelingFramework;

using PublicOpinion.CandidatePairExtraction;
using PublicOpinion.DataModel.Inner;
using PublicOpinion.Util;

namespace PublicOpinion.Tools
{
	internal class ManualLabelledSentenceConvertor
	{
		private IRootLayerConvertor _rootLayerConvertor = new SmallSegwordRootLayerConvertor();


		private List<LayerConvertor> _layerConvertors = new List<LayerConvertor>();

		public void SetLayerConvertor(LayerConvertor convertor)
		{
			this._layerConvertors.Add(convertor);
		}

		public LabelledSentence Convert(PomModel pom)
		{
			string content = pom.Content.Split('\t')[1];
			content = content.Replace(" ", "&");
			Sentence sentence = _rootLayerConvertor.GetRootSentence(content);
			List<SequenceLayer> layers = new List<SequenceLayer>();
			foreach (LayerConvertor layerConvertor in _layerConvertors)
			{
				SequenceLayer result = layerConvertor.GetSequenceLayer(sentence, pom);
				layers.Add(result);
			}
			SequenceSentence sequenceSentence = this.CombineToSequenceSentence(sentence, layers);
			return LabelledSentenceHelper.ConvertToLabelledSentence(sequenceSentence);
		}

		private SequenceSentence CombineToSequenceSentence(Sentence sen1, List<SequenceLayer> layers)
		{
			if (sen1 == null || layers == null)
			{
				throw new ArgumentNullException();
			}
			int length = sen1.Words.Count;

			foreach (SequenceLayer layer in layers)
			{
				if (length != layer.Length)
				{
					throw new ArgumentException();
				}
			}

			SequenceLayer wordLayer = new SequenceLayer("word", new SequenceCellCollection(length));
			SequenceLayer posLayer = new SequenceLayer("pos", new SequenceCellCollection(length));

			for (int i = 0; i < length; i++)
			{
				wordLayer.Cells.Cells[i] = new SequenceCell(sen1.Words[i].Name);
				posLayer.Cells.Cells[i] = new SequenceCell(sen1.Words[i].Pos);
			}

			SequenceSentence sentence = new SequenceSentence();
			sentence.AddLayer(wordLayer);
			sentence.AddLayer(posLayer);

			foreach (SequenceLayer layer in layers)
			{
				sentence.AddLayer(layer);
			}

			return sentence;
		}
	}
}