using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 在线留言
    /// </summary>
    [SugarTable("onlinemessage")]
	public class OnlinemessageDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 姓名
		/// </summary>
		[SugarColumn(ColumnName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Desc: 邮箱
		/// </summary>
		[SugarColumn(ColumnName = "email")]
		public string Email { get; set; }

		/// <summary>
		/// Desc: 电话
		/// </summary>
		[SugarColumn(ColumnName = "mobile")]
		public string Mobile { get; set; }

		/// <summary>
		/// Desc: 留言内容
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
