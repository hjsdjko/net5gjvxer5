using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models;
using Xiezn.Core.Models.DbModel;


namespace Xiezn.Core.Business.Services
{
    public class ShipindianboService : BaseService<ShipindianboDbModel>
    {
        private readonly long _uid;
        private readonly string _role;
        private readonly string _tablename;

        public ShipindianboService()
        {
            try
            {
                if (CacheHelper.TokenModel != null)
                {
                    _uid = CacheHelper.TokenModel.Uid;
                    _role = CacheHelper.TokenModel.Role;
                    _tablename = CacheHelper.TokenModel.Tablename;
                }
            }
            catch
            {
                _uid = 0;
                _role = "游客";
            }
        }


		public int Thumbsup(long id, int type)
        {
            ShipindianboDbModel updateObj = new ShipindianboDbModel();
            int res = 0;
            if (type == 1)
            {
                res = Db.Updateable(updateObj).UpdateColumns(it => new { it.Thumbsupnum }).ReSetValue(it => it.Thumbsupnum == (it.Thumbsupnum + 1)).Where(it => it.Id == id).ExecuteCommand();
            }

            if (type == 2)
            {
                res = Db.Updateable(updateObj).UpdateColumns(it => new { it.Crazilynum }).ReSetValue(it => it.Crazilynum == (it.Crazilynum + 1)).Where(it => it.Id == id).ExecuteCommand();
            }
            return res;
        }

        public int IntelRecom(long id)
        {
            ShipindianboDbModel updateObj = new ShipindianboDbModel();
            //return Db.Updateable(updateObj).UpdateColumns(it => new { it.Clicktime }).ReSetValue(it => it.Clicktime == (it.Clicktime + 1)).Where(it => it.Id == id).ExecuteCommand();
            return Db.Updateable(updateObj).UpdateColumns(it => new { it.Clicktime }).ReSetValue(it => it.Clicktime == DateTime.Now).Where(it => it.Id == id).ExecuteCommand();
        }



        public PageModel<ShipindianboDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;
            string[] sortFields = sort.Split(',');
            string[] orderFields = order.Split(',');
            string mysort = "";
            for (int i = 0; i < sortFields.Length; i++)
            {
                if (i == sortFields.Length - 1)
                {
                    mysort += sortFields[i] + " " + orderFields[i];
                }
                else
                {
                    mysort += sortFields[i] + " " + orderFields[i] + ",";

                }

            }
            List<ShipindianboDbModel> ts = Db.Queryable<ShipindianboDbModel>().Where(conModels).OrderBy(mysort).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<ShipindianboDbModel> t = new PageModel<ShipindianboDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ShipindianboDbModel>()
                {
                    Total = totalNumber,
                    PageSize = limit,
                    TotalPage = totalPage,
                    CurrPage = page,
                    List = ts
                }
            };

            return t;
        }








    }
}
