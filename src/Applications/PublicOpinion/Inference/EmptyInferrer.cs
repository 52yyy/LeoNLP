using PublicOpinion.DataModel;

namespace PublicOpinion.Inference
{
	/// <summary>
	///		空推理器
	/// </summary>
	public class EmptyInferrer : IInferrer
	{
		public PublicOpinionPair Infer(PublicOpinionPair publicOpinionPair)
		{
			return publicOpinionPair;
		}
	}
}