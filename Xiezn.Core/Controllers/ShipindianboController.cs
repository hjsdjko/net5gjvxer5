using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xiezn.Core.Business.Services;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models;
using Xiezn.Core.Models.DbModel;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Text;
using System.Net;
using NETCore.MailKit.Core;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Alipay.AopSdk.Core;
using Xiezn.Core.Models.ConfModel;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Xiezn.Core.Models.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;
using StackExchange.Redis;


namespace Xiezn.Core.Controllers
{
    /// <summary>
    /// 视频点播相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class ShipindianboController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _savePath;
        private readonly long _uid;
        private readonly string _role;
        private readonly ShipindianboService _bll;
        private readonly IEmailService _EmailService;
        private readonly ConfigService _configBLL;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShipindianboController(IHostingEnvironment hostingEnvironment, IEmailService emailService)
        {
            try
            {
                _hostingEnvironment = hostingEnvironment;
                _savePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar + ConfigHelper.GetConfig("SchemaName") + Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar;
                if (CacheHelper.TokenModel != null)
                {
                    _uid = CacheHelper.TokenModel.Uid;
                    _role = CacheHelper.TokenModel.Role;
                }

                _EmailService = emailService;
                _configBLL = new ConfigService();
            }
            catch
            {
                _uid = 0;
                _role = "游客";
            }

            _hostingEnvironment = hostingEnvironment;
            _savePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar + ConfigHelper.GetConfig("SchemaName") + Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar;
            _bll = new ShipindianboService();
        }


        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Page(int page = 1, int limit = 10, string sort = "id", string order = "asc")
        {
            try
            {
            	List<IConditionalModel> conModels = new List<IConditionalModel>();
                List<String> jujianList = new List<string>();
                jujianList.Add("faburiqistart");
                jujianList.Add("faburiqiend");
                var faburiqistart = HttpContext.Request.Query["faburiqistart"].ToString();
                if (!string.IsNullOrEmpty(faburiqistart))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.GreaterThanOrEqual, FieldValue = faburiqistart });
                }
                var faburiqiend = HttpContext.Request.Query["faburiqiend"].ToString();
                if (!string.IsNullOrEmpty(faburiqiend))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.LessThanOrEqual, FieldValue = faburiqiend });
                }

                var shipinmingcheng = HttpContext.Request.Query["shipinmingcheng"].ToString();
                if (!string.IsNullOrEmpty(shipinmingcheng))
                {
                    if (shipinmingcheng.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinmingcheng", ConditionalType = ConditionalType.Like, FieldValue = shipinmingcheng });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinmingcheng", ConditionalType = ConditionalType.Equal, FieldValue = shipinmingcheng });
                    }
                }
                var shipinfenlei = HttpContext.Request.Query["shipinfenlei"].ToString();
                if (!string.IsNullOrEmpty(shipinfenlei))
                {
                    if (shipinfenlei.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinfenlei", ConditionalType = ConditionalType.Like, FieldValue = shipinfenlei });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinfenlei", ConditionalType = ConditionalType.Equal, FieldValue = shipinfenlei });
                    }
                }
                var shipinbiaoqian = HttpContext.Request.Query["shipinbiaoqian"].ToString();
                if (!string.IsNullOrEmpty(shipinbiaoqian))
                {
                    if (shipinbiaoqian.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinbiaoqian", ConditionalType = ConditionalType.Like, FieldValue = shipinbiaoqian });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinbiaoqian", ConditionalType = ConditionalType.Equal, FieldValue = shipinbiaoqian });
                    }
                }
                var faburiqi = HttpContext.Request.Query["faburiqi"].ToString();
                if (!string.IsNullOrEmpty(faburiqi))
                {
                    if (faburiqi.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.Like, FieldValue = faburiqi });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.Equal, FieldValue = faburiqi });
                    }
                }

                foreach (var queryString in HttpContext.Request.Query)
                {
                    var key = queryString.Key;
                    var values = queryString.Value;
                    if (key != "vipread" && key != "page" && key != "limit" && key!="" && key!="sort" && key!="order" && !jujianList.Contains(key))
                    {
                        bool isContains = conModels.Any(model =>
                        {
                            return ((ConditionalModel)model).FieldName == key;
                        });
                        if (!isContains)
                        {
                            conModels.Add(new ConditionalModel() { FieldName = key, ConditionalType = ConditionalType.Equal, FieldValue = values });
                        }
                    }
                }
                return Json(_bll.GetPageList(page, limit, sort, order, conModels));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }


        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]

        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List(int page = 1, int limit = 10, string sort = "id", string order = "asc")
        {
            try
            {
                List<IConditionalModel> conModels = new List<IConditionalModel>();
                List<String> jujianList = new List<string>();
                jujianList.Add("faburiqistart");
                jujianList.Add("faburiqiend");
                var faburiqistart = HttpContext.Request.Query["faburiqistart"].ToString();
                if (!string.IsNullOrEmpty(faburiqistart))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.GreaterThanOrEqual, FieldValue = faburiqistart });
                }
                var faburiqiend = HttpContext.Request.Query["faburiqiend"].ToString();
                if (!string.IsNullOrEmpty(faburiqiend))
                {
                    conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.LessThanOrEqual, FieldValue = faburiqiend });
                }
                var shipinmingcheng = HttpContext.Request.Query["shipinmingcheng"].ToString();
				if (!string.IsNullOrEmpty(shipinmingcheng))
                {
                    if (shipinmingcheng.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinmingcheng", ConditionalType = ConditionalType.Like, FieldValue = shipinmingcheng });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinmingcheng", ConditionalType = ConditionalType.Equal, FieldValue = shipinmingcheng });
                    }
                }
                var shipinfenlei = HttpContext.Request.Query["shipinfenlei"].ToString();
				if (!string.IsNullOrEmpty(shipinfenlei))
                {
                    if (shipinfenlei.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinfenlei", ConditionalType = ConditionalType.Like, FieldValue = shipinfenlei });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinfenlei", ConditionalType = ConditionalType.Equal, FieldValue = shipinfenlei });
                    }
                }
                var shipinbiaoqian = HttpContext.Request.Query["shipinbiaoqian"].ToString();
				if (!string.IsNullOrEmpty(shipinbiaoqian))
                {
                    if (shipinbiaoqian.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinbiaoqian", ConditionalType = ConditionalType.Like, FieldValue = shipinbiaoqian });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "shipinbiaoqian", ConditionalType = ConditionalType.Equal, FieldValue = shipinbiaoqian });
                    }
                }
                var faburiqi = HttpContext.Request.Query["faburiqi"].ToString();
				if (!string.IsNullOrEmpty(faburiqi))
                {
                    if (faburiqi.Contains("%"))
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.Like, FieldValue = faburiqi });
                    }
                    else
                    {
                        conModels.Add(new ConditionalModel() { FieldName = "faburiqi", ConditionalType = ConditionalType.Equal, FieldValue = faburiqi });
                    }
                }

                Dictionary<string, string> filterPairs = new Dictionary<string, string>();
                foreach (var queryString in HttpContext.Request.Query)
                {
                    var key = queryString.Key;
                    var values = queryString.Value;
                    if (key != "vipread" && key != "page" && key != "limit" && key!="" && key!="sort" && key!="order" && !jujianList.Contains(key)&& typeof(ShipindianboDbModel).GetProperty(key)!=null)
                    {
                        var hasKey = false;
                        foreach (var model in conModels)  
                        {  
                            ConditionalModel conditionalModelClass = model as ConditionalModel;
                            if (conditionalModelClass!=null && conditionalModelClass.FieldName == key)  
                            {  
                                hasKey =true;  
                            }  
                        }
                        if (hasKey==false){
                            conModels.Add(new ConditionalModel() { FieldName = key, ConditionalType = ConditionalType.Equal, FieldValue = values });
                        }
                    }
                }
                return Json(_bll.GetPageList(page, limit, sort, order, conModels));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Save([FromBody] ShipindianboDbModel entity)
        {
            try
            {
                Random rd = new Random();
                int i = rd.Next(0, 1000000000);
                entity.Id = DateTime.Now.Ticks / 100000 + i;
                if (_bll.BaseInsert(entity) > 0)
                {
                    return Json(new { Code = 0, Msg = "添加成功！" });
                }

                return Json(new { Code = -1, Msg = "添加失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add([FromBody] ShipindianboDbModel entity)
        {
            try
            {

                Random rd = new Random();
                int i = rd.Next(0, 1000000000);
                entity.Id = DateTime.Now.Ticks / 100000 + i;
                if (_bll.BaseInsert(entity) > 0)
                {
                    return Json(new { Code = 0, Msg = "添加成功！" });
                }

                return Json(new { Code = -1, Msg = "添加失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 更新接口
        /// </summary>
        /// <param name="entity">更新实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Update([FromBody] ShipindianboDbModel entity)
        {

            try
            {
                if (_bll.BaseUpdate(entity))
                {
                    return Json(new { Code = 0, Msg = "编辑成功！" });
                }

                return Json(new { Code = -1, Msg = "编辑失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="ids">主键int[]</param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "Admin,Client")]
        public JsonResult Delete([FromBody] dynamic[] ids)
        {
            try
            {
                if (_bll.BaseDels(ids))
                {
                    return Json(new { Code = 0, Msg = "删除成功！" });
                }

                return Json(new { Code = -1, Msg = "删除失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 详情接口
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Info(long id)
        {
            try
            {
                _bll.IntelRecom(id);
                ShipindianboDbModel infoData = _bll.BaseGetById(id);
                return Json(new { Code = 0, Data = infoData });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 详情接口
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [HttpGet("{id}")]
        public JsonResult Detail(long id)
        {
            try
            {
                _bll.IntelRecom(id);
                ShipindianboDbModel detailData = _bll.BaseGetById(id);

                return Json(new { Code = 0, Data = detailData });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 赞、踩接口
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="type">type=1 赞；type=2 踩</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult Thumbsup(long id, int type)
        {
            try
            {
                if (_bll.Thumbsup(id, type) > 0)
                {
                    return Json(new { Code = 0, Msg = "操作成功！" });
                }
                return Json(new { Code = 500, Msg = "操作失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }
        


		/// <summary>
        /// 获取需要提醒的记录数接口
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="type">类型（1表示数字比较提醒，2表示日期比较提醒）</param>
        /// <param name="remindStart">remindStart小于等于columnName满足条件提醒,当比较日期时，该值表示天数</param>
        /// <param name="remindEnd">columnName小于等于remindEnd 满足条件提醒,当比较日期时，该值表示天数</param>
        /// <returns></returns>
        [HttpGet("{columnName}/{type}")]
        public JsonResult Remind(string columnName, int type, int remindStart=-1, int remindEnd=-1)
        {
            try
            {
                string where = "";
                return Json(new { Code = 0, Count = _bll.Common("shipindianbo", columnName, "", type, "remind", remindStart, remindEnd, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 智能推荐接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认desc）</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AutoSort(int page = 1, int limit = 5, string sort = "clicktime", string order = "desc")
        {
            try
            {
                sort = "browseduration";
            	List<IConditionalModel> conModels = new List<IConditionalModel>();
                Dictionary<string, string> filterPairs = new Dictionary<string, string>();
                foreach (var queryString in HttpContext.Request.Query)
                {
                    var key = queryString.Key;
                    var values = queryString.Value;
                    if (key != "vipread" && key != "page" && key != "limit" && key!="" && key!="sort" && key!="order")
                    {
                        bool isContains = conModels.Any(model =>
                        {
                            return ((ConditionalModel)model).FieldName == key;
                        });
                        if (!isContains)
                        {
                            conModels.Add(new ConditionalModel() { FieldName = key, ConditionalType = ConditionalType.Equal, FieldValue = values });
                        }
                    }
                }
                return Json(_bll.GetPageList(page, limit, sort, order, conModels));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }




        /// <summary>
        /// 按值统计接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <returns></returns>
        [HttpGet("{xColumnName}/{yColumnName}")]
        public JsonResult Value(string xColumnName, string yColumnName)
        {
            try
            {
                string where = " WHERE 1 = 1 ";
                return Json(new { Code = 0, Data = _bll.Common("shipindianbo", xColumnName, yColumnName, 0, "value", 0, 0, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 按时间统计类型接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <param name="timeStatType">类型</param>
        /// <returns></returns>
        [HttpGet("{xColumnName}/{yColumnName}/{timeStatType}")]
        public JsonResult Value(string xColumnName, string yColumnName, string timeStatType)
        {
            try
            {
                string where = " WHERE 1 = 1 ";
                string tableName = CacheHelper.TokenModel.Tablename;
                return Json(new { Code = 0, Data = _bll.StatDate("shipindianbo", xColumnName, yColumnName, timeStatType, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 按时间统计类型接口(多)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <returns></returns>
        [HttpGet("{xColumnName}")]
        public JsonResult valueMul(string xColumnName)
        {
            try
            {
                string yColumnNameMul = HttpContext.Request.Query["yColumnNameMul"].ToString();
                string where = " WHERE 1 = 1 ";
                // 创建一个 List 对象
                List<List<dynamic>> total = new List<List<dynamic>>();

                foreach (var item in yColumnNameMul.Split(","))
                {
                    List<dynamic> itemList = _bll.Common("shipindianbo", xColumnName, item, 0,"value",0,0, where);
                    // 创建一个 Dictionary 对象，表示数组中的每个元素
                    List<dynamic> element = new List<dynamic>();
                    foreach(var i in itemList)
                    {
                        element.Add(i);
                    }
                    total.Add(element);
                }

                return Json(new { Code = 0, Data = total });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 按时间统计类型接口(多)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <param name="timeStatType">类型</param>
        /// <returns></returns>
        [HttpGet("{xColumnName}/{timeStatType}")]
        public JsonResult valueMul(string xColumnName, string timeStatType)
        {
            try
            {
                string yColumnNameMul = HttpContext.Request.Query["yColumnNameMul"].ToString();
                string where = " WHERE 1 = 1 ";
                // 创建一个 List 对象
                List<List<dynamic>> total = new List<List<dynamic>>();

                foreach (var item in yColumnNameMul.Split(","))
                {
                    List<dynamic> itemList =  _bll.StatDate("shipindianbo", xColumnName, item, timeStatType, where);
                    // 创建一个 Dictionary 对象，表示数组中的每个元素
                    List<dynamic> element = new List<dynamic>();
                    foreach(var i in itemList)
                    {
                        element.Add(i);
                    }
                    total.Add(element);
                }

                return Json(new { Code = 0, Data = total });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 类别统计接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        [HttpGet("{columnName}")]
        public JsonResult Group(string columnName)
        {
            try
            {
                string where = " WHERE 1 = 1 ";
                return Json(new { Code = 0, Data = _bll.Common("shipindianbo", columnName, "", 0, "group", 0, 0, where) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }




        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Count(ShipindianboDbModel entity)
        {
            try
            {
                List<IConditionalModel> conModels = new List<IConditionalModel>();


                PageModel<ShipindianboDbModel> list = _bll.GetPageList(1, 1000, "id", "asc", conModels);
                return Json(new { Code = 0, Data = list.Data.List.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }





        /// <summary>
        /// 修改浏览时长
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [HttpPost("{id}")]
        public JsonResult UpdateBrowseDuration(long id)
        {
            try
            {
                int duration = Convert.ToInt32(HttpContext.Request.Query["duration"]);
                ShipindianboDbModel entity = _bll.BaseGetById(id);
                entity.Browseduration = entity.Browseduration + duration;

                _bll.BaseUpdate(entity);

                return Json(new { Code = 0 });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }








        /// <summary>
        /// 分页接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Lists()
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.BaseGetList(t => true) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }



    }
}
