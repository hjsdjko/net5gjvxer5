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
    public class ForumService : BaseService<ForumDbModel>
    {
        private readonly long _uid;
        private readonly string _role;
        private readonly string _tablename;

        public ForumService()
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






        public PageModel<ForumDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
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
            if (_role != "Admin")
            {
                conModels.Add(new ConditionalModel() { FieldName = "userid", ConditionalType = ConditionalType.Equal, FieldValue = _uid.ToString() });
            }

            List<ForumDbModel> ts = Db.Queryable<ForumDbModel>().Where(conModels).OrderBy(mysort).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<ForumDbModel> t = new PageModel<ForumDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ForumDbModel>()
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



        public PageModel<ForumDbModel> GetAllPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string[] sortFields = sort.Split(',');
            string[] orderFields = order.Split(',');
            var query = Db.Queryable<ForumDbModel>().Where(conModels);

            string mysort = "";
            for (int i = 0; i < sortFields.Length; i++)
            {
                if (i == sortFields.Length - 1)
                {
                    mysort += sortFields[i] + " " + orderFields[i];
                }else
                {
                    mysort += sortFields[i] + " " + orderFields[i] + ",";
                }
               
            }
            List<ForumDbModel> ts = query.OrderBy(mysort).ToPageList(page, limit, ref totalNumber, ref totalPage);
            PageModel<ForumDbModel> t = new PageModel<ForumDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ForumDbModel>()
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

        public ForumDbModel GetListByParentId(long id)
        {
            ForumDbModel forumDbModel = BaseGetById(id);
            List<ForumDbModel> treeNodes = new List<ForumDbModel>();

            //List<ForumDbModel> getAll = Db.Queryable<ForumDbModel>().Where(it => it.Parentid == id).ToList();
            List<ForumDbModel> getAll = Db.Queryable<ForumDbModel>().Where(it => it.Isdone == "开放" || it.Isdone == "" || it.Isdone == null).ToList();
            GetTreeNodes(getAll, id.ToString(), ref treeNodes);
            forumDbModel.Childs = treeNodes;
            return forumDbModel;
        }

        /// <summary>
        /// 获取组织结构树
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <param name="treeNodes"></param>
        /// <returns></returns>
        static void GetTreeNodes(List<ForumDbModel> list, string id, ref List<ForumDbModel> treeNodes)
        {
            if (list == null)
                return;
            List<ForumDbModel> sublist;
            if (!string.IsNullOrWhiteSpace(id))
            {
                sublist = list.Where(t => t.Parentid.ToString() == id).ToList();
            }
            else
            {
                sublist = list.Where(t => string.IsNullOrWhiteSpace(t.Parentid.ToString())).ToList();
            }
            foreach (var item in sublist)
            {
                treeNodes.Add(item);
                List<ForumDbModel> itemNodes = new List<ForumDbModel>();
                itemNodes = list.Where(t => t.Parentid == item.Id).ToList();
                item.Childs = itemNodes;
            }
            return;
        }





    }
}
