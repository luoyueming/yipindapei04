using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Collections;

namespace NewXzc.Common
{
    public class companyPage
    {
        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page)
        {

            string FunctionName = "GoPage";
            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append("<div class='page_list clearfix'>");
                //当前页等于1时和不等于1时
                if (page == 1)
                {
                    pageHtml.Append("<a class='unc'>首页</a>");
                }
                else
                {
                    pageHtml.Append("<a class='unc' href='javascript:void(0);' onclick='" + FunctionName + "(1)'>首页</a>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a>");
                    pageHtml.Append("..");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a>");
                    pageHtml.Append("..");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }

                //当前页
                pageHtml.Append("<a class='num current'>" + page + "</a>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }

                    pageHtml.Append("..");
                    pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }

                    pageHtml.Append("..");
                    pageHtml.Append("<a class='num' href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a>");
                }
                

                //当前页等于MaxPage时和不等于MaxPage时
                if (page == MaxPage)
                {
                    pageHtml.Append("<a class='unc'>尾页</a>");
                }
                else
                {
                    pageHtml.Append("<a class='unc' href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>尾页</a>");
                }

                pageHtml.Append("<input type='text' onkeyup=\"this.value=this.value.replace(/\\D/g,'')\" id=\"pageNum\" value=\"" + page + "\">");
                pageHtml.Append("<a class=\"unc go\" href=\"javascript:void(0)\" onclick=\"GoPage(document.getElementById('pageNum').value);\">跳转</a><span id='Span2'>/" + MaxPage + "页</span>");
                if (page > 1)
                {
                    pageHtml.Append("<a class=\"pn unc prev\" title=\"上一页\" href=\"javascript:void(0)\" onclick='" + FunctionName + "(" + (page - 1) + ")'>上一页</a>");
                }
                else
                {
                    pageHtml.Append("<a class=\"pn unc prev\" title=\"上一页\" href=\"javascript:void(0)\">上一页</a>");
                }
                if (page < MaxPage)
                {
                    pageHtml.Append("<a class=\"pn unc next\" title=\"下一页\" href=\"javascript:void(0)\" onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a>");
                }
                else
                {
                    pageHtml.Append("<a class=\"pn unc next\" title=\"下一页\" href=\"javascript:void(0)\">下一页</a>");
                }

                pageHtml.Append("</div>");
            }
            return pageHtml.ToString();
        }
        #endregion
    }

    public class GenerPage
    {
        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page, string FunctionName = "GetList_page")
        {
            if (page < 1)
            {
                page = 1;
            }

            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage>1)
            {
                pageHtml.Append("<div class=\"page_list clearfix\">");
                pageHtml.Append("<ul>");
                //当前页等于1时和不等于1时
                if (page == 1)
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\">上一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page - 1) + ")'>上一页</a></li>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li><a class=\"page_list_current\">" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                

                //当前页等于MaxPage时和不等于MaxPage时
                if (page == MaxPage)
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\">下一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a></li>");
                }
                pageHtml.Append("<li style='display:none' id='Span2'>" + MaxPage + "</li>");
                pageHtml.Append("</ul>");

                //pageHtml.Append("<p>跳至");
                //pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
                //pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(document.getElementById('pageNum').value);\">跳转</a></p>");

                pageHtml.Append("</div>");
            }
            return pageHtml.ToString();


        }
        #endregion

        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <param name="FunctionName">方法名称</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page, string FunctionName,int num,int interview_id)
        {
            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append("<div class=\"page_list clearfix\">");
                pageHtml.Append("<ul>");
                //当前页等于1时和不等于1时
                if (page == 1)
                {
                    pageHtml.Append("<li><a class=\"N-commonNext_New\">上一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a class=\"N-commonNext_New\" href='javascript:void(0);' onclick='" + FunctionName + "("+num+","+interview_id+"," + (page - 1) + ")'>上一页</a></li>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + ",1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + ",1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li><a class=\"page_list_current_new\">" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + MaxPage + ")'>" + MaxPage + "</a></li>");
                }


                //当前页等于MaxPage时和不等于MaxPage时
                if (page == MaxPage)
                {
                    pageHtml.Append("<li><a class=\"N-commonNext_New\">下一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a class=\"N-commonNext_New\" href='javascript:void(0);' onclick='" + FunctionName + "(" + num + "," + interview_id + "," + (page + 1) + ")'>下一页</a></li>");
                }
                pageHtml.Append("<li style='display:none' id='Span3'>" + MaxPage + "</li>");
                pageHtml.Append("</ul>");

                //pageHtml.Append("<p>跳至");
                //pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
                //pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(document.getElementById('pageNum').value);\">跳转</a></p>");

                pageHtml.Append("</div>");
            }
            return pageHtml.ToString();


        }
        #endregion
    }

    public class LimitlePage
    {
        #region 无限分页假数据+pageHtml(int page)
        /// <summary>
        /// 无限分页假数据+pageHtml(int page)
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int page)
        {
            // 1 2 3 4 5 6 7 8 9
            string FunctionName = "GoPage";

            StringBuilder pageHtml = new StringBuilder("");
            pageHtml.Append("<div class=\"page_list clearfix\">");
            pageHtml.Append("<ul>");
            //当前页等于1时和不等于1时
            if (page == 1)
            {
                pageHtml.Append("<li><a>首页</a></li>");
            }
            else
            {
                pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>首页</a></li>");
            }

            if (page < 5)
            {
                for (int i = 1; i < page; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }
            else 
            {
                for (int i = page - 4; i < page; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }
            //当前页
            pageHtml.Append("<li><a class=\"page_list_current\">" + page + "</a></li>");

            if (page < 5)
            {
                for (int i = page+1; i <= 9; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }
            else
            {
                for (int i = page +1; i <= page+4; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }


            pageHtml.Append("<li style='display:none' id='Span2'>100000</li>");
            pageHtml.Append("</ul>");

            pageHtml.Append("<p>跳至");
            pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
            pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(document.getElementById('pageNum').value);\">跳转</a></p>");
            pageHtml.Append("</div>");
            

            return pageHtml.ToString();
        }
       #endregion
    }

    public class LimitlePage1
    {
        #region 无限分页假数据+pageHtml(int page)
        /// <summary>
        /// 无限分页假数据+pageHtml(int page)
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int page)
        {
            // 1 2 3 4 5 6 7
            string FunctionName = "GoPage";

            StringBuilder pageHtml = new StringBuilder("");
            pageHtml.Append("<div class=\"page_list clearfix\">");
            pageHtml.Append("<ul>");
            //当前页等于1时和不等于1时
            if (page == 1)
            {
                pageHtml.Append("<li><a>上一页</a></li>");
            }
            else if (page > 1)
            {
                pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + (page - 1) + ")'>上一页</a></li>");
            }

            if (page < 4)
            {
                for (int i = 1; i < page; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }
            else
            {
                for (int i = page - 3; i < page; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }
            //当前页
            pageHtml.Append("<li><a class=\"page_list_current\">" + page + "</a></li>");

            if (page < 4)
            {
                for (int i = page + 1; i <= 7; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }
            else
            {
                for (int i = page + 1; i <= page + 3; i++)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                }
            }

            if (page >=10000)
            {
                pageHtml.Append("<li><a>下一页</a></li>");
            }
            else if (page < 10000)
            {
                pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a></li>");
            }

            //pageHtml.Append("<li style='display:none' id='Span2'>100000</li>");
            pageHtml.Append("</ul>");

            //pageHtml.Append("<p>跳至");
            //pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
            //pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(document.getElementById('pageNum').value);\">跳转</a></p>");
            pageHtml.Append("</div>");


            return pageHtml.ToString();
        }
        #endregion
    }



    public class ReplyPage
    {
        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page)
        {

            string FunctionName = "OsPageGo";
            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append("<div class=\"page_list clearfix\">");
                pageHtml.Append("<ul>");
                //当前页等于1时和不等于1时
                if (page == 1)
                {
                    pageHtml.Append("<li><a>首页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this,1)'>首页</a></li>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this,1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this,1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li><a class=\"page_list_current\">" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + MaxPage + ")'>" + MaxPage + "</a></li>");
                }


                //当前页等于MaxPage时和不等于MaxPage时
                if (page == MaxPage)
                {
                    pageHtml.Append("<li><a>尾页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + MaxPage + ")'>尾页</a></li>");
                }
                pageHtml.Append("<li style='display:none' id='Span2'>" + MaxPage + "</li>");
                pageHtml.Append("</ul>");

                pageHtml.Append("<p>跳至");
                pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
                pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(this,document.getElementById('pageNum').value);\">跳转</a></p>");

                pageHtml.Append("</div>");
            }
            return pageHtml.ToString();


        }
        #endregion
    }

    public class AdMsgPage
    {
        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page)
        {

            string FunctionName = "GoPage";
            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append(" <div><ul>");
                //当前页等于1时和不等于1时
                //if (page == 1)
                //{
                //    pageHtml.Append("<li  class='cur'><a href='javascript:;'>1</a></li>");
                //}
                //else
                //{
                //    pageHtml.Append("<li><a href='javascript:;' onclick='" + FunctionName + "(1)'>1</a></li>");
                //}


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:;' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("..");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("..");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li class='cur'><a>" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("..");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    //pageHtml.Append("..");
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }

                if (page < MaxPage)
                {
                    pageHtml.Append("<li class='down'><a title=\"下一页\" href=\"javascript:void(0)\" onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li class='down'><a class=\"pn unc next\" title=\"下一页\" href=\"javascript:void(0)\">下一页</a></li>");
                }

                pageHtml.Append("</ul></div>");
            }
            return pageHtml.ToString();
        }
        #endregion
    }

    public class AdPeople
    {
        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page,string FunctionName="PageGo")
        {

            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append(" <ul class=\"awp_ul2\">");
                pageHtml.AppendFormat("<li>共{0}条记录</li>", count);
                //当前页等于1时和不等于1时
                if(page>1)
                {
                    pageHtml.Append(" <li><a href=\"javascript:void(0);\" onclick='" + FunctionName + "(" + (page - 1) + ")'><<上一页</a></li>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li class='cur'><a >" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }


                if (page < MaxPage)
                {
                    pageHtml.Append("<li><a  class=\"next\"  title=\"下一页\" href=\"javascript:void(0)\" onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页>></a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a  class=\"next\"  title=\"下一页\" href=\"javascript:void(0)\">下一页>></a></li>");
                }

                pageHtml.Append("</ul>");
            }
            return pageHtml.ToString();
        }

        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml_New(int count, int val, int page, int page_size_sel)
        {

            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            string FunctionName = "PageGo";

            StringBuilder pageHtml = new StringBuilder("");

            ArrayList arr = new ArrayList();
            arr.Add(10);
            arr.Add(20);
            arr.Add(50);
            arr.Add(100);

            pageHtml.Append("<div class=\"awp_div\">");

            pageHtml.Append("<ul class=\"awp_ul\">");
            pageHtml.Append("<li>显示</li>");
            pageHtml.Append("<li class=\"aw_input\">");
            pageHtml.Append("<span class=\"selectWrap awp_page\">");
            pageHtml.Append("<select id=\"sel_page\">");
            for (int k = 0; k < arr.Count; k++)
            {
                if (arr[k].ToString() == page_size_sel.ToString())
                {
                    pageHtml.AppendFormat("<option value=\"{0}\" selected=\"selected\">{0}</option>",arr[k].ToString());
                }
                else
                {
                    pageHtml.AppendFormat("<option value=\"{0}\">{0}</option>", arr[k].ToString());
                }
            }
            pageHtml.Append("</select>");
            pageHtml.Append("</span>");
            pageHtml.Append("</li>");
            pageHtml.Append("<li>条/页</li>");
            pageHtml.Append("</ul>");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                

                pageHtml.Append(" <ul class=\"awp_ul2\">");
                pageHtml.AppendFormat("<li>共{0}条记录</li>", count);
                //当前页等于1时和不等于1时
                if (page > 1)
                {
                    pageHtml.Append(" <li><a href=\"javascript:void(0);\" onclick='" + FunctionName + "(" + (page - 1) + ")'><<上一页</a></li>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li class='cur'><a >" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a href='javascript:void(0);'>..</a></li>");
                    pageHtml.Append("<li><a  href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }


                if (page < MaxPage)
                {
                    pageHtml.Append("<li class=\"next\"><a  class=\"next\"  title=\"下一页\" href=\"javascript:void(0)\" onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页>></a></li>");
                }
                else
                {
                    pageHtml.Append("<li class=\"next\"><a  class=\"next\"  title=\"下一页\" href=\"javascript:void(0)\">下一页>></a></li>");
                }

                pageHtml.Append("</ul>");

                
            }

            pageHtml.Append("</div>");

            return pageHtml.ToString();
        }
        #endregion
    }


    public class NewArticlePageHtml
    {
        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="count">数据总数</param>
        /// <param name="val">每页显示数量</param>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public static string pageHtml(int count, int val, int page)
        {

            string FunctionName = "GoPage";
            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append("<div class=\"pagination clearfix\">");
                //当前页等于1时和不等于1时
                if (page == 1)
                {
                    pageHtml.Append("<a href=\"javascript:void(0);\" class=\"pages prev\">上一页</a>");
                }
                else
                {
                    pageHtml.Append("<a class=\"pages prev\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page - 1) + ")'>上一页</a>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a>");
                    pageHtml.Append("<a  class=\"more\">...</a>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a>");
                    pageHtml.Append("<a class=\"more\">...</a>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                }

                //当前页
                pageHtml.Append("<a class=\"cur\">" + page + "</a>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }

                    pageHtml.Append("<a class=\"more\">...</a>");
                    pageHtml.Append("<a class=\"pages next\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }
                    pageHtml.Append("<a class=\"pages next\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a>");
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a>");
                    }

                    pageHtml.Append("<a class=\"more\">...</a>");
                    pageHtml.Append("<a class=\"pages next\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a>");
                }
               

                ////当前页等于MaxPage时和不等于MaxPage时
                //if (page == MaxPage)
                //{
                //    pageHtml.Append("<li><a>尾页</a></li>");
                //}
                //else
                //{
                //    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(this," + MaxPage + ")'>尾页</a></li>");
                //}
                //pageHtml.Append("<li style='display:none' id='Span2'>" + MaxPage + "</li>");
                //pageHtml.Append("</ul>");

                //pageHtml.Append("<p>跳至");
                //pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
                //pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(this,document.getElementById('pageNum').value);\">跳转</a></p>");

                pageHtml.Append("</div>");
            }
            return pageHtml.ToString();


        }
        #endregion


        #region 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// <summary>
        /// 分页样式（类似博客园分页）+pageHtml(int count, int val,int page,string FunctionName)
        /// </summary>
        /// <param name="record_cnt">数据总数</param>
        /// <param name="pagesize">每页显示数量</param>
        /// <param name="curpage">当前页页码</param>
        /// <param name="pageurl">文章页码地址</param>
        /// <returns></returns>
        public static string pageHtml_CreateHtml(int record_cnt, int pagesize, int curpage)
        {

            string FunctionName = "GoPage";

            int count = record_cnt;
            int val = pagesize;
            int page = curpage;

            if (page < 1)
            {
                page = 1;
            }

            int midNum = 5;//初始时显示到第几页
            int MaxPage = 0;//最大的页数

            if (count % val != 0)//页数
            {
                MaxPage = count / val + 1;
            }
            else
            {
                MaxPage = count / val;
            }

            if (page > MaxPage)
            {
                page = MaxPage;
            }

            StringBuilder pageHtml = new StringBuilder("");

            /***********************************************************************
             *分页中当前页码的前半部分显示情况
             ***********************************************************************/
            if (count > 0 && MaxPage > 1)
            {
                pageHtml.Append("<div class=\"page_list clearfix\">");
                pageHtml.Append("<ul>");
                //当前页等于1时和不等于1时
                if (page == 1)
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\">上一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page - 1) + ")'>上一页</a></li>");
                }


                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    for (int i = page - 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage - page < 4 && MaxPage >= (midNum + 2))
                {
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(1)'>1</a></li>");
                    pageHtml.Append("<li><a>...</a></li>");
                    int leftBegin = page - (midNum - 1 - (MaxPage - page));
                    for (int i = leftBegin; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (MaxPage <= 7 || (page <= 4 && MaxPage > 7))
                {
                    for (int i = 1; i < page; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }

                //当前页
                pageHtml.Append("<li><a class=\"page_list_current\">" + page + "</a></li>");

                /***********************************************************************
                 *分页中当前页码的后半部分显示情况
                 ***********************************************************************/

                //当前页减去首页的值>=5
                if (page - 1 >= 4 && MaxPage - page >= 4)
                {
                    for (int i = page + 1; i <= page + 1; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }
                else if (MaxPage <= 7 || (MaxPage - page < 4 && MaxPage > 7))
                {
                    for (int i = page + 1; i <= MaxPage; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }
                }
                else if (page - 1 < 4 && MaxPage >= (midNum + 2))
                {
                    for (int i = page + 1; i <= midNum; i++)
                    {
                        pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + i + ")'>" + i + "</a></li>");
                    }

                    pageHtml.Append("<li><a>...</a></li>");
                    pageHtml.Append("<li><a href='javascript:void(0);' onclick='" + FunctionName + "(" + MaxPage + ")'>" + MaxPage + "</a></li>");
                }


                //当前页等于MaxPage时和不等于MaxPage时
                if (page == MaxPage)
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\">下一页</a></li>");
                }
                else
                {
                    pageHtml.Append("<li><a class=\"N-commonNext\" href='javascript:void(0);' onclick='" + FunctionName + "(" + (page + 1) + ")'>下一页</a></li>");
                }
                pageHtml.Append("<li style='display:none' id='Span2'>" + MaxPage + "</li>");
                pageHtml.Append("</ul>");

                //pageHtml.Append("<p>跳至");
                //pageHtml.Append("<input class=\"page_jump\" type=\"text\" onkeyup=\"this.value=this.value.replace(/\\D/g,'')\"  id=\"pageNum\" value=\"" + page + "\">");
                //pageHtml.Append("页<a href=\"javascript:void(0)\" onclick=\"GoPage(document.getElementById('pageNum').value);\">跳转</a></p>");

                pageHtml.Append("</div>");
            }
            return pageHtml.ToString();


        }
        #endregion

    }
}
