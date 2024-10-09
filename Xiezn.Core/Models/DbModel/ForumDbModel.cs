using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 在线交流
    /// </summary>
    [SugarTable("forum")]
	public class ForumDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 帖子标题
		/// </summary>
		[SugarColumn(ColumnName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Desc: 帖子内容
		/// </summary>
		[SugarColumn(ColumnName = "content")]
		public string Content { get; set; }

		/// <summary>
		/// Desc: 父节点id
		/// </summary>
        [SugarColumn(ColumnName = "parentid")]
		public long? Parentid { get; set; } = 0;

		/// <summary>
		/// Desc: 用户id
		/// </summary>
        [SugarColumn(ColumnName = "userid")]
		public long? Userid { get; set; } = 0;

		/// <summary>
		/// Desc: 用户名
		/// </summary>
		[SugarColumn(ColumnName = "username")]
		public string Username { get; set; }

		/// <summary>
		/// Desc: 头像
		/// </summary>
		[SugarColumn(ColumnName = "avatarurl")]
		public string Avatarurl { get; set; }

		/// <summary>
		/// Desc: 状态
		/// </summary>
		[SugarColumn(ColumnName = "isdone")]
		public string Isdone { get; set; }

		/// <summary>
		/// Desc: 是否置顶
		/// </summary>
        [SugarColumn(ColumnName = "istop")]
		public int? Istop { get; set; } = 0;

		/// <summary>
		/// Desc: 置顶时间
		/// </summary>
        [SugarColumn(ColumnName = "toptime")]
		public DateTime? Toptime { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

		/// <summary>
        /// Desc: 额外字段
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<ForumDbModel> Childs { get; set; }
	}
}
