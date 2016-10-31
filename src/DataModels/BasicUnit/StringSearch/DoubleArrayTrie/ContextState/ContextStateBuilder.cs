namespace BasicUnit.DoubleArrayTrie
{
	internal static class ContextStateManager
	{
		public static readonly InsertContextState IndexInsertContextState = new IndexInsertContextState();
		public static readonly InsertContextState SearchInsertContextState = new SearchInsertContextState();
		public static readonly InsertContextState DeleteInsertContextState = new DeleteInsertContextState();
		public static readonly PrefixCollisionUpdateContextState IndexPrefixCollisionUpdateContextState = new IndexPrefixCollisionUpdateContextState();
		public static readonly PrefixCollisionUpdateContextState SearchPrefixCollisionUpdateContextState = new SearchPrefixCollisionUpdateContextState();
		public static readonly PrefixCollisionUpdateContextState DeletePrefixCollisionUpdateContextState = new DeletePrefixCollisionUpdateContextState();
		public static readonly TailCollisionUpdateContextState IndexTailCollisionUpdateContextState = new IndexTailCollisionUpdateContextState();
		public static readonly TailCollisionUpdateContextState SearchTailCollisionUpdateContextState = new SearchTailCollisionUpdateContextState();
		public static readonly TailCollisionUpdateContextState DeleteTailCollisionUpdateContextState = new DeleteTailCollisionUpdateContextState();
		public static readonly GCheckContextState GCheckContextState = new GCheckContextState();
		public static readonly BCheckContextState BCheckContextState = new BCheckContextState();
	}

	internal class ContextStateBuilder
	{
		public static InsertContextState BuildInsertContextState(TrieState trieState)
		{
			switch (trieState)
			{
				case TrieState.Index:
				{
					return ContextStateManager.IndexInsertContextState;
				}
				case TrieState.Search:
				{
					return ContextStateManager.SearchInsertContextState;
				}
				default:
				{
					return ContextStateManager.DeleteInsertContextState;
				}
			}
		}

		public static PrefixCollisionUpdateContextState BuildPrefixCollisionUpdateContextState(TrieState trieState)
		{
			switch (trieState)
			{
				case TrieState.Index:
				{
					return ContextStateManager.IndexPrefixCollisionUpdateContextState;
				}
				case TrieState.Search:
				{
					return ContextStateManager.SearchPrefixCollisionUpdateContextState;
				}
				default:
				{
					return ContextStateManager.DeletePrefixCollisionUpdateContextState;
				}
			}
		}

		public static TailCollisionUpdateContextState BuildTailCollisionUpdateContextState(TrieState trieState)
		{
			switch (trieState)
			{
				case TrieState.Index:
				{
					return ContextStateManager.IndexTailCollisionUpdateContextState;
				}
				case TrieState.Search:
				{
					return ContextStateManager.SearchTailCollisionUpdateContextState;
				}
				default:
				{
					return ContextStateManager.DeleteTailCollisionUpdateContextState;
				}
			}
		}
	}
}