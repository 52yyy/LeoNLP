using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicUnit.DoubleArrayTrie
{
	public abstract class DATrieNode
	{
		protected DATrieNode(int index)
		{
			this.Index = index;
			this.Base = 0;
			this.Check = 0;
			this.Status = 1;
		}

		public int Index { get; set; }

		public int Base { get; set; }

		public int Check { get; set; }

		public byte Status { get; set; }

		public string Word { get; set; }

		public object Value { get; set; }

		public override string ToString()
		{
			return this.ToText();
		}

		/// <summary>
		///		从词典中加载如果又特殊需求可重写此构造方法
		/// </summary>
		/// <param name="split"></param>
		public abstract void Init(string key, object value);

		/// <summary>
		///		从生成的词典中加载。应该和ToText方法对应
		/// </summary>
		/// <param name="split"></param>
		public abstract void InitValue(string[] split);

		/// <summary>
		///		以文本格式序列化的显示
		/// </summary>
		/// <returns></returns>
		public abstract string ToText();

		/// <summary>
		///		Value值转成String
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected abstract string Object2String(object obj);

		/// <summary>
		///		string转成Value值
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected abstract object String2Object(string str);
	}

	public class DefaultDATrieNode : DATrieNode
	{
		public DefaultDATrieNode(int index)
			: base(index)
		{
		}

		public DefaultDATrieNode(string name, object value)
			: base(0)
		{
			this.Init(name, value);
		}

		/// <summary>
		///		从词典中加载如果又特殊需求可重写此构造方法
		/// </summary>
		/// <param name="split"></param>
		public override sealed void Init(string key, object value)
		{
			this.Word = key;
			this.Value = value;
		}

		/// <summary>
		///		从生成的词典中加载，应该和ToText方法对应
		/// </summary>
		/// <param name="split"></param>
		public override void InitValue(string[] split)
		{
			Index = int.Parse(split[0]);
			Base = int.Parse(split[1]);
			Check = int.Parse(split[2]);
			Status = byte.Parse(split[3]);
			Word = split[4];
			if (split.Length>5)
			{
				Value = String2Object(split[5]);
			}
		}

		/// <summary>
		///		以文本格式序列化的显示
		/// </summary>
		/// <returns></returns>
		public override string ToText()
		{
			if (Value == null)
			{
				return Index + "\t" + Base + "\t" + Check + "\t" + Status + "\t" + Word;				
			}
			else
			{
				return Index + "\t" + Base + "\t" + Check + "\t" + Status + "\t" + Word + "\t" + Object2String(Value);
			}
		}

		/// <summary>
		///		Value值转成String
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected override string Object2String(object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			return obj.ToString();
		}

		/// <summary>
		///		string转成Value值
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected override object String2Object(string str)
		{
			return str;
		}
	}
}
