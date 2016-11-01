using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
	public class TrainingLDA
	{
		private static Lda _lda;
        public readonly Utils Utils;

        public TrainingLDA()
        {
			_lda = new Lda(1);
            Utils = new Utils();
        }

		public TrainingLDA(int n)
		{
			_lda = new Lda(n);
			Utils = new Utils();
		}

        public void SingleFileLda(String path, Encoding encoding)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }
            var sr = new StreamReader(new FileStream(path, FileMode.Open), encoding);
            String temp;
            var i = 0;
            while ((temp = sr.ReadLine()) != null)
            {
                var words = Utils.WordFilter(temp.Split(Utils.Spliter, StringSplitOptions.RemoveEmptyEntries));
                _lda.AddDoc(Convert.ToString(++i), words);
            }
            sr.Close();
            //_lda.TrainAndSave("result/news/", encoding);
        }
	}

	public class Demo
	{
		private static Lda _lda;
		public readonly Utils Utils;

		public Demo()
		{
			_lda = new Lda(1);
			Utils = new Utils();
		}

		public Demo(int n)
		{
			_lda = new Lda(n);
			Utils = new Utils();
		}

		public LdaTrainedResult TrainModelWithFileLda(String path, Encoding encoding)
		{
			if (String.IsNullOrEmpty(path))
			{
				throw new ArgumentException();
			}
			var sr = new StreamReader(new FileStream(path, FileMode.Open), encoding);
			String temp;
			var i = 0;
			string doc = "";
			while ((temp = sr.ReadLine()) != null)
			{
				if (string.IsNullOrEmpty(doc))
				{
					doc = temp;
				}
				var words = Utils.WordFilter(temp.Split(Utils.Spliter, StringSplitOptions.RemoveEmptyEntries));
				_lda.AddDoc(Convert.ToString(++i), words);
			}
			sr.Close();
			return _lda.Train();
		}
	}
}
