using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewXzc.Common
{
    public class PageHelper
    {
        
        private static int page_cnt = 5;//每页最多显示多少页  8
        private static int page_num = page_cnt/2+1;//点击哪页时会加载下面的一个序列
        private static int mid_num = page_cnt / 2;//中间页
        private static int cur_top_num = 0;//前面减去的数值
        private static int cur_bottom_num = 0;//前面相加的数值
        private static int cur_type = 3;//以1  2  3  ...  maxpage 这种形式显示

        /// <summary>
        /// 获取中间值
        /// </summary>
        /// <returns></returns>
        private static int GetMiddleNum(int cur_pages,int type)
        {
            //以1  2  3  ...  maxpage 这种形式显示
            if (type == 3)
            {
                if (page_cnt % 2 == 0)
                {
                    mid_num = page_cnt / 2;
                }
                else
                {
                    mid_num = page_cnt / 2 + 1;//4  2   8  5   3   9  6  4  10   7  5   11   8  6  12
                }
                cur_top_num = cur_pages - 1;//3  2  4  4  3   5   5   4   6  6  5  7  7  6   8   8  7  9
                cur_bottom_num = cur_pages + 1;

                if (cur_top_num < 1)
                {
                    cur_top_num = 1;
                }

            }
            else//以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表
            {
                if (page_cnt % 2 == 0)
                {
                    mid_num = page_cnt / 2;
                    cur_top_num = cur_pages - mid_num + 1;//4  2   7   5   3   8  6  4  9   7  5   10   8  6  11
                    cur_bottom_num = cur_pages + mid_num;
                }
                else
                {
                    mid_num = page_cnt / 2 + 1;//4  2   8  5   3   9  6  4  10   7  5   11   8  6  12
                    cur_top_num = cur_pages - mid_num + 2;
                    cur_bottom_num = cur_pages + mid_num;
                }
            }
            return mid_num;
        }

        /// <summary>
        /// 加载页码字符串
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="maxpage">最大页码</param>
        public static string WritePage(int page, int maxpage)
        {
            if (maxpage == 1)
            {
                return "";
            }
            StringBuilder html = new StringBuilder();

            mid_num = GetMiddleNum(page,cur_type);

            //if (page > 1)
            //{
            //    html.Append("<a class=\"FirstPage\" href=\"javascript:GetPage('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");
            //}
            html.Append("<a class=\"FirstPage\" href=\"javascript:GetPage('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");

            if (maxpage > page_cnt)
            {
                if (page < mid_num)//以1  2  3  ...  maxpage 这种形式显示< mid_num；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表< page_num
                {
                    for (int i = 1; i <= mid_num; i++)//以1  2  3  ...  maxpage 这种形式显示<= mid_num；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表<= page_cnt
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:GetPage('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else if (page < maxpage - page_num)//  6   15   1   10  2   11  7   15  3   12
                {
                    //html.Append("<a href=\"javascript:GetPage('1')\" dataval=\"1\">1</a>");
                    //html.Append("<a>...</a>");
                    //for (int i = cur_top_num; i <= cur_bottom_num; i++)//8  5-3  5+4   7  4-2   4+4   6   4-2  4+3    5   3-1  3+3
                    for (int i = cur_top_num; i <= cur_top_num + 2; i++)//以1  2  3  ...  maxpage 这种形式显示i <=cur_top_num + 2 ；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表i <= cur_bottom_num
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:GetPage('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else
                {
                    //html.Append("<a href=\"javascript:GetPage('1')\" dataval=\"1\">1</a>");
                    //html.Append("<a>...</a>");
                    for (int i = maxpage - (page_cnt-1); i <= maxpage; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                }
            }
            else
            {

                for (int i = 1; i <= maxpage; i++)
                {
                    if (i == page)
                    {
                        html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                    else
                    {
                        html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                }

            }

            //if (page < maxpage)
            //{
            //    html.Append("<a class=\"LastPage\" href=\"javascript:GetPage('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");
            //}
            html.Append("<a class=\"LastPage\" href=\"javascript:GetPage('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");

            html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum\" /><font class=\"font_color\">页</font>&nbsp;&nbsp;<input type=\"button\" value=\"确定\" onclick=\"GoSkipPage()\" class=\"page_btn_ok\" />");
            //html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum\" /><font class=\"font_color\">页</font><span><a onclick=\"GoSkipPage()\"  class=\"page_btn_ok\" />确定</a></span>");

            return (html.ToString());
        }

        /// <summary>
        /// 加载页码的一个集合
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="maxpage">最大页码</param>
        public static StringBuilder WritePage(int page, int maxpage,StringBuilder html)
        {
            if (maxpage == 1)
            {
                return html;
            }
            mid_num = GetMiddleNum(page, cur_type);

            //if (page > 1)
            //{
            //    html.Append("<a class=\"FirstPage\" href=\"javascript:GetPage('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");
            //}
            html.Append("<a class=\"FirstPage\" href=\"javascript:GetPage('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");

            if (maxpage > page_cnt)
            {
                if (page < mid_num)//以1  2  3  ...  maxpage 这种形式显示< mid_num；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表< page_num
                {
                    for (int i = 1; i <= mid_num; i++)//以1  2  3  ...  maxpage 这种形式显示<= mid_num；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表<= page_cnt
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:GetPage('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else if (page < maxpage - page_num)
                {
                    //html.Append("<a href=\"javascript:GetPage('1')\" dataval=\"1\">1</a>");
                    //html.Append("<a>...</a>");
                    //for (int i = cur_top_num; i <= cur_bottom_num; i++)//8  5-3  5+4   7  4-2   4+4   6   4-2  4+3    5   3-1  3+3
                    for (int i = cur_top_num; i <= cur_top_num + 2; i++)//以1  2  3  ...  maxpage 这种形式显示i <=cur_top_num + 2 ；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表i <= cur_bottom_num
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:GetPage('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else
                {
                    //html.Append("<a href=\"javascript:GetPage('1')\" dataval=\"1\">1</a>");
                    //html.Append("<a>...</a>");
                    for (int i = maxpage - (page_cnt - 1); i <= maxpage; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                }
            }
            else
            {

                for (int i = 1; i <= maxpage; i++)
                {
                    if (i == page)
                    {
                        html.Append("<a href=\"javascript:GetPage('" + i + "')\" class=\"this_pages\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                    else
                    {
                        html.Append("<a href=\"javascript:GetPage('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                }

            }

            //if (page < maxpage)
            //{
            //    html.Append("<a class=\"LastPage\" href=\"javascript:GetPage('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");
            //}
            html.Append("<a class=\"LastPage\" href=\"javascript:GetPage('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");

            html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum\" /><font class=\"font_color\">页</font>&nbsp;&nbsp;<input type=\"button\" value=\"确定\" onclick=\"GoSkipPage()\" class=\"page_btn_ok\" />");
            //html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum\" /><font class=\"font_color\">页</font><a onclick=\"GoSkipPage()\"  class=\"page_btn_ok\" />确定</a>");

            return html;
        }

        /// <summary>
        /// 加载页码的一个集合
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="maxpage">最大页码</param>
        public static StringBuilder WritePage(int page, int maxpage,string curclass, StringBuilder html)
        {
            if (maxpage == 1)
            {
                return html;
            }

            mid_num = GetMiddleNum(page, cur_type);

            if (page > 1)
            {
                html.Append("<a class=\"FirstPage\" href=\"javascript:GetPageByInterview('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");
            }

            if (maxpage > page_cnt)
            {
                if (page > mid_num)
                {
                    for (int i = 1; i <= mid_num; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:GetPageByInterview('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else if (page < maxpage - page_num)
                {
                    html.Append("<a href=\"javascript:GetPageByInterview('1')\" dataval=\"1\">1</a>");
                    html.Append("<a>...</a>");
                    for (int i = cur_top_num; i <= cur_bottom_num; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:GetPageByInterview('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else
                {
                    html.Append("<a href=\"javascript:GetPageByInterview('1')\" dataval=\"1\">1</a>");
                    html.Append("<a>...</a>");
                    for (int i = maxpage - (page_cnt - 1); i <= maxpage; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                }
            }
            else
            {

                for (int i = 1; i <= maxpage; i++)
                {
                    if (i == page)
                    {
                        html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                    else
                    {
                        html.Append("<a href=\"javascript:GetPageByInterview('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                }

            }

            if (page < maxpage)
            {
                html.Append("<a class=\"LastPage\" href=\"javascript:GetPageByInterview('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");
            }

            html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum2\" /><font class=\"font_color\">页</font>&nbsp;&nbsp;<input type=\"button\" value=\"确定\" onclick=\"GoSkipPage2()\" class=\"page_btn_ok\" />");
            //html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum2\" /><font class=\"font_color\">页</font><a onclick=\"GoSkipPage2()\"  class=\"page_btn_ok\" />确定</a>");

            return html;
        }

        /// <summary>
        /// 加载页码的一个集合
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="maxpage">最大页码</param>
        /// <param name="function_name">翻页的方法名称</param>
        /// <param name="curclass">当前页的样式名称</param>
        /// <param name="html">集合变量</param>
        /// <returns></returns>
        public static StringBuilder WritePage(int page, int maxpage,string function_name, string curclass, StringBuilder html)
        {
            if (maxpage == 1)
            {
                return html;
            }

            mid_num = GetMiddleNum(page, cur_type);

            if (page <= 1)
            {
                html.Append("<a class=\"prev\" href=\"javascript:void(0)\"\">上一页</a>");
            }
            else
            {
                html.Append("<a class=\"prev\" href=\"javascript:" + function_name + "('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");
            }
            //html.Append("<a class=\"prev\" href=\"javascript:" + function_name + "('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");

            if (maxpage > page_cnt)
            {
                if (page < mid_num)
                {
                    for (int i = 1; i <= mid_num; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:" + function_name + "('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else if (page < maxpage - page_num)
                {
                    //html.Append("<a href=\"javascript:" + function_name + "('1')\" dataval=\"1\">1</a>");
                    //html.Append("<a>...</a>");
                    for (int i = cur_top_num; i <= cur_top_num + 2; i++)//以1  2  3  ...  maxpage 这种形式显示i <=cur_top_num + 2 ；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表i <= cur_bottom_num
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:" + function_name + "('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else
                {
                    //html.Append("<a href=\"javascript:" + function_name + "('1')\" dataval=\"1\">1</a>");
                    //html.Append("<a>...</a>");
                    for (int i = maxpage - (page_cnt - 1); i <= maxpage; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                }
            }
            else
            {

                for (int i = 1; i <= maxpage; i++)
                {
                    if (i == page)
                    {
                        html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                    else
                    {
                        html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                }

            }

            if (page < maxpage)
            {
                html.Append("<a class=\"next\" href=\"javascript:" + function_name + "('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");
            }
            else
            {
                html.Append("<a class=\"next\" href=\"javascript:void(0);\"\">下一页</a>");
            }
            //html.Append("<a class=\"next\" href=\"javascript:" + function_name + "('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");

            //html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum3\" /><font class=\"font_color\">页</font>&nbsp;&nbsp;<input type=\"button\" value=\"确定\" onclick=\"GoSkipPage3()\" class=\"page_btn_ok\" />");

            return html;
        }

        /// <summary>
        /// 加载页码的一个集合
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="maxpage">最大页码</param>
        /// <param name="function_name">翻页的方法名称</param>
        /// <param name="curclass">当前页的样式名称</param>
        /// <param name="html">集合变量</param>
        /// <returns></returns>
        public static StringBuilder WritePage(int page, int maxpage, string function_name, string curclass, StringBuilder html,string goFun)
        {
            if (maxpage == 1)
            {
                return html;
            }

            mid_num = GetMiddleNum(page, cur_type);

            if (page <= 1)
            {
                html.Append("<a class=\"prev\" href=\"javascript:void(0);\" \">上一页</a>");
            }
            else
            {
                html.Append("<a class=\"prev\" href=\"javascript:" + function_name + "('" + (page - 1) + "')\" dataval=\"" + (page - 1) + "\">上一页</a>");
            }

            if (maxpage > page_cnt)
            {
                if (page < mid_num)
                {
                    for (int i = 1; i <= mid_num; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:" + function_name + "('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else if (page < maxpage - page_num)
                {
                    for (int i = cur_top_num; i <= cur_top_num + 2; i++)//以1  2  3  ...  maxpage 这种形式显示i <=cur_top_num + 2 ；以正规形式显示1  2  3  4  5  6点击第4页时显示下一列页码的列表i <= cur_bottom_num
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                    html.Append("<a>...</a>");
                    html.Append("<a href=\"javascript:" + function_name + "('" + maxpage + "')\" dataval=\"" + maxpage + "\">" + maxpage + "</a>");
                }
                else
                {
                    for (int i = maxpage - (page_cnt - 1); i <= maxpage; i++)
                    {
                        if (i == page)
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                        else
                        {
                            html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                        }
                    }
                }
            }
            else
            {

                for (int i = 1; i <= maxpage; i++)
                {
                    if (i == page)
                    {
                        html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" class=\"" + curclass + "\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                    else
                    {
                        html.Append("<a href=\"javascript:" + function_name + "('" + i + "')\" dataval=\"" + i + "\">" + i + "</a>");
                    }
                }

            }

            if (page < maxpage)
            {
                html.Append("<a class=\"next\" href=\"javascript:" + function_name + "('" + (page + 1) + "')\" dataval=\"" + (page + 1) + "\">下一页</a>");
            }

            html.Append("<font class=\"font_color\">跳至</font><input type=\"text\" id=\"pagenum3\" /><font class=\"font_color\">页</font>&nbsp;&nbsp;<input type=\"button\" value=\"确定\" onclick=\"" + goFun + "()\" class=\"page_btn_ok\" />");



            return html;
        }
    }
}
