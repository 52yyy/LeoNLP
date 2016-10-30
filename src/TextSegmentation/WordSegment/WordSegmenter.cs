using System.Collections.Generic;
using BasicUnit;

namespace WordSegment
{
	public class WordSegmenter : IWordSegmenter, ILexcionCustomizable
	{
		private bool _initialized;

		private JiebaSegmenter _segmenter;

        /// <summary>
        ///		对输入的字符串进行中文分词
        /// </summary>
        /// <param name="sentenceString">待分词的字符串</param>
        /// <returns>分词结果</returns>
		public Sentence SegWord(string sentenceString)
		{
			var sentence = new Sentence();
	        IEnumerable<SegToken> segTokens = this._segmenter.Process(sentenceString, SegMode.Search);
			foreach (var segToken in segTokens)
			{
				string content = segToken.Word;
				string pos = segToken.Tag;
				int start = segToken.StartOffset;
				int end = segToken.EndOffset;
				var word = new Word { Name = content, Pos = pos, Start = start, End = end };
				sentence.Words.Add(word);
			}
			return sentence;
		}

		/// <summary>
		///		加载用户词典
		/// </summary>
		/// <param name="dictPath"></param>
		/// <returns></returns>
		public bool ImportUserDict(string dictPath)
		{
			_segmenter.ImportUserDict(dictPath);
			return true;
		}

		/// <summary>
		///		初始化，加载词典文件
		/// </summary>
		/// <param name="modelPath">中文分词词典文件的路径</param>
		/// <returns></returns>
		public bool Initialize(string modelPath)
		{
			if (_initialized)
			{
				return false;
			}
			this._segmenter = new JiebaSegmenter(modelPath);
			return true;
		}
    }
}
