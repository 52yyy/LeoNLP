using System.IO;

using NestedSequenceLabelingFramework;

using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.CandidatePairExtraction
{
	/// <summary>
	///		序列标注器工厂类
	/// </summary>
	internal static class SequenceSentenceFeatureExtractorFactory
	{
		/// <summary>
		///		创建一个序列标注器，POM任务的序列标注器包含两层，第一层用于标注实体词，第二层用于标注Chunk
		/// </summary>
		/// <param name="modelPath">模型文件路径</param>
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