namespace SentenceSegment
{
	internal static class ContextStateManager
	{
		public static FinishCheckContextState FinishCheckContextState = new FinishCheckContextState();
		public static CharCheckContextState CharCheckContextState = new CharCheckContextState();
		public static PairSignContextState PairSignContextState = new PairSignContextState();
		public static PairSignCloseContextState PairSignCloseContextState = new PairSignCloseContextState();
		public static SplitContextState SplitContextState = new SplitContextState();
	}
}