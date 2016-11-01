using System;

namespace LDA
{
    public class LdaGibbsModel : LdaModel
    {
        public LdaGibbsModel(int topicNum, double alpha, double beta, int iteration, int saveStep, int beginSaveIters)
            : base(topicNum, alpha, beta, iteration, saveStep, beginSaveIters)
        { }

        // Compute p(z_i = k|z_-i, w) 抽样
        public override void SampleTopic(Doc doc, Vector vector)
        {
			var oldTopic = vector.TopicId;
			doc.RemoveVector(vector);
			Topics[oldTopic].RemoveVector(vector);

            var p = new double[TopicNum];
			for (var k = 0; k < TopicNum; k++)
	        {
		        p[k] = (Topics[k].VectorIdArray[vector.Id] + Beta) 
					/ (Topics[k].VCount + WordCount * Beta)
					* (doc.TopicArray[k] + Alpha) 
					/ (doc.Vectors.Count - 1 + TopicNum * Alpha);
	        }

	        // 累计使得p[k]是前面所有topic可能性的和
            for (int k = 1; k < TopicNum; k++)
            {
                p[k] += p[k - 1];
            }

            double u = Rdm.NextDouble() * p[TopicNum - 1];
			int newTopic;
            for (newTopic = 0; newTopic < TopicNum; newTopic++)
            {
                if (u < p[newTopic])
                {
                    break;
                }
            }

            vector.TopicId = newTopic;
			Topics[newTopic].AddVector(vector);
			doc.UpdateVecotr(vector);
        }

        //@Override
        public override void UpdateEstimatedParameters(Double[,] phi, Double[,] theta)
        {
            for (int k = 0; k < TopicNum; k++)
            {
                Topic topic = Topics[k];
                for (int v = 0; v < WordCount; v++)
                {
                    phi[k,v] = (topic.VectorIdArray[v] + Beta) / (topic.VCount + WordCount * Beta);
                }
            }

            for (int d = 0; d < DocCount; d++)
            {
                Doc doc = Docs[d];
                for (int k = 0; k < TopicNum; k++)
                {
                    theta[d,k] = (doc.TopicArray[k] + Alpha) / (doc.Vectors.Count + TopicNum * Alpha);
                }
            }
        }
    }
}
