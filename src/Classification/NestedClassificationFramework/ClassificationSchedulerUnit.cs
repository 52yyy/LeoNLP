using NestedClassificationFramework.CalculationRule;
using NestedClassificationFramework.ClassificationCalculator;
using NestedClassificationFramework.ResultUpdation;

namespace NestedClassificationFramework
{
    /// <summary>
    ///		分类器的调度类中的调度单元
    /// </summary>
    public class ClassificationSchedulerUnit
    {
        /// <summary>
        ///		计算单元
        /// </summary>
        public IClassificationCalculator Calculator { get; set; }

        /// <summary>
        ///		分类结果是否达到要求的规则
        /// </summary>
        public ICalculationRule Rule { get; set; }

        /// <summary>
        ///		分类结果是否达到要求的规则
        /// </summary>
        public IResultUpdation Updation { get; set; }

        /// <summary>
        ///		构造方法
        /// </summary>
        public ClassificationSchedulerUnit(IClassificationCalculator calculator, ICalculationRule rule, IResultUpdation updation)
        {
            this.Calculator = calculator;
            this.Rule = rule;
            this.Updation = updation;
        }
    }
}
