using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列句
	/// </summary>
	public class SequenceSentence
	{
		private Dictionary<string, SequenceLayer> _layers;

		/// <summary>
		///		构造一个新的序列句
		/// </summary>
		public SequenceSentence()
		{
			this._layers = new Dictionary<string, SequenceLayer>();
		}

		/// <summary>
		///		层深，表示层中嵌套的数目
		/// </summary>
		public int LayerDepth
		{
			get
			{
				return this._layers == null ? 0 : this._layers.Count;
			}
		}

		/// <summary>
		///		句长度，即句中每一层包含的序列单元数
		/// </summary>
		public int Length
		{
			get
			{
				return this._layers.Count == 0 ? 0 : this._layers.First().Value.Length;
			}
		}

		/// <summary>
		///		增加一个新的序列层
		/// </summary>
		/// <param name="layer"></param>
		/// <exception cref="ArgumentException">输入的层不能为空层</exception>
		/// <exception cref="ArgumentException">输入的层长度不合法</exception>
		/// <exception cref="DuplicateNameException">已经拥有该层</exception>
		public void AddLayer(SequenceLayer layer)
		{
			if (layer.Length == 0)
			{
				throw new ArgumentException("empty layer.");
			}
			if (this.Length != 0 && this.Length != layer.Length)
			{
				throw new ArgumentException("layer length invalid.");
			}
			if (this._layers.ContainsKey(layer.Desc))
			{
				throw new DuplicateNameException("layer desc is duplicate.");
			}
			this._layers.Add(layer.Desc, layer);
		}

		/// <summary>
		///		抽取序列层
		/// </summary>
		/// <param name="layerDesc"></param>
		/// <returns></returns>
		public SequenceLayer GetLayer(string layerDesc)
		{
			if (_layers.ContainsKey(layerDesc))
			{
				return _layers[layerDesc];
			}
			return null;
		}

		/// <summary>
		///		枚举序列层
		/// </summary>
		/// <returns></returns>
		public IEnumerable<SequenceLayer> GetLayerEnumerator()
		{
			return this._layers.Select(pair => pair.Value);
		}
	}
}