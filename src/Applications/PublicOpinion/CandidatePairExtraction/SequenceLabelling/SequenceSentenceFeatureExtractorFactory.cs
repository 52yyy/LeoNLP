using System.IO;

using NestedSequenceLabelingFramework;

using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		���б�ע��������
	/// </summary>
	internal static class SequenceSentenceFeatureExtractorFactory
	{
		/// <summary>
		///		����һ�����б�ע����POM��������б�ע���������㣬��һ�����ڱ�עʵ��ʣ��ڶ������ڱ�עChunk
		/// </summary>
		/// <param name="modelPath">ģ���ļ�·��</param>
		/// <returns></returns>
		public static IContentFeatureExtractor<LabelledSentence> Create(string modelPath)
		{
			SequenceScheduler scheduler = new SequenceScheduler();

			SequenceCalculator calculator1 = null;  // = new CrfSequenceCalculator();
			SequenceLabeller labeller1 = new SequenceLabeller("ne", new[] { "word", "pos" });
			labeller1.SetCalculator(calculator1);
			labeller1.Initialize(Path.Combine(modelPath, "NeLayer"));
			scheduler.AddLayer(labeller1);

			SequenceCalculator calculator2 = null;  // = new CrfSequenceCalculator();
			SequenceLabeller labeller2 = new SequenceLabeller("chunk", new[] { "word", "pos", "ne" });
			labeller2.SetCalculator(calculator2);
			labeller2.Initialize(Path.Combine(modelPath, "ChunkLayer"));
			scheduler.AddLayer(labeller2);

			IRootLayerConvertor convertor = new SmallSegwordRootLayerConvertor();

			return new SequenceSentenceFeatureExtractor(scheduler, convertor);
		}
	}
}