using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Model;
namespace NewXzc.BLL
{
    /// <summary>
    /// HRENH_ARTICLE_REVIEW_REPLY
    /// </summary>
    public partial class HRENH_ARTICLE_REVIEW_REPLY
    {
        private readonly NewXzc.DAL.HRENH_ARTICLE_REVIEW_REPLY dal = new NewXzc.DAL.HRENH_ARTICLE_REVIEW_REPLY();
        public HRENH_ARTICLE_REVIEW_REPLY()
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
        public bool Add(NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY model)
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
        public NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY GetModel(int ID)
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
        public List<NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY> DataTableToList(DataTable dt)
        {
            List<NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY> modelList = new List<NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["USERID"] != null && dt.Rows[n]["USERID"].ToString() != "")
                    {
                        model.USERID = int.Parse(dt.Rows[n]["USERID"].ToString());
                    }
                    if (dt.Rows[n]["REPLY_ID"] != null && dt.Rows[n]["REPLY_ID"].ToString() != "")
                    {
                        model.REPLY_ID = int.Parse(dt.Rows[n]["REPLY_ID"].ToString());
                    }
                    if (dt.Rows[n]["PUB_USERID"] != null && dt.Rows[n]["PUB_USERID"].ToString() != "")
                    {
                        model.PUB_USERID = int.Parse(dt.Rows[n]["PUB_USERID"].ToString());
                    }
                    if (dt.Rows[n]["REVIEW_PID"] != null && dt.Rows[n]["REVIEW_PID"].ToString() != "")
                    {
                        model.REVIEW_PID = int.Parse(dt.Rows[n]["REVIEW_PID"].ToString());
                    }
                    if (dt.Rows[n]["CONTENTS"] != null && dt.Rows[n]["CONTENTS"].ToString() != "")
                    {
                        model.CONTENTS = dt.Rows[n]["CONTENTS"].ToString();
                    }
                    if (dt.Rows[n]["ISLIKE"] != null && dt.Rows[n]["ISLIKE"].ToString() != "")
                    {
                        model.ISLIKE = int.Parse(dt.Rows[n]["ISLIKE"].ToString());
                    }
                    if (dt.Rows[n]["TYPES"] != null && dt.Rows[n]["TYPES"].ToString() != "")
                    {
                        model.TYPES = int.Parse(dt.Rows[n]["TYPES"].ToString());
                    }
                    if (dt.Rows[n]["ARTICLE_ID"] != null && dt.Rows[n]["ARTICLE_ID"].ToString() != "")
                    {
                        model.ARTICLE_ID = int.Parse(dt.Rows[n]["ARTICLE_ID"].ToString());
                    }
                    if (dt.Rows[n]["ADDTIME"] != null && dt.Rows[n]["ADDTIME"].ToString() != "")
                    {
                        model.ADDTIME = DateTime.Parse(dt.Rows[n]["ADDTIME"].ToString());
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

