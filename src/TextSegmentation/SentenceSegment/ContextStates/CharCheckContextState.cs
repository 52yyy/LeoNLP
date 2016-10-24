using System.Linq;

namespace SentenceSegment
{
	/// <summary>
	///		字符检查状态
	/// </summary>
	internal class CharCheckContextState : ContextState
	{
		/// <summary>
		///		在当前状态下执行处理
		///		检查字符类型,并实现调度
		/// </summary>
		/// <param name="context"></param>
		public override void Execute(Context context)
		{
			char currentChar = context.CurrentChar;

			// 没有引号时，字符都直接进入即可
			// 匹配起始引号
			if (!context.IsPairSignOpened && PairSign.PairSigns.ContainsKey(currentChar))//&& (context.CurrentIndex + 1 == context.Text.Length))
			{
				context.State = ContextStateManager.PairSignContextState;
			}
				// 匹配断句号
			else if (Sign.SplitSign.Contains(currentChar))
			{
				context.CurrentSentenceBuilder.Append(currentChar);

				// 切换到断句状态
				context.State = ContextStateManager.SplitContextState;
			}
			else if (context.IsPairSignOpened && context.BackPairSign == currentChar)
			{

				// 切换到引号匹配关闭状态
				context.State = ContextStateManager.PairSignCloseContextState;
			}
				// 非sign字符
			else
			{
				context.CurrentSentenceBuilder.Append(currentChar);
				// 准备处理下一个字符
				context.State = ContextStateManager.FinishCheckContextState;
			}
		}
	}
}