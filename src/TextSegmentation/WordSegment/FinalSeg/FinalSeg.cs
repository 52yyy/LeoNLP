using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordSegment
{
	internal class FinalSeg : IFinalSeg
	{
		private string _modelPath;


		private const string ProbEmit =  "prob_emit.txt";

		private static readonly char[] States =  { 'B', 'M', 'E', 'S' };

		private static IDictionary<char, IDictionary<char, double>> _emit;

		private static IDictionary<char, double> _start;

		private static IDictionary<char, IDictionary<char, double>> _trans;

		private static IDictionary<char, char[]> _prevStatus;

	    private const double MinFloat = -3.14e100;


	    public FinalSeg(string modelPath)
	    {
		    this._modelPath = modelPath;
			this.LoadModel(modelPath);
	    }

	    public bool IsChineseLetter(char c)
		{
			if (c >= 0x4e00 && c <= 0x9fbb)
			{
				return true;
			}
			return false;
		}

	    public void LoadModel(string modelPath)
		{
			//long s = System.currentTimeMillis();
			_prevStatus = new Dictionary<char, char[]>();
			_prevStatus.Add('B', new char[] { 'E', 'S' });
			_prevStatus.Add('M', new char[] { 'M', 'B' });
			_prevStatus.Add('S', new char[] { 'S', 'E' });
			_prevStatus.Add('E', new char[] { 'B', 'M' });

			_start = new Dictionary<char, double>();
			_start.Add('B', -0.26268660809250016);
			_start.Add('E', -3.14e+100);
			_start.Add('M', -3.14e+100);
			_start.Add('S', -1.4652633398537678);

			_trans = new Dictionary<char, IDictionary<char, double>>();
			var transB = new Dictionary<char, double>();
			transB.Add('E', -0.510825623765990);
			transB.Add('M', -0.916290731874155);
			_trans.Add('B', transB);
			var transE = new Dictionary<char, double>();
			transE.Add('B', -0.5897149736854513);
			transE.Add('S', -0.8085250474669937);
			_trans.Add('E', transE);
			var transM = new Dictionary<char, double>();
			transM.Add('E', -0.33344856811948514);
			transM.Add('M', -1.2603623820268226);
			_trans.Add('M', transM);
			var transS = new Dictionary<char, double>();
			transS.Add('B', -0.7211965654669841);
			transS.Add('S', -0.6658631448798212);
			_trans.Add('S', transS);

		    StreamReader reader = null;
		    try
		    {
				reader = new StreamReader(Path.Combine(modelPath, ProbEmit), Encoding.UTF8);
			    _emit = new Dictionary<char, IDictionary<char, double>>();
			    string line;
				IDictionary<char, double> values = null;
			    while ((line = reader.ReadLine()) != null)
			    {
				    if (string.IsNullOrEmpty(line.Trim()))
				    {
					    continue;
				    }
				    var tokens = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
				    if (tokens.Length == 1)
				    {
					    values = new Dictionary<char, double>();
					    _emit.Add(tokens[0][0], values);
				    }
				    else
				    {
					    values.Add(tokens[0][0], double.Parse(tokens[1]));
				    }
			    }
		    }
		    catch (IOException)
		    {
				throw new Exception(string.Format("%s: load model failure!", ProbEmit));
		    }
		    finally
		    {
			    if (reader != null)
			    {
				    reader.Close();
			    }
		    }
		}

		/// <summary>
		///		未登录词识别
		/// </summary>
		/// <param name="sentence"></param>
		/// <param name="tokens"></param>
		public void Cut(string sentence, List<string> tokens)
		{
			var chinese = new StringBuilder();
			var other = new StringBuilder();
			for (var i = 0; i < sentence.Length; ++i)
			{
				var ch = sentence[i];
				if (IsChineseLetter(ch))
				{
					if (other.Length > 0)
					{
						ProcessOtherUnknownWords(other.ToString(), tokens);
						other = new StringBuilder();
					}
					chinese.Append(ch);
				}
				else
				{
					if (chinese.Length > 0)
					{
						Viterbi(chinese.ToString(), tokens);
						chinese = new StringBuilder();
					}
					other.Append(ch);
				}

			}
		    if (chinese.Length > 0)
		    {
		        Viterbi(chinese.ToString(), tokens);
		    }
			else
			{
				ProcessOtherUnknownWords(other.ToString(), tokens);
			}
		}

		public void Viterbi(string sentence, List<string> tokens)
		{
            List<char> tmp;
			IDictionary<char, double> tmphash;
			char[] ch;
			var v = new List<IDictionary<char, double>>();
			var path = new Dictionary<char, List<char>>();


			v.Add(new Dictionary<char, double>());

			foreach (var state in States)
			{
				tmphash = (IDictionary<char, double>)_emit[state];
			    double emP;
			    emP = !tmphash.ContainsKey(sentence[0]) ? MinFloat : double.Parse(tmphash[sentence[0]].ToString());

				v.ElementAt(0).Add(state, Double.Parse(_start[state].ToString()) + emP);
				path.Add(state, new List<char>());
				tmp = (List<char>)path[state];
				tmp.Add(state);
				path[state] = tmp;
			}

			for (var i = 1; i < sentence.Length; ++i)
			{
				var vv = new Dictionary<char, double>();
				v.Add(vv);
				var newPath = new Dictionary<char, List<char>>();
				foreach (var y in States)
				{
					tmphash = (IDictionary<char, double>)_emit[y];
					//var emp = (double)tmphash[sentence[i]];
                    double emp;
					emp = !tmphash.ContainsKey(sentence[i]) ? MinFloat : double.Parse(tmphash[sentence[i]].ToString());

					Pair<char> candidate = null;
                    ch = (char[])_prevStatus[y];
					foreach (var y0 in ch)
					{
						tmphash = (IDictionary<char, double>)_trans[y0];
                        var tranp = (double)tmphash[y];
						if (tranp==null)
						{
							tranp = MinFloat;
						}
                        tranp += (emp + (double)v[i - 1][y0]);
					    if (candidate==null)
					    {
                            candidate = new Pair<char>(y0, tranp);
					    }
						else if (candidate.Freq <= tranp)
						{
							candidate.Freq = tranp;
							candidate.Key = y0;
						}
					}
					vv.Add(y, candidate.Freq);

                    var newPathValue = new List<char>();
                    newPathValue.AddRange((List<char>)path[candidate.Key]);
					newPathValue.Add(y);
					newPath.Add(y, newPathValue);
				}
				path = newPath;
			}
            var probE = (double)v[sentence.Length - 1]['E'];
            var probS = (double)v[sentence.Length - 1]['S'];
			List<char> posList;
		    if (probE < probS)
		    {
                posList = (List<char>)path['S'];
		    }
		    else
		    {
                posList = (List<char>)path['E'];
		    }

			int begin = 0, next = 0;
			for (var i = 0; i < sentence.Length; ++i)
			{
				var pos = posList[i];
				if (pos == 'B') begin = i;
				else if (pos == 'E')
				{
					tokens.Add(sentence.Substring(begin, i - begin + 1));
					next = i + 1;
				}
				else if (pos == 'S')
				{
					tokens.Add(sentence.Substring(i, 1));
					next = i + 1;
				}
			}
			if (next < sentence.Length) tokens.Add(sentence.Substring(next));
		}

		public void ProcessOtherUnknownWords(string other, List<string> tokens)
		{
			var p = new Regex(CharacterUtil.ReSkip);
			var mat = p.Matches(other);
			int current = 0;
			for (var i = 0; i < mat.Count; i++)
			{
				if (mat[i].Index > current)
				{
					tokens.Add(other.Substring(current, mat[i].Index - current));
					tokens.Add(mat[i].Value);
				}
				else if (mat[i].Index == current)
				{
					tokens.Add(mat[i].Value);
				}
				current = mat[i].Index + mat[i].Length;
			}
			if (current < other.Length) tokens.Add(other.Substring(current));
		}

		public IEnumerable<string> Cut(string sentence)
		{
			throw new NotImplementedException();
		}
	}
}

