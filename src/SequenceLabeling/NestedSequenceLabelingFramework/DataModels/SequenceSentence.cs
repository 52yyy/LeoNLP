using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���о�
	/// </summary>
	public class SequenceSentence
	{
		private Dictionary<string, SequenceLayer> _layers;

		/// <summary>
		///		����һ���µ����о�
		/// </summary>
		public SequenceSentence()
		{
			this._layers = new Dictionary<string, SequenceLayer>();
		}

		/// <summary>
		///		�����ʾ����Ƕ�׵���Ŀ
		/// </summary>
		public int LayerDepth
		{
			get
			{
				return this._layers == null ? 0 : this._layers.Count;
			}
		}

		/// <summary>
		///		�䳤�ȣ�������ÿһ����������е�Ԫ��
		/// </summary>
		public int Length
		{
			get
			{
				return this._layers.Count == 0 ? 0 : this._layers.First().Value.Length;
			}
		}

		/// <summary>
		///		����һ���µ����в�
		/// </summary>
		/// <param name="layer"></param>
		/// <exception cref="ArgumentException">����Ĳ㲻��Ϊ�ղ�</exception>
		/// <exception cref="ArgumentException">����Ĳ㳤�Ȳ��Ϸ�</exception>
		/// <exception cref="DuplicateNameException">�Ѿ�ӵ�иò�</exception>
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
		///		��ȡ���в�
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
		///		ö�����в�
		/// </summary>
		/// <returns></returns>
		public IEnumerable<SequenceLayer> GetLayerEnumerator()
		{
			return this._layers.Select(pair => pair.Value);
		}
	}
}