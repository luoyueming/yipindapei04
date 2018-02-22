using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.DBUtility;

namespace NewXzc.Web.Common
{
    public class FolderSizeHelper
    {

        /// <summary>
        /// 获取当前文件或文件夹的大小（单位B、KB、M、G）
        /// </summary>
        /// <param name="curtotalsize">当前文件的大小</param>
        /// <param name="totalfilesize">总文件的大小</param>
        /// <param name="type">1：获取文件的大小，2：获取文件在所有文件空间所占的百分比的具体数值</param>
        /// <returns></returns>
        public static string GetFile_Folder_Size(float curtotalsize, double totalfilesize,int type)
        {

            float kb = 1024;//换算变量

            double percentnum = 0;//所点百分比

            string kbnum = "0";

            //用户计算当前用户所拥有的空间换算成b后的具体值，初始化为2G
            totalfilesize = totalfilesize * 1024 * 1024 * 1024;


            if (curtotalsize < kb)//B  小于1024B，1KB
            {
                kbnum = Math.Round(curtotalsize, 2) + "B";
                percentnum = curtotalsize / totalfilesize;
            }
            else//KB   大于1KB
            {
                if (curtotalsize < kb * kb)//小于1M   1024*1024
                {
                    kbnum = Math.Round(curtotalsize / kb, 2).ToString() + "KB";
                    percentnum = curtotalsize / totalfilesize;
                }
                else//KB   大于1MB
                {
                    if (curtotalsize < kb * kb * kb)//小于1G   1024*1024*1204
                    {
                        kbnum = Math.Round(curtotalsize / (kb * kb), 2).ToString() + "M";
                        percentnum = curtotalsize / totalfilesize;
                    }
                    else
                    {
                        kbnum = Math.Round(curtotalsize / (kb * kb * kb), 2).ToString() + "G";
                        percentnum = curtotalsize / totalfilesize;
                    }
                }
            }

            percentnum = percentnum * 100;

            string now_unit_folder = kbnum;

            if (type == 2)
            {
                now_unit_folder = percentnum.ToString();
            }

            return now_unit_folder;
        }
    }
}