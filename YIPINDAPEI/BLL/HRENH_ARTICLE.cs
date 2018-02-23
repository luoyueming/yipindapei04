using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Model;
namespace NewXzc.BLL
{
    /// <summary>
    /// HRENH_ARTICLE
    /// </summary>
    public partial class HRENH_ARTICLE
    {
        private readonly NewXzc.DAL.HRENH_ARTICLE dal = new NewXzc.DAL.HRENH_ARTICLE();
        public HRENH_ARTICLE()
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
        public bool Add(NewXzc.Model.HRENH_ARTICLE model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NewXzc.Model.HRENH_ARTICLE model)
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
        public NewXzc.Model.HRENH_ARTICLE GetModel(int ID)
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
        public List<NewXzc.Model.HRENH_ARTICLE> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.HRENH_ARTICLE> DataTableToList(DataTable dt)
        {
            List<NewXzc.Model.HRENH_ARTICLE> modelList = new List<NewXzc.Model.HRENH_ARTICLE>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewXzc.Model.HRENH_ARTICLE model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new NewXzc.Model.HRENH_ARTICLE();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["TITLE"] != null && dt.Rows[n]["TITLE"].ToString() != "")
                    {
                        model.TITLE = dt.Rows[n]["TITLE"].ToString();
                    }
                    if (dt.Rows[n]["ISIMG"] != null && dt.Rows[n]["ISIMG"].ToString() != "")
                    {
                        model.ISIMG = int.Parse(dt.Rows[n]["ISIMG"].ToString());
                    }
                    if (dt.Rows[n]["IMG_URL"] != null && dt.Rows[n]["IMG_URL"].ToString() != "")
                    {
                        model.IMG_URL = dt.Rows[n]["IMG_URL"].ToString();
                    }
                    if (dt.Rows[n]["CONTENTS"] != null && dt.Rows[n]["CONTENTS"].ToString() != "")
                    {
                        model.CONTENTS = dt.Rows[n]["CONTENTS"].ToString();
                    }
                    if (dt.Rows[n]["TYPES"] != null && dt.Rows[n]["TYPES"].ToString() != "")
                    {
                        model.TYPES = int.Parse(dt.Rows[n]["TYPES"].ToString());
                    }
                    if (dt.Rows[n]["TYPES_PID"] != null && dt.Rows[n]["TYPES_PID"].ToString() != "")
                    {
                        model.TYPES_PID = int.Parse(dt.Rows[n]["TYPES_PID"].ToString());
                    }
                    if (dt.Rows[n]["SUBJECT_ID"] != null && dt.Rows[n]["SUBJECT_ID"].ToString() != "")
                    {
                        model.SUBJECT_ID = int.Parse(dt.Rows[n]["SUBJECT_ID"].ToString());
                    }
                    if (dt.Rows[n]["ZP_ZX_ID"] != null && dt.Rows[n]["ZP_ZX_ID"].ToString() != "")
                    {
                        model.ZP_ZX_ID = int.Parse(dt.Rows[n]["ZP_ZX_ID"].ToString());
                    }
                    if (dt.Rows[n]["READ_CNT"] != null && dt.Rows[n]["READ_CNT"].ToString() != "")
                    {
                        model.READ_CNT = int.Parse(dt.Rows[n]["READ_CNT"].ToString());
                    }
                    if (dt.Rows[n]["FALSH_READ_CNT"] != null && dt.Rows[n]["FALSH_READ_CNT"].ToString() != "")
                    {
                        model.FALSH_READ_CNT = int.Parse(dt.Rows[n]["FALSH_READ_CNT"].ToString());
                    }
                    if (dt.Rows[n]["ISHOT"] != null && dt.Rows[n]["ISHOT"].ToString() != "")
                    {
                        model.ISHOT = int.Parse(dt.Rows[n]["ISHOT"].ToString());
                    }
                    if (dt.Rows[n]["ISEND"] != null && dt.Rows[n]["ISEND"].ToString() != "")
                    {
                        model.ISEND = int.Parse(dt.Rows[n]["ISEND"].ToString());
                    }
                    if (dt.Rows[n]["EDITTIME"] != null && dt.Rows[n]["EDITTIME"].ToString() != "")
                    {
                        model.EDITTIME = DateTime.Parse(dt.Rows[n]["EDITTIME"].ToString());
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

