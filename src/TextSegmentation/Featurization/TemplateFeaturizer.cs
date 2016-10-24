using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Featurization
{
	public class TemplateFeaturizer : IFeaturizable
	{
		private int _maxFeatureId;

		private List<string> _templates;

		public TemplateFeaturizer()
		{
		}

		public TemplateFeaturizer(string strFileName)
		{
			this.LoadTemplateFromFile(strFileName);
		}

		public int GetFeatureSize()
		{
			return this._maxFeatureId;
		}

		public List<string> GetFeatureTemplates()
		{
			return this._templates;
		}

		//Generate feature in string by given record, start position and saved templates
		public List<string> GenerateFeatures(List<string[]> record, int startX)
		{
			List<string> featureList = new List<string>();
			foreach (string strTemplate in this._templates)
			{
				//Generate feature by template
				string strFeature = this.GenerateFeature(strTemplate, record, startX);
				featureList.Add(strFeature);
			}

			return featureList;
		}

		public List<string> GenerateAllFeatures(List<string[]> record)
		{
			List<string> features = new List<string>();
			//The end of current record
			for (int i = 0; i < record.Count; i++)
			{
				//Get feature of current token
				List<string> featureList = this.GenerateFeatures(record, i);
				features.AddRange(featureList);
			}
			return features;
		}

		//Load template from given file
		public void LoadTemplateFromFile(string strFileName)
		{
			this._templates = new List<string>();

			StreamReader sr = new StreamReader(strFileName);
			string strLine = null;
			while ((strLine = sr.ReadLine()) != null)
			{
				strLine = strLine.Trim();
				if (strLine.StartsWith("#") == true)
				{
					//Ignore comment line
					continue;
				}

				//Only load U templates
				if (strLine.StartsWith("U") == true)
				{
					this._templates.Add(strLine);
				}
				else if (strLine.StartsWith("MaxTemplateFeatureId:") == true)
				{
					strLine = strLine.Replace("MaxTemplateFeatureId:", "");
					this._maxFeatureId = int.Parse(strLine);
				}
			}
			sr.Close();
		}

		//U22:%x[-4,0]/%x[-3,0]/%x[-2,0]
		private string GenerateFeature(string strTemplate, List<string[]> record, int startX)
		{
			StringBuilder sb = new StringBuilder();

			string[] keyvalue = strTemplate.Split(':');
			string tId = keyvalue[0];
			sb.Append(tId);
			sb.Append(":");

			string[] items = keyvalue[1].Split('/');
			foreach (string item in items)
			{
				int bpos = item.LastIndexOf('[');
				int enpos = item.LastIndexOf(']');
				string strPos = item.Substring(bpos + 1, enpos - bpos - 1);
				string[] xy = strPos.Split(',');
				int x = int.Parse(xy[0]) + startX;
				int y = int.Parse(xy[1]);

				if (x >= 0 && x < record.Count && y >= 0 && y < record[x].Length)
				{
					sb.Append(record[x][y]);
				}
				else
				{
					if (x < 0)
					{
						sb.Append("B_" + x.ToString() + "_" + xy[1]);
					}
					else
					{
						sb.Append("B_" + (x - record.Count + 1).ToString() + "_" + xy[1]);
					}
				}
				sb.Append("/");
			}

			sb.Remove(sb.Length - 1, 1);

			return sb.ToString();
		}
	}
}