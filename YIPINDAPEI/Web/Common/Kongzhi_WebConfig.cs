﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace NewXzc.Web.Common
{
    public class Kongzhi_WebConfig : IDisposable
    {
        private Configuration config;  
        public Kongzhi_WebConfig() : this(HttpContext.Current.Request.ApplicationPath)  
        {  
  
        }
        public Kongzhi_WebConfig(string path)  
        {  
            config = WebConfigurationManager.OpenWebConfiguration(path);  
        }


        ///   
        /// 设置应用程序配置节点,如果已经存在此节点,则会修改该节点的值,否则添加此节点  
        ///   
        /// 节点名称   
        /// 节点值   
        public void SetAppSetting(string key, string value)  
        {  
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");  
            if (appSetting.Settings[key] == null)//如果不存在此节点,则添加  
            {  
                appSetting.Settings.Add(key, value);  
            }  
            else//如果存在此节点,则修改  
            {  
                appSetting.Settings[key].Value = value;  
            }  
        }  
        ///   
        /// 设置数据库连接字符串节点,如果不存在此节点,则会添加此节点及对应的值,存在则修改  
        ///   
        /// 节点名称   
        /// 节点值   
        public void SetConnectionString(string key, string connectionString)  
        {  
            ConnectionStringsSection connectionSetting = (ConnectionStringsSection)config.GetSection("connectionStrings");  
            if (connectionSetting.ConnectionStrings[key] == null)//如果不存在此节点,则添加  
            {  
                ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings(key, connectionString);  
                connectionSetting.ConnectionStrings.Add(connectionStringSettings);  
            }  
            else//如果存在此节点,则修改  
            {  
                connectionSetting.ConnectionStrings[key].ConnectionString = connectionString;  
            }  
        }  
        ///   
        /// 保存所作的修改  
        ///   
        public void Save()  
        {  
            config.Save();  
            config = null;  
        }  
        public void Dispose()  
        {  
            if (config != null)  
            {  
                config.Save();  
            }  
        }  
    }



}