using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 关于我们
    /// </summary>
    [SugarTable("aboutus")]
	public class AboutusDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 标题
		/// </summary>
		[SugarColumn(ColumnName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Desc: 副标题
		/// </summary>
		[SugarColumn(ColumnName = "subtitle")]
		public string Subtitle { get; set; }

		/// <summary>
		/// Desc: 内容
		/// </summary>
		[SugarColumn(ColumnName = "content")]
		public string Content { get; set; }

		/// <summary>
		/// Desc: 图片1
		/// </summary>
		[SugarColumn(ColumnName = "picture1")]
		public string Picture1 { get; set; }

		/// <summary>
		/// Desc: 图片2
		/// </summary>
		[SugarColumn(ColumnName = "picture2")]
		public string Picture2 { get; set; }

		/// <summary>
		/// Desc: 图片3
		/// </summary>
		[SugarColumn(ColumnName = "picture3")]
		public string Picture3 { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
