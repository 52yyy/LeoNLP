using System;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���з������ְ࣬���Ǽ������ɵı�ע���Ƿ�ȫ�����ڣ�������ط�����ز�
	/// </summary>
	public class SequenceDistributor : ISequenceDistributor, IModelInitializable
	{
		public SequenceCellCollection[] Distribute(SequenceSentence sentence, string[] layerDescs)
		{
			SequenceCellCollection[] layerArray = new SequenceCellCollection[layerDescs.Length];
			for (int i = 0; i < layerDescs.Length; i++)
			{
				SequenceLayer layer = sentence.GetLayer(layerDescs[i]);
				if (layer==null)
				{
					throw new ArgumentException(layerDescs[i] + " has not found.");
				}
				layerArray[i] = layer.Cells;
			}
			return layerArray;
		}

		public bool Initialize(string modelPath)
		{
			throw new NotImplementedException();
		}
	}
}