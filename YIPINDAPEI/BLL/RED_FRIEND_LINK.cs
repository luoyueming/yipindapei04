using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Model;
namespace NewXzc.BLL
{
    /// <summary>
    /// RED_FRIEND_LINK
    /// </summary>
    public partial class RED_FRIEND_LINK
    {
        private readonly NewXzc.DAL.RED_FRIEND_LINK dal = new NewXzc.DAL.RED_FRIEND_LINK();
        public RED_FRIEND_LINK()
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
        public bool Add(NewXzc.Model.RED_FRIEND_LINK model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NewXzc.Model.RED_FRIEND_LINK model)
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
        public NewXzc.Model.RED_FRIEND_LINK GetModel(int ID)
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
        public List<NewXzc.Model.RED_FRIEND_LINK> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.RED_FRIEND_LINK> DataTableToList(DataTable dt)
        {
            List<NewXzc.Model.RED_FRIEND_LINK> modelList = new List<NewXzc.Model.RED_FRIEND_LINK>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewXzc.Model.RED_FRIEND_LINK model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new NewXzc.Model.RED_FRIEND_LINK();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["LINK_NAME"] != null && dt.Rows[n]["LINK_NAME"].ToString() != "")
                    {
                        model.LINK_NAME = dt.Rows[n]["LINK_NAME"].ToString();
                    }
                    if (dt.Rows[n]["LINK_URL"] != null && dt.Rows[n]["LINK_URL"].ToString() != "")
                    {
                        model.LINK_URL = dt.Rows[n]["LINK_URL"].ToString();
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

