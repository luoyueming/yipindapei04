using System;
namespace NewXzc.Model
{
	/// <summary>
	/// RED_USER_LOGIN_RECORD:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RED_USER_LOGIN_RECORD
	{
		public RED_USER_LOGIN_RECORD()
		{}
		#region Model
		private int _id;
		private int? _userid;
		private DateTime? _login_time;
		private string _login_ip;
		private string _sessionid;
		private string _remark;
		private int? _login_type;
		/// <summary>
		/// 主键ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 登录人USERID
		/// </summary>
		public int? USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 登录时间
		/// </summary>
		public DateTime? Login_Time
		{
			set{ _login_time=value;}
			get{return _login_time;}
		}
		/// <summary>
		/// 登录IP
		/// </summary>
		public string Login_IP
		{
			set{ _login_ip=value;}
			get{return _login_ip;}
		}
		/// <summary>
		/// 当前登录用户的SessionID
		/// </summary>
		public string SessionID
		{
			set{ _sessionid=value;}
			get{return _sessionid;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 登录类型，0：邮箱；1：手机
		/// </summary>
		public int? Login_Type
		{
			set{ _login_type=value;}
			get{return _login_type;}
		}
		#endregion Model

	}
}

