using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Common;
using NewXzc.Model;
namespace NewXzc.BLL
{
	/// <summary>
	/// RED_USER
	/// </summary>
	public partial class RED_USER
	{
		private readonly NewXzc.DAL.RED_USER dal=new NewXzc.DAL.RED_USER();
		public RED_USER()
		{}

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="USERID">用户USERID</param>
        /// <returns></returns>
        public bool Exists(int USERID)
        {
            return dal.Exists(USERID);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.RED_USER model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model">用户信息Model</param>
        /// <param name="type">执行操作类型，0：全部修改，1：修改用户头像和昵称，2：修改密码</param>
        /// <returns></returns>
        public bool Update(NewXzc.Model.RED_USER model, int type = 0)
        {
            return dal.Update(model,type);
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
        /// <param name="USERID">用户USERID</param>
        /// <returns></returns>
        public NewXzc.Model.RED_USER GetModel(int USERID)
        {
            return dal.GetModel(USERID);
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
        public List<NewXzc.Model.RED_USER> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<NewXzc.Model.RED_USER> DataTableToList(DataTable dt)
        {
            List<NewXzc.Model.RED_USER> modelList = new List<NewXzc.Model.RED_USER>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewXzc.Model.RED_USER model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new NewXzc.Model.RED_USER();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["USERID"] != null && dt.Rows[n]["USERID"].ToString() != "")
                    {
                        model.USERID = int.Parse(dt.Rows[n]["USERID"].ToString());
                    }
                    if (dt.Rows[n]["USERNAME"] != null && dt.Rows[n]["USERNAME"].ToString() != "")
                    {
                        model.USERNAME = dt.Rows[n]["USERNAME"].ToString();
                    }
                    if (dt.Rows[n]["PWD"] != null && dt.Rows[n]["PWD"].ToString() != "")
                    {
                        model.PWD = dt.Rows[n]["PWD"].ToString();
                    }
                    if (dt.Rows[n]["TEL"] != null && dt.Rows[n]["TEL"].ToString() != "")
                    {
                        model.TEL = dt.Rows[n]["TEL"].ToString();
                    }
                    if (dt.Rows[n]["TEL_STATE"] != null && dt.Rows[n]["TEL_STATE"].ToString() != "")
                    {
                        model.TEL_STATE = int.Parse(dt.Rows[n]["TEL_STATE"].ToString());
                    }
                    if (dt.Rows[n]["USER_HEAD"] != null && dt.Rows[n]["USER_HEAD"].ToString() != "")
                    {
                        model.USER_HEAD = dt.Rows[n]["USER_HEAD"].ToString();
                    }
                    if (dt.Rows[n]["SEX"] != null && dt.Rows[n]["SEX"].ToString() != "")
                    {
                        model.SEX = int.Parse(dt.Rows[n]["SEX"].ToString());
                    }
                    if (dt.Rows[n]["EMAIL"] != null && dt.Rows[n]["EMAIL"].ToString() != "")
                    {
                        model.EMAIL = dt.Rows[n]["EMAIL"].ToString();
                    }
                    if (dt.Rows[n]["USER_TYPE"] != null && dt.Rows[n]["USER_TYPE"].ToString() != "")
                    {
                        model.USER_TYPE = int.Parse(dt.Rows[n]["USER_TYPE"].ToString());
                    }
                    if (dt.Rows[n]["ISCOMPLETE"] != null && dt.Rows[n]["ISCOMPLETE"].ToString() != "")
                    {
                        model.ISCOMPLETE = int.Parse(dt.Rows[n]["ISCOMPLETE"].ToString());
                    }
                    if (dt.Rows[n]["PROVINCE"] != null && dt.Rows[n]["PROVINCE"].ToString() != "")
                    {
                        model.PROVINCE = dt.Rows[n]["PROVINCE"].ToString();
                    }
                    if (dt.Rows[n]["CITY"] != null && dt.Rows[n]["CITY"].ToString() != "")
                    {
                        model.CITY = dt.Rows[n]["CITY"].ToString();
                    }
                    if (dt.Rows[n]["AREA"] != null && dt.Rows[n]["AREA"].ToString() != "")
                    {
                        model.AREA = dt.Rows[n]["AREA"].ToString();
                    }
                    if (dt.Rows[n]["PEOPLE_IDENTITY"] != null && dt.Rows[n]["PEOPLE_IDENTITY"].ToString() != "")
                    {
                        model.PEOPLE_IDENTITY = dt.Rows[n]["PEOPLE_IDENTITY"].ToString();
                    }
                    if (dt.Rows[n]["OCCUPATION"] != null && dt.Rows[n]["OCCUPATION"].ToString() != "")
                    {
                        model.OCCUPATION = dt.Rows[n]["OCCUPATION"].ToString();
                    }
                    if (dt.Rows[n]["PERSONALITY"] != null && dt.Rows[n]["PERSONALITY"].ToString() != "")
                    {
                        model.PERSONALITY = dt.Rows[n]["PERSONALITY"].ToString();
                    }
                    if (dt.Rows[n]["INTRODUCE"] != null && dt.Rows[n]["INTRODUCE"].ToString() != "")
                    {
                        model.INTRODUCE = dt.Rows[n]["INTRODUCE"].ToString();
                    }
                    if (dt.Rows[n]["IDENTIFICATION_STATE"] != null && dt.Rows[n]["IDENTIFICATION_STATE"].ToString() != "")
                    {
                        model.IDENTIFICATION_STATE = int.Parse(dt.Rows[n]["IDENTIFICATION_STATE"].ToString());
                    }
                    if (dt.Rows[n]["SAME_HOBBY_PEOPLE"] != null && dt.Rows[n]["SAME_HOBBY_PEOPLE"].ToString() != "")
                    {
                        model.SAME_HOBBY_PEOPLE = dt.Rows[n]["SAME_HOBBY_PEOPLE"].ToString();
                    }
                    if (dt.Rows[n]["EXP"] != null && dt.Rows[n]["EXP"].ToString() != "")
                    {
                        model.EXP = int.Parse(dt.Rows[n]["EXP"].ToString());
                    }
                    if (dt.Rows[n]["SCORE"] != null && dt.Rows[n]["SCORE"].ToString() != "")
                    {
                        model.SCORE = int.Parse(dt.Rows[n]["SCORE"].ToString());
                    }
                    if (dt.Rows[n]["REALM_NAME"] != null && dt.Rows[n]["REALM_NAME"].ToString() != "")
                    {
                        model.REALM_NAME = dt.Rows[n]["REALM_NAME"].ToString();
                    }
                    if (dt.Rows[n]["ADDTIME"] != null && dt.Rows[n]["ADDTIME"].ToString() != "")
                    {
                        model.ADDTIME = DateTime.Parse(dt.Rows[n]["ADDTIME"].ToString());
                    }
                    if (dt.Rows[n]["IS_RED"] != null && dt.Rows[n]["IS_RED"].ToString() != "")
                    {
                        model.IS_RED = int.Parse(dt.Rows[n]["IS_RED"].ToString());
                    }
                    if (dt.Rows[n]["STATE"] != null && dt.Rows[n]["STATE"].ToString() != "")
                    {
                        model.STATE = int.Parse(dt.Rows[n]["STATE"].ToString());
                    }
                    if (dt.Rows[n]["REMARK"] != null && dt.Rows[n]["REMARK"].ToString() != "")
                    {
                        model.REMARK = dt.Rows[n]["REMARK"].ToString();
                    }
                    if (dt.Rows[n]["Person_Desc"] != null && dt.Rows[n]["Person_Desc"].ToString() != "")
                    {
                        model.Person_Desc = dt.Rows[n]["Person_Desc"].ToString();
                    }
                    if (dt.Rows[n]["Person_NickName"] != null && dt.Rows[n]["Person_NickName"].ToString() != "")
                    {
                        model.Person_NickName = dt.Rows[n]["Person_NickName"].ToString();
                    }
                    if (dt.Rows[n]["Porder"] != null && dt.Rows[n]["Porder"].ToString() != "")
                    {
                        model.Porder = int.Parse(dt.Rows[n]["Porder"].ToString());
                    }
                    if (dt.Rows[n]["UpdateTime"] != null && dt.Rows[n]["UpdateTime"].ToString() != "")
                    {
                        model.UpdateTime = DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["PLAT_VAL"] != null && dt.Rows[n]["PLAT_VAL"].ToString() != "")
                    {
                        model.PLAT_VAL = dt.Rows[n]["PLAT_VAL"].ToString();
                    }
                    if (dt.Rows[n]["SPECIALTY_VAL"] != null && dt.Rows[n]["SPECIALTY_VAL"].ToString() != "")
                    {
                        model.SPECIALTY_VAL = dt.Rows[n]["SPECIALTY_VAL"].ToString();
                    }
                    if (dt.Rows[n]["IPURL"] != null && dt.Rows[n]["IPURL"].ToString() != "")
                    {
                        model.IPURL = dt.Rows[n]["IPURL"].ToString();
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

        #region 更新用户头像
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="tel">用户手机号</param>
        /// <param name="logoPath">更新后的头像的路径</param>
        /// <returns></returns>
        public bool UpdateLogo(string tel, string logoPath)
        {
            return dal.UpdateLogo(tel, logoPath);
        }
        #endregion
	}
}

