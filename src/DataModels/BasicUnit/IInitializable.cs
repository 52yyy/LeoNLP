namespace BasicUnit
{
	/// <summary>
	///		模型初始化接口
	/// </summary>
	public interface IInitializable
	{
		/// <summary>
		///		初始化
		/// </summary>
		/// <param name="modelPath">模型所在的路径</param>
		/// <returns>返回初始化是否成功</returns>
		bool Initialize(string modelPath);
	}
}
