using System;
using System.Linq;
using System.Text;

using PublicOpinion.DataModel;

namespace PublicOpinion
{
	/// <summary>
	///		公众意见抽取接口
	/// </summary>
    public interface IPublicOpinionExtractor
	{
		/// <summary>
		///		执行抽取
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		PublicOpinionPairCollection Extract(string text);

		/// <summary>
		///		执行抽取
		/// </summary>
		/// <param name="pomSentence"></param>
		/// <returns></returns>
		PublicOpinionPairCollection Extract(PomSentence pomSentence);
	}
}
