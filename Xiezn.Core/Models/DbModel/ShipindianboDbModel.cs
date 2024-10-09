using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 视频点播
    /// </summary>
    [SugarTable("shipindianbo")]
	public class ShipindianboDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 视频名称
		/// </summary>
		[SugarColumn(ColumnName = "shipinmingcheng")]
		public string Shipinmingcheng { get; set; }

		/// <summary>
		/// Desc: 视频分类
		/// </summary>
		[SugarColumn(ColumnName = "shipinfenlei")]
		public string Shipinfenlei { get; set; }

		/// <summary>
		/// Desc: 视频标签
		/// </summary>
		[SugarColumn(ColumnName = "shipinbiaoqian")]
		public string Shipinbiaoqian { get; set; }

		/// <summary>
		/// Desc: 视频封面
		/// </summary>
		[SugarColumn(ColumnName = "shipinfengmian")]
		public string Shipinfengmian { get; set; }

		/// <summary>
		/// Desc: 视频内容
		/// </summary>
		[SugarColumn(ColumnName = "shipinneirong")]
		public string Shipinneirong { get; set; }

		/// <summary>
		/// Desc: 视频介绍
		/// </summary>
		[SugarColumn(ColumnName = "shipinjieshao")]
		public string Shipinjieshao { get; set; }

		/// <summary>
		/// Desc: 发布日期
		/// </summary>
        [SugarColumn(ColumnName = "faburiqi")]
		[JsonConverter(typeof(Common.Helpers.DateFormatConverter), "yyyy-MM-dd")]
		public DateTime Faburiqi { get; set; }

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
		/// Desc: 最近点击时间
		/// </summary>
        [SugarColumn(ColumnName = "clicktime")]
		public DateTime? Clicktime { get; set; }

		/// <summary>
		/// Desc: 浏览时长
		/// </summary>
        [SugarColumn(ColumnName = "browseduration")]
		public int? Browseduration { get; set; } = 0;

		/// <summary>
		/// Desc: 评论数
		/// </summary>
        [SugarColumn(ColumnName = "discussnum")]
		public int? Discussnum { get; set; } = 0;

		/// <summary>
		/// Desc: 收藏数
		/// </summary>
        [SugarColumn(ColumnName = "storeupnum")]
		public int? Storeupnum { get; set; } = 0;

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
