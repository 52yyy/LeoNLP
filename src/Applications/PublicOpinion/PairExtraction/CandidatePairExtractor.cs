using System.Collections.Generic;

using PublicOpinion.DataModel;
using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction
{
	/// <summary>
	///		候选配对抽取器
	/// </summary>
	internal class CandidatePairExtractor
	{
		/// <summary>
		///		从ChunkSentence中抽取候选配对
		/// </summary>
		/// <param name="chunk">单个ChunkSentence</param>
		/// <returns>候选配对集合</returns>
		public IEnumerable<CandidatePublicOpinionPair> CandidatePairExtract(ChunkSentence chunk)
		{
			List<ChunkTargetWord> targets = new List<ChunkTargetWord>();
			List<ChunkFeatureWord> features = new List<ChunkFeatureWord>();
			List<ChunkModifyWord> modifies = new List<ChunkModifyWord>();
			List<ChunkOpinionWord> opinions = new List<ChunkOpinionWord>();
			foreach (ChunkWord word in chunk.Words)
			{
				switch (word.Tag)
				{
					case "T":
					{
						targets.Add(new ChunkTargetWord(word));
						break;
					}
					case "F":
					{
						features.Add(new ChunkFeatureWord(word));
						break;
					}
					case "M":
					{
						modifies.Add(new ChunkModifyWord(word));
						break;
					}
					case "O":
					{
						opinions.Add(new ChunkOpinionWord(word));
						break;
					}
				}
			}

			if (targets.Count == 0)
			{
				targets.Add(null);
			}
			if (features.Count == 0)
			{
				features.Add(null);
			}
			if (modifies.Count == 0)
			{
				modifies.Add(null);
			}
			if (opinions.Count == 0)
			{
				opinions.Add(null);
			}


			foreach (var tmp in this.Run(targets, features, modifies, opinions, chunk))
			{
				yield return tmp;
			}
		}


		public IEnumerable<CandidatePublicOpinionPair> CandidatePairExtract(ChunkSentence chunk, PomMeta meta)
		{
			List<ChunkTargetWord> targets = new List<ChunkTargetWord>();
			List<ChunkFeatureWord> features = new List<ChunkFeatureWord>();
			List<ChunkModifyWord> modifies = new List<ChunkModifyWord>();
			List<ChunkOpinionWord> opinions = new List<ChunkOpinionWord>();
			foreach (ChunkWord word in chunk.Words)
			{
				switch (word.Tag)
				{
					case "T":
					{
						targets.Add(new ChunkTargetWord(word));
						break;
					}
					case "F":
					{
						features.Add(new ChunkFeatureWord(word));
						break;
					}
					case "M":
					{
						modifies.Add(new ChunkModifyWord(word));
						break;
					}
					case "O":
					{
						opinions.Add(new ChunkOpinionWord(word));
						break;
					}
				}
			}

			if (targets.Count == 0)
			{
				targets.Add(null);
			}
			if (features.Count == 0)
			{
				features.Add(null);
			}
			if (modifies.Count == 0)
			{
				modifies.Add(null);
			}
			if (opinions.Count == 0)
			{
				opinions.Add(null);
			}


			foreach (var tmp in this.Run(targets, features, modifies, opinions, chunk))
			{
				yield return tmp;
			}


			if (meta == null)
			{
				yield break;
			}

			ChunkTargetWord metaTarget = new ChunkTargetWord(meta.Target);
			ChunkFeatureWord metaFeature = new ChunkFeatureWord(meta.Feature);

			if (metaTarget.Name != null)
			{
				foreach (ChunkFeatureWord feature in features)
				{
					foreach (ChunkOpinionWord opinion in opinions)
					{
						bool isPair = this.Check(feature, opinion);
						if (!isPair)
						{
							continue;
						}
						foreach (ChunkModifyWord modify in modifies)
						{
							var candidate = new CandidatePublicOpinionPair(metaTarget, feature, modify, opinion, chunk);

							yield return candidate;
						}
					}
				}
			}

			if (metaFeature.Name != null)
			{
				foreach (ChunkTargetWord target in targets)
				{
					foreach (ChunkModifyWord modify in modifies)
					{
						foreach (ChunkOpinionWord opinion in opinions)
						{
							var candidate = new CandidatePublicOpinionPair(target, metaFeature, modify, opinion, chunk);

							yield return candidate;
						}
					}
				}
			}

			if (metaTarget.Name != null)
			{
				if (metaFeature.Name != null)
				{
					foreach (ChunkModifyWord modify in modifies)
					{
						foreach (ChunkOpinionWord opinion in opinions)
						{
							var candidate = new CandidatePublicOpinionPair(metaTarget, metaFeature, modify, opinion, chunk);

							yield return candidate;
						}
					}
				}
			}
		}

		private bool Check(FeatureWord feature, OpinionWord opinion)
		{
			if (feature == null && opinion == null)
			{
				return false;
			}
			return true;
		}

		private IEnumerable<CandidatePublicOpinionPair> Run(
			IEnumerable<ChunkTargetWord> targets,
			IEnumerable<ChunkFeatureWord> features,
			IEnumerable<ChunkModifyWord> modifies,
			IEnumerable<ChunkOpinionWord> opinions,
			ChunkSentence chunk)
		{
			foreach (ChunkFeatureWord feature in features)
			{
				foreach (ChunkOpinionWord opinion in opinions)
				{
					bool isPair = this.Check(feature, opinion);
					if (!isPair)
					{
						continue;
					}
					foreach (ChunkTargetWord target in targets)
					{
						foreach (ChunkModifyWord modify in modifies)
						{
							var candidate = new CandidatePublicOpinionPair(target, feature, modify, opinion, chunk);
							yield return candidate;
						}
					}
				}
			}
		}
	}
}