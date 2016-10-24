namespace SentenceSegment
{
	/// <summary>
	///		上下文的状态
	/// </summary>
	internal abstract class ContextState
	{
		/// <summary>
		///		在当前状态下执行处理
		/// </summary>
		/// <param name="context"></param>
		public abstract void Execute(Context context);
	}
}