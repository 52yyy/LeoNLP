using System;
using System.Collections.Generic;
using System.Linq;

using Featurization;
using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction.Check
{
	/// <summary>
	///		配对特征生成器
	/// </summary>
	internal class PairFeaturizer : IPairFeaturizer
	{
		private IFeaturizable _featurizable;

		/// <summary>
		///		构造函数，创建一个配对特征生成器
		/// </summary>
		/// <param name="modelPath">模型文件路径</param>
		public PairFeaturizer(string modelPath)
		{
			this.Initialize(modelPath);
		}

		/// <summary>
		///		特征抽取方法
		/// </summary>
		/// <param name="pair">包含上下文信息的候选配对</param>
		/// <returns>该候选配对的候选特征集合</returns>
		public List<string> Featurize(CandidatePublicOpinionPair pair)
		{
			if (this._featurizable == null)
			{
				throw new ArgumentException("模型没有初始化");
			}

			List<string[]> record = new List<string[]>();
			foreach (ChunkWord word in pair.Chunk.Words)
			{
				record.Add(new[] { word.Name, word.Tag });
			}

			List<string> featureList = new List<string>();
			if (pair.TargetChunkIndex >= 0)
			{
				List<string> tmp = this._featurizable.GenerateFeatures(record, pair.TargetChunkIndex);
				featureList.AddRange(tmp.Select(s => "T" + s));
			}
			if (pair.FeatureChunkIndex >= 0)
			{
				List<string> tmp = this._featurizable.GenerateFeatures(record, pair.FeatureChunkIndex);
				featureList.AddRange(tmp.Select(s => "F" + s));
			}
			if (pair.ModifyChunkIndex >= 0)
			{
				List<string> tmp = this._featurizable.GenerateFeatures(record, pair.ModifyChunkIndex);
				featureList.AddRange(tmp.Select(s => "M" + s));
			}
			if (pair.OpinionChunkIndex >= 0)
			{
				List<string> tmp = this._featurizable.GenerateFeatures(record, pair.OpinionChunkIndex);
				featureList.AddRange(tmp.Select(s => "O" + s));
			}
			return featureList;
		}

		private bool Initialize(string modelPath)
		{
			this._featurizable = new TemplateFeaturizer(modelPath);
			return true;
		}
	}
}