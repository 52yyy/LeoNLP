using System.Collections.Generic;

using PublicOpinion.DataModel;

namespace PublicOpinion.Normalization
{
	/// <summary>
	///		POM格式数据正规化接口
	/// </summary>
	public interface IPomNormalizable
	{
		/// <summary>
		///		正规化方法，将从外部获取的原始文本转化成POM格式的数据流
		/// </summary>
		/// <param name="rawText">原始文本</param>
		/// <returns></returns>
		IEnumerable<PomSentence> Normalize(string rawText);
	}
}
