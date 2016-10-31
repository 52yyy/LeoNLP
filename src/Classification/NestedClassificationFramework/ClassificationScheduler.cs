using System.Collections.Generic;
using BasicUnit;
using NestedClassificationFramework.CalculationRule;
using NestedClassificationFramework.ClassificationCalculator;
using NestedClassificationFramework.ResultUpdation;

namespace NestedClassificationFramework
{
    /// <summary>
    ///		分类器的调度类
    /// </summary>
    public class ClassificationScheduler
    {
        /// <summary>
        ///		调度中包含的分类器
        /// </summary>
        private List<ClassificationSchedulerUnit> _layers;

        /// <summary>
        ///		支持的分类
        /// </summary>
        private List<string> _classificationTypes;
       
        /// <summary>
        ///		构造函数
        /// </summary>
        public ClassificationScheduler(IEnumerable<string> classificationLabels)
        {
            _classificationTypes = new List<string>();
            foreach (string label in classificationLabels)
            {
                _classificationTypes.Add(label);
            }

            _layers = new List<ClassificationSchedulerUnit>();
        }

        /// <summary>
        ///		添加一层计算单元
        /// </summary>
        public void AddLayer(ClassificationSchedulerUnit unit)
        {
            _layers.Add(unit);
        }

        /// <summary>
        ///	    获得某一层计算单元
        /// </summary>
        public ClassificationSchedulerUnit GetUnitAt(int index)
        {            
            return _layers[index];
        }

        /// <summary>
        ///	    计算
        /// </summary>
        public ClassificationResultWrapper Calculate(Sentence sentence)
        {
            var ret = new ClassificationResult();
	        for (int i = 0; i < this._layers.Count; i++)
	        {
		        ClassificationSchedulerUnit unit = this._layers[i];
				IClassificationCalculator calculator = unit.Calculator;
				ICalculationRule rule = unit.Rule;
				IResultUpdation updation = unit.Updation;
				Dictionary<string, double> rawResult = calculator.Calculate(sentence);
				ClassificationResult currentResult = FiltereResult(rawResult);
				ret = updation.UpdateResult(ret, currentResult);
				if (rule.HasDone(ret))
				{
					return new ClassificationResultWrapper(ret, true, calculator.CalculatorConfidence);
				}
	        }
	        return new ClassificationResultWrapper(ret, false, 10);
        }

        /// <summary>
        ///	    获得计算单元的个数
        /// </summary>
        private ClassificationResult FiltereResult(Dictionary<string, double> rawResult)
        {
            var ret = new ClassificationResult(this._classificationTypes);
			foreach (string type in _classificationTypes)
	        {
				if (rawResult.ContainsKey(type))
		        {
					ret.TypeList[type] = rawResult[type];
		        }
	        }
            return ret;
        }

        /// <summary>
        ///	    获得计算单元的个数
        /// </summary>
        public int GetLayNum()
        {
            return _layers.Count;
        }
        
    }
}
