using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 通知公告
    /// </summary>
    [SugarTable("news")]
	public class NewsDbModel
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
		/// Desc: 简介
		/// </summary>
		[SugarColumn(ColumnName = "introduction")]
		public string Introduction { get; set; }

		/// <summary>
		/// Desc: 分类名称
		/// </summary>
		[SugarColumn(ColumnName = "typename")]
		public string Typename { get; set; }

		/// <summary>
		/// Desc: 发布人
		/// </summary>
		[SugarColumn(ColumnName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Desc: 头像
		/// </summary>
		[SugarColumn(ColumnName = "headportrait")]
		public string Headportrait { get; set; }

		/// <summary>
		/// Desc: 点击次数
		/// </summary>
        [SugarColumn(ColumnName = "clicknum")]
		public int? Clicknum { get; set; } = 0;

		/// <summary>
		/// Desc: 最近点击时间
		/// </summary>
        [SugarColumn(ColumnName = "clicktime")]
		public DateTime? Clicktime { get; set; }

		/// <summary>
		/// Desc: 赞
		/// </summary>
        [SugarColumn(ColumnName = "thumbsupnum")]
		public int? Thumbsupnum { get; set; } = 0;

		/// <summary>
		/// Desc: 踩
		/// </summary>
        [SugarColumn(ColumnName = "crazilynum")]
		public int? Crazilynum { get; set; } = 0;

		/// <summary>
		/// Desc: 收藏数
		/// </summary>
        [SugarColumn(ColumnName = "storeupnum")]
		public int? Storeupnum { get; set; } = 0;

		/// <summary>
		/// Desc: 图片
		/// </summary>
		[SugarColumn(ColumnName = "picture")]
		public string Picture { get; set; }

		/// <summary>
		/// Desc: 内容
		/// </summary>
		[SugarColumn(ColumnName = "content")]
		public string Content { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
