using System.Collections.Generic;
using System.Text;

namespace NestedClassificationFramework
{
    /// <summary>
    ///		分类结果
    /// </summary>
    public class ClassificationResult
    {
        /// <summary>
        ///		分类结果的内容
        /// </summary>
        public Dictionary<string, double> TypeList { get; set; }

        public ClassificationResult()
        {
            TypeList = new Dictionary<string, double>();
        }

		public ClassificationResult(IEnumerable<string> types)
		{
			TypeList = new Dictionary<string, double>();
			foreach (string type in types)
			{
				this.Put(type,0);
			}
		}

        public void Put(string type, double value)
        {
            TypeList[type] = value;
        }

        /// <summary>
        ///		获得结果中最大的confidence
        /// </summary>
        public double GetMaxConfidence()
        {
            double ret = 0;
            foreach (var item in TypeList)
            {
                if (item.Value > ret)
                {
                    ret = item.Value;
                }
            }
            return ret;
        }

        /// <summary>
        ///		获得最大的confidence对应的string
        /// </summary>
        public string getItemWithMaxConfidence()
        {
            string ret = "";
            double maxConfidence = 0.0;
            foreach (var item in TypeList)
            {
                if (item.Value > maxConfidence)
                {
                    maxConfidence = item.Value;
                    ret = item.Key;
                }
            }
            return ret;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            string gap = "";
            foreach (var item in TypeList)
            {
                ret.Append(gap + item.ToString());
                gap = " ";
            }
            return ret.ToString();
        }
    }
}
