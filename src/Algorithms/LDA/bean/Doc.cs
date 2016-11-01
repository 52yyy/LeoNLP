using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LDA
{
	[DebuggerDisplay("_name = {_name}")]
	public class Doc
	{
		private readonly string _name;

		public Doc(string name, int topicNum)
		{
			this._name = name;
			this.TopicArray = new int[topicNum];
			this.Vectors = new List<Vector>();
		}

		/// <summary>
		///		Doc-Topic Matrix
		/// </summary>
		public int[] TopicArray { get; set; }

		/// <summary>
		///		Doc-Vector Matrix
		/// </summary>
		public List<Vector> Vectors { get; set; }

		public void AddVector(Vector vector)
		{
			this.Vectors.Add(vector);
			this.TopicArray[vector.TopicId]++;
		}

		public string GetName()
		{
			return this._name;
		}

		public void RemoveVector(Vector vector)
		{
			this.TopicArray[vector.TopicId]--;
		}

		public void UpdateVecotr(Vector vector)
		{
			this.TopicArray[vector.TopicId]++;
		}
	}
}