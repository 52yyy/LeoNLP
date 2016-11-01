using System;
using System.Collections.Generic;
using System.ComponentModel;

using PublicOpinion.DataModel.Inner;
using PublicOpinion.Util;

namespace PublicOpinion.Tools
{
	internal class ManualPairFeatureConvertor
	{
		private ManualLabelledSentenceConvertor _labelledSentenceConvertor;

		public ManualPairFeatureConvertor()
		{
			this._labelledSentenceConvertor = new ManualLabelledSentenceConvertor();
			this._labelledSentenceConvertor.SetLayerConvertor(new NeLayerConvertor());
			this._labelledSentenceConvertor.SetLayerConvertor(new ChunkLayerConvertor());
			
		}

		public IEnumerable<ChunkSentence> Convert(PomModel pom)
		{
			LabelledSentence labelledSentence = this._labelledSentenceConvertor.Convert(pom);
			IEnumerable<ChunkSentence> chunks = ChunkHelper.ChunkSplit(labelledSentence);
			return chunks;
		}

	}
}