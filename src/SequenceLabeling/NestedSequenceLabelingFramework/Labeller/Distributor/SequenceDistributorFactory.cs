using System.Collections.Generic;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		序列分派器工厂类
	/// </summary>
	public class SequenceDistributorFactory
	{
		private static Dictionary<string, ISequenceDistributor> _distributors;

		static SequenceDistributorFactory() 
		{
			_distributors = new Dictionary<string, ISequenceDistributor>();
			_distributors.Add("default", new SequenceDistributor());
		}

		/// <summary>
		///		创建一个默认的序列分派器
		/// </summary>
		/// <returns></returns>
		public static ISequenceDistributor CreateDefaultDistributor()
		{
			return _distributors["default"];
		}
	}
}