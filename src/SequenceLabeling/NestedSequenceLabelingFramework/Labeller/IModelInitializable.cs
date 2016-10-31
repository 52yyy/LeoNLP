namespace NestedSequenceLabelingFramework
{
	/// <summary>
	///		模型初始化接口
	/// </summary>
	public interface IModelInitializable
	{
		/// <summary>
		///		模型初始化
		/// </summary>
		/// <param name="modelPath"></param>
		/// <returns></returns>
		bool Initialize(string modelPath);
	}
}