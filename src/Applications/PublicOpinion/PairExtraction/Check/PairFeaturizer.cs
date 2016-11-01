using System;
using System.Collections.Generic;
using System.Linq;

using Featurization;
using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction.Check
{
	/// <summary>
	///		�������������
	/// </summary>
	internal class PairFeaturizer : IPairFeaturizer
	{
		private IFeaturizable _featurizable;

		/// <summary>
		///		���캯��������һ���������������
		/// </summary>
		/// <param name="modelPath">ģ���ļ�·��</param>
		public PairFeaturizer(string modelPath)
		{
			this.Initialize(modelPath);
		}

		/// <summary>
		///		������ȡ����
		/// </summary>
		/// <param name="pair">������������Ϣ�ĺ�ѡ���</param>
		/// <returns>�ú�ѡ��Եĺ�ѡ��������</returns>
		public List<string> Featurize(CandidatePublicOpinionPair pair)
		{
			if (this._featurizable == null)
			{
				throw new ArgumentException("ģ��û�г�ʼ��");
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