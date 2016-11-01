using System.Collections.Generic;
using System.Linq;

using BasicUnit;

namespace LDA
{
    public class Document: Sentence
    {
        private readonly List<Sentence> _sentences;
        private readonly List<string> _documentWords;
        public Document()
        {
            _sentences = new List<Sentence>();
            _documentWords = new List<string>();
        }

        public void Add(Sentence sentence)
        {
            this._sentences.Add(sentence);
        }

        public void Add(List<Sentence> sentences)
        {
            this._sentences.AddRange(sentences);
        }

        public ICollection<object> GetWords()
        {
            return this._documentWords.ToArray();
        }

        public List<string> Doc2WordName(Document doc)
        {
            foreach (var words in doc.GetSentences().Select(sentence => sentence.Words))
            {
                _documentWords.AddRange(words.Select(w => w.Name));
            }
            return _documentWords;
        }

        private IEnumerable<Sentence> GetSentences()
        {
            return this._sentences;
        }


        public void Clear()
        {
            this._sentences.Clear();
            this._documentWords.Clear();
        }
    }
}
