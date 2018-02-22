using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Model;
namespace NewXzc.BLL
{
    /// <summary>
    /// HRENH_ARTICLE_TYPE
    /// </summary>
    public partial class HRENH_ARTICLE_TYPE
    {
        private readonly NewXzc.DAL.HRENH_ARTICLE_TYPE dal = new NewXzc.DAL.HRENH_ARTICLE_TYPE();
        public HRENH_ARTICLE_TYPE()
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
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.HRENH_ARTICLE_TYPE model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NewXzc.Model.HRENH_ARTICLE_TYPE model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NewXzc.Model.HRENH_ARTICLE_TYPE GetModel(int ID)
        {

            return dal.GetModel(ID);
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
        public List<NewXzc.Model.HRENH_ARTICLE_TYPE> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.HRENH_ARTICLE_TYPE> DataTableToList(DataTable dt)
        {
            List<NewXzc.Model.HRENH_ARTICLE_TYPE> modelList = new List<NewXzc.Model.HRENH_ARTICLE_TYPE>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewXzc.Model.HRENH_ARTICLE_TYPE model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new NewXzc.Model.HRENH_ARTICLE_TYPE();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["TypeName"] != null && dt.Rows[n]["TypeName"].ToString() != "")
                    {
                        model.TypeName = dt.Rows[n]["TypeName"].ToString();
                    }
                    if (dt.Rows[n]["TypeOrder"] != null && dt.Rows[n]["TypeOrder"].ToString() != "")
                    {
                        model.TypeOrder = int.Parse(dt.Rows[n]["TypeOrder"].ToString());
                    }
                    if (dt.Rows[n]["PID"] != null && dt.Rows[n]["PID"].ToString() != "")
                    {
                        model.PID = int.Parse(dt.Rows[n]["PID"].ToString());
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["Addtime"] != null && dt.Rows[n]["Addtime"].ToString() != "")
                    {
                        model.Addtime = DateTime.Parse(dt.Rows[n]["Addtime"].ToString());
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
    }
}

