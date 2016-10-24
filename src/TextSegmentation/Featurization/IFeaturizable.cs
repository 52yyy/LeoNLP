using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featurization
{
	/// <summary>
	///		特征化接口
	/// </summary>
	public interface IFeaturizable
	{
		/// <summary>
		///		生成特征
		/// </summary>
		/// <param name="record">原始数据</param>
		/// <param name="startX">初始位置</param>
		/// <returns></returns>
		List<string> GenerateFeatures(List<string[]> record, int startX);

		/// <summary>
		///		生成全部特征
		/// </summary>
		/// <param name="record">原始数据</param>
		/// <returns></returns>
		List<string> GenerateAllFeatures(List<string[]> record);
	}
}
