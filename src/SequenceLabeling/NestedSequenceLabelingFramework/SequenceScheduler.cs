using System.Collections.Generic;

using BasicUnit;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列标注调度类
	/// </summary>
	public class SequenceScheduler
	{
		private List<SequenceLabeller> _layers;

		private ISequenceCombiner _combiner;

		public SequenceScheduler()
		{
			this._layers = new List<SequenceLabeller>();
			this._combiner = new SequenceCombiner();
		}

		public SequenceSentence Label(Sentence sentence)
		{
			SequenceSentence tmpSentence = this.ConvertToSequenceSentence(sentence);
			foreach (SequenceLabeller schedulerUnit in this._layers)
			{
				SequenceLayer newLayer = schedulerUnit.Label(tmpSentence);
				tmpSentence = _combiner.Combine(tmpSentence, newLayer);
			}
			return tmpSentence;
		}

		private SequenceSentence ConvertToSequenceSentence(Sentence sentence)
		{
			SequenceCellCollection nameCollection = new SequenceCellCollection(sentence.Words.Count);
			SequenceCellCollection posCollection = new SequenceCellCollection(sentence.Words.Count);
			for (int i = 0; i < sentence.Words.Count; i++)
			{
				Word token = sentence.Words[i];
				nameCollection.Cells[i] = new SequenceCell(token.Name);
				posCollection.Cells[i] = new SequenceCell(token.Pos);
			}
			SequenceLayer wordLayer = new SequenceLayer("word", nameCollection);
			SequenceLayer posLayer = new SequenceLayer("pos", posCollection);
			SequenceSentence sequenceSentence = new SequenceSentence();
			sequenceSentence.AddLayer(wordLayer);
			sequenceSentence.AddLayer(posLayer);
			return sequenceSentence;
		}


		/// <summary>
		///		设置合并器
		/// </summary>
		/// <param name="combiner"></param>
		public void SetCombiner(ISequenceCombiner combiner)
		{
			this._combiner = combiner;
		}

		/// <summary>
		///		添加处理层
		/// </summary>
		/// <param name="unit"></param>
		public void AddLayer(SequenceLabeller layer)
		{
			this._layers.Add(layer);
		}
	}
}