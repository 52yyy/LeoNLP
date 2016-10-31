using System;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列分派器类，职责是检查待分派的标注层是否全都存在，并有序地返回相关层
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