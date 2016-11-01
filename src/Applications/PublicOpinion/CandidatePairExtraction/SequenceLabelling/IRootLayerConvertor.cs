using BasicUnit;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		初始层转换抽象类
	/// </summary>
	internal interface IRootLayerConvertor
	{
		/// <summary>
		///		将字符串格式的文本转换成序列标注输入的句子
		/// </summary>
		/// <param name="text">字符串格式的文本</param>
		/// <returns></returns>
		Sentence GetRootSentence(string text);
	}
}