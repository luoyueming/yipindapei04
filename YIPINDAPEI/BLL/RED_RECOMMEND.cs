using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Model;
namespace NewXzc.BLL
{
    /// <summary>
    /// RED_RECOMMEND
    /// </summary>
    public partial class RED_RECOMMEND
    {
        private readonly NewXzc.DAL.RED_RECOMMEND dal = new NewXzc.DAL.RED_RECOMMEND();
        public RED_RECOMMEND()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.RED_RECOMMEND model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NewXzc.Model.RED_RECOMMEND model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NewXzc.Model.RED_RECOMMEND GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.RED_RECOMMEND> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.RED_RECOMMEND> DataTableToList(DataTable dt)
        {
            List<NewXzc.Model.RED_RECOMMEND> modelList = new List<NewXzc.Model.RED_RECOMMEND>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewXzc.Model.RED_RECOMMEND model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new NewXzc.Model.RED_RECOMMEND();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["PageId"] != null && dt.Rows[n]["PageId"].ToString() != "")
                    {
                        model.PageId = int.Parse(dt.Rows[n]["PageId"].ToString());
                    }
                    if (dt.Rows[n]["Cid"] != null && dt.Rows[n]["Cid"].ToString() != "")
                    {
                        model.Cid = int.Parse(dt.Rows[n]["Cid"].ToString());
                    }
                    if (dt.Rows[n]["Sort"] != null && dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    if (dt.Rows[n]["Url"] != null && dt.Rows[n]["Url"].ToString() != "")
                    {
                        model.Url = dt.Rows[n]["Url"].ToString();
                    }
                    if (dt.Rows[n]["V1"] != null && dt.Rows[n]["V1"].ToString() != "")
                    {
                        model.V1 = dt.Rows[n]["V1"].ToString();
                    }
                    if (dt.Rows[n]["V2"] != null && dt.Rows[n]["V2"].ToString() != "")
                    {
                        model.V2 = dt.Rows[n]["V2"].ToString();
                    }
                    if (dt.Rows[n]["V3"] != null && dt.Rows[n]["V3"].ToString() != "")
                    {
                        model.V3 = dt.Rows[n]["V3"].ToString();
                    }
                    if (dt.Rows[n]["v4"] != null && dt.Rows[n]["v4"].ToString() != "")
                    {
                        model.v4 = dt.Rows[n]["v4"].ToString();
                    }
                    if (dt.Rows[n]["AddTime"] != null && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        #region  推荐操作  宋明亮 2014-12-05
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">获取指定的数目，可为0</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataSet GetList_NewIndex(int top, string strWhere)
        {
            return dal.GetList_NewIndex(top, strWhere);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="top">获取指定的数目，可为0</param>
        /// <param name="tabname">需要被查询的表</param>
        /// <param name="coloum">需要显示哪些列，此处是列名，如（,username,user_head），提示：此处的列名已有一部分固定PageId,Cid,Sort,Url,V1,V2,V3,isnull(V4,'') as V4，只要将需要显示的其他的列名在其后加","显示即可</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataSet GetList_NewIndex(int top, string tabname, string coloum, string strWhere)
        {
            return dal.GetList_NewIndex(top, strWhere, coloum, strWhere);
        }

        #endregion
    }
}

