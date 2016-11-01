using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PublicOpinion.DataModel;

namespace PublicOpinion.Inference
{
	/// <summary>
	///		推理器
	/// </summary>
	internal interface IInferrer
	{
		/// <summary>
		///		推断
		/// </summary>
		/// <param name="publicOpinionPair"></param>
		/// <returns></returns>
		PublicOpinionPair Infer(PublicOpinionPair publicOpinionPair);
	}
}
