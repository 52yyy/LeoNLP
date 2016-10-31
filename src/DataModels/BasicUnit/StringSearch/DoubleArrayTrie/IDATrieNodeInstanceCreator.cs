namespace BasicUnit.DoubleArrayTrie
{
	/// <summary>
	///		Dat node 实例接口
	/// </summary>
	public interface IDATrieNodeInstanceCreator
	{
		DATrieNode CreateNewDoubleArrayTrieNode(int index);

		DATrieNode CreateNewDoubleArrayTrieNode(string key, object value);
	}

	/// <summary>
	///		default dat node 实例器
	/// </summary>
	public class DefaultNodeInstanceCreator : IDATrieNodeInstanceCreator
	{
		public DATrieNode CreateNewDoubleArrayTrieNode(int index)
		{
			return new DefaultDATrieNode(index);
		}

		public DATrieNode CreateNewDoubleArrayTrieNode(string key, object value)
		{
			return new DefaultDATrieNode(key, value);
		}
	}
}