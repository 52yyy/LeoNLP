using System;
using System.IO;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列标注器类
	/// </summary>
	public class SequenceLabeller:IModelInitializable
	{
		private SequenceCalculator _calculator;

		/// <summary>
		///		序列标签描述
		/// </summary>
		public string Desc { get; private set; }

		/// <summary>
		///		序列标签依赖的序列层描述
		/// </summary>
		public string[] LayerDescs { get; private set; }

		/// <summary>
		///		创建一个序列标注器类
		/// </summary>
		/// <param name="desc">序列标签描述</param>
		/// <param name="layerDescs">序列标签依赖的序列层描述，依赖应是若干层的集合</param>
		public SequenceLabeller(string desc, string[] layerDescs)
		{
			this.Desc = desc;
			this.LayerDescs = layerDescs;
		}

		/// <summary>
		///		设置计算器
		/// </summary>
		/// <param name="calculator"></param>
		public void SetCalculator(SequenceCalculator calculator)
		{
			this._calculator = calculator;
		}

		/// <summary>
		///		序列标注类模型初始化
		/// </summary>
		/// <param name="modelPath">模型路径</param>
		/// <returns></returns>
		public bool Initialize(string modelPath)
		{
			string calculatorPath = Path.Combine(modelPath, "model");
			this._calculator.Initialize(calculatorPath);
			return true;
		}

		/// <summary>
		///		将整个序列句输入并为序列打上新的标记
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		public SequenceLayer Label(SequenceSentence sentence)
		{
			if (sentence==null)
			{
				throw new ArgumentNullException("input is null.");
			}
			if (this._calculator == null)
			{
				throw new ArgumentNullException("processor is null.");
			}
			SequenceCellCollection[] cellCollections = this.Distribute(sentence, this.LayerDescs);
			SequenceCellCollection newCellCollection = this._calculator.Calculate(cellCollections);
			SequenceLayer newLayer = new SequenceLayer(Desc, newCellCollection);
			return newLayer;
		}

		private SequenceCellCollection[] Distribute(SequenceSentence sentence, string[] layerDescs)
		{
			SequenceCellCollection[] layerArray = new SequenceCellCollection[layerDescs.Length];
			for (int i = 0; i < layerDescs.Length; i++)
			{
				SequenceLayer layer = sentence.GetLayer(layerDescs[i]);
				if (layer == null)
				{
					throw new ArgumentException(layerDescs[i] + " has not found.");
				}
				layerArray[i] = layer.Cells;
			}
			return layerArray;
		}
	}
}