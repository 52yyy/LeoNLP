using System.Collections.Generic;

namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		���з�����������
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
		///		����һ��Ĭ�ϵ����з�����
		/// </summary>
		/// <returns></returns>
		public static ISequenceDistributor CreateDefaultDistributor()
		{
			return _distributors["default"];
		}
	}
}