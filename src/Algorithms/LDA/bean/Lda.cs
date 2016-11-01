using System;
using System.Collections.Generic;
using System.Text;

namespace LDA
{
    public class Lda
    {
        private readonly LdaModel _ldaAModel;
	    public Lda(int topicNum) {
		    _ldaAModel = new LdaGibbsModel(topicNum, 5/(double)topicNum, 0.1, 100, int.MaxValue, int.MaxValue);
	    }

        public void AddDoc(String name, ICollection<Object> wordset)
        {
            _ldaAModel.AddDoc(name, wordset);
	    }

        public void AddDoc(string name, Document document)
        {
            AddDoc(name, document.GetWords());
        }
	

	    public void TrainAndSave(String modelPath, Encoding encoding) {
            _ldaAModel.TrainAndSave(modelPath, encoding);
	    }

	    public LdaTrainedResult Train()
	    {
		    return _ldaAModel.Train();
	    }

        public void GetToipcWords(out List<List<string>> topicwords)
        {
            topicwords = _ldaAModel.TopicWordsList;
        }
    }
}
