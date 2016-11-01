using System.Collections.Generic;
using System.IO;

using BasicUnit;

using NestedClassificationFramework;
using NestedClassificationFramework.CalculationRule;
using NestedClassificationFramework.ClassificationCalculator;
using NestedClassificationFramework.ResultUpdation;

using PublicOpinion.DataModel.Inner;

namespace PublicOpinion.PairExtraction.Check
{
	/// <summary>
	///		基于分类的配对检查器
	/// </summary>
	internal class ClassifyPairChecker : IPublicOpinionPairChecker
	{
		private IPairFeaturizer _pairFeaturizer;

		private ClassificationScheduler _classifier;

		/// <summary>
		///		构造方法，构造一个基于分类的配对检查器
		/// </summary>
		/// <param name="modelPath">模型文件路径</param>
		public ClassifyPairChecker(string modelPath)
		{
			this.Initialize(modelPath);
		}


		/// <summary>
		///		根据候选配对的上下文信息检查配对是否存在
		/// </summary>
		/// <param name="pair">候选配对</param>
		/// <returns>true表示配对存在应当保留，false表示配对不存在应当移除</returns>
		public bool Check(CandidatePublicOpinionPair pair)
		{
			List<string> features = _pairFeaturizer.Featurize(pair);
			Sentence sentence = new Sentence();
			foreach (string feature in features)
			{
				sentence.Words.Add(new Word(feature));
			}
			ClassificationResultWrapper result = this._classifier.Calculate(sentence);
			string isAccept = result.Result.getItemWithMaxConfidence();
			return isAccept == "1";  // 表示分类器接受类型"1"，即配对存在
		}

		private bool Initialize(string modelPath)
		{
			var labelList = new List<string> { "1", "0" };  // 这里的"1"和"0"都与模型文件强绑定，即模型文件必须以1和0命名，否则无效。
			IClassificationCalculator calculator = null;  // ClassificationCalculatorFactory.CreateSvmClassificationCalculator(modelPath, ClassifierType.svmnet, new DefaultFeatureGenerator());
			ICalculationRule rule = CalculationRuleFactory.CreateDefaultConfidenceRule();
			IResultUpdation updation = ResultUpdationFactory.CreateOneGramResultUpdation();

			this._classifier = new ClassificationScheduler(labelList);
			this._classifier.AddLayer(new ClassificationSchedulerUnit(calculator, rule, updation));

			this._pairFeaturizer = new PairFeaturizer(Path.Combine(modelPath, "test.template"));
			return true;
		}
	}
}