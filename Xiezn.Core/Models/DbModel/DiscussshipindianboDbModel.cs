using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 视频点播评论表
    /// </summary>
    [SugarTable("discussshipindianbo")]
	public class DiscussshipindianboDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 关联表id
		/// </summary>
        [SugarColumn(ColumnName = "refid")]
		public long? Refid { get; set; } = 0;

		/// <summary>
		/// Desc: 用户id
		/// </summary>
        [SugarColumn(ColumnName = "userid")]
		public long? Userid { get; set; } = 0;

		/// <summary>
		/// Desc: 头像
		/// </summary>
		[SugarColumn(ColumnName = "avatarurl")]
		public string Avatarurl { get; set; }

		/// <summary>
		/// Desc: 用户名
		/// </summary>
		[SugarColumn(ColumnName = "nickname")]
		public string Nickname { get; set; }

		/// <summary>
		/// Desc: 评论内容
		/// </summary>
		[SugarColumn(ColumnName = "content")]
		public string Content { get; set; }

		/// <summary>
		/// Desc: 评分
		/// </summary>
        [SugarColumn(ColumnName = "score")]
		public double? Score { get; set; } = 0;

		/// <summary>
		/// Desc: 回复内容
		/// </summary>
		[SugarColumn(ColumnName = "reply")]
		public string Reply { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
