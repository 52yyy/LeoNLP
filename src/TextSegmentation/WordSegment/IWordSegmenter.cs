using BasicUnit;

namespace WordSegment
{
	/// <summary>
	///		���ķִʽӿ�
	/// </summary>
	public interface IWordSegmenter : IInitializable
	{
		/// <summary>
		///		��������ַ����������ķִ�
		/// </summary>
		/// <param name="sentenceString">���ִʵ��ַ���</param>
		/// <returns>�ִʽ��</returns>
		Sentence SegWord(string sentenceString);
	}
}