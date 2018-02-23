var now_detail_url = window.location.href.toLowerCase();

if (now_detail_url.indexOf("hrzx") >= 0 || now_detail_url.indexOf("hrzp") >= 0 || now_detail_url.indexOf("hrzf") >= 0 || now_detail_url.indexOf("hrxz") >= 0 || now_detail_url.indexOf("hrcz") >= 0 || now_detail_url.indexOf("hrsd") >= 0 || now_detail_url.indexOf("xwk") >= 0) {
    now_detail_url = now_detail_url.replace("hrzx", "zixun").replace("hrzp", "zuopin").replace("hrzf", "zhuanfang").replace("hrxz", "xiezhen").replace("hrcz", "chengzhang").replace("hrsd", "whjj/redian").replace("xwk", "xingwangka");

    window.location.href = now_detail_url;
}
else if (now_detail_url.indexOf("redian") >= 0) {
    if (now_detail_url.indexOf("whjj") < 0) {
        now_detail_url = now_detail_url.replace("redian", "whjj/redian");

        window.location.href = now_detail_url;
    }
}
else if (now_detail_url.indexOf("wanghongzhubo") >= 0) {
    if (now_detail_url.indexOf("whjj") < 0) {
        now_detail_url = now_detail_url.replace("wanghongzhubo", "whjj/wanghongzhubo");

        window.location.href = now_detail_url;
    }
}

$(function () {
    // 轮播图效果
    $('#slides').slidesjs({
        width: 640,
        height: 300,
        navigation: {
            active: true
        },
        play: {
            auto: true,
            pauseOnHover: true
        }
    });
});

jQuery(".txtMarquee-top").slide({ mainCell: ".bd ul", autoPlay: true, effect: "top", vis: 1, delayTime: 700 });

//验证当前页码是否符合条件
function iscurpage() {
    $(".page_list ul li a").css("color", "#ccc");
    var curpage = 1;

    curpage = $(".page_list_current").html();

    if (curpage == null || curpage == undefined) {
        curpage = 1;
    }

    jQuery(".page_list ul li a.N-commonNext:first").css("position", "absolute").css("left", "3px").css("backgroundPosition", "-251px -248px").css("color","#212121");
    jQuery(".page_list ul li a.N-commonNext:last").css("position", "absolute").css("right", "3px").css("textIndent", "-34px").css("backgroundPosition", "97px -248px").css("color", "#212121");
    jQuery(".page_list ul li a").not(".N-commonNext").mouseover(function () {
        jQuery(".page_list ul li a").not(".N-commonNext").css("color", "#ccc");
        $(this).css("color", "red");
        $(".page_list_current").css("color", "#212121")
    });
    jQuery(".page_list ul li a").not(".N-commonNext").mouseout(function () {
        jQuery(".page_list ul li a").not(".N-commonNext").css("color", "#ccc");
        $(this).css("color", "#ccc");
        $(".page_list_current").css("color", "#212121")
    });
    jQuery(".page_list ul li a.N-commonNext").mouseover(function () {

        $(this).css("color", "#212121").css("border", "2px solid #212121").css("height","51px").css("lineHeight","51px");
    });
    jQuery(".page_list ul li a.N-commonNext").mouseout(function () {
   
        $(this).css("color", "#212121").css("border", "1px solid #f2f2f2").css("height", "53px").css("lineHeight", "53px");
    });

    $(".page_list ul li a.page_list_current").css("color", "#212121").css("background","none");

    if (curpage <= 1) {
        jQuery(".page_list ul li a.N-commonNext:first").css("color", "#ccc").css("backgroundPosition", "-251px -296px");
        jQuery(".page_list ul li a.N-commonNext:first").hover(function () {
            jQuery(".page_list ul li a.N-commonNext:first").css("color", "#ccc").css("border", "1px solid #f2f2f2").css("height", "53px").css("lineHeight", "53px").css("backgroundPosition", "-251px -296px");
        })
    }

    var lastpage = $(".page_list ul li").eq($(".page_list ul li").length - 3).find("a").html();


    if (curpage >= lastpage) {
        jQuery(".page_list ul li a.N-commonNext:last").css("color", "#ccc").css("backgroundPosition", "97px -296px");
        jQuery(".page_list ul li a.N-commonNext:first").css("color", "#212121").css("backgroundPosition", "-260px -248px");
        jQuery(".page_list ul li a.N-commonNext:last").hover(function () {
            jQuery(".page_list ul li a.N-commonNext:last").css("color", "#ccc").css("backgroundPosition", "97px -296px").css("border", "1px solid #f2f2f2").css("height", "53px").css("lineHeight", "53px");
        })
    }
}

iscurpage();

//翻页
function GoPage(page) {
    

    var type = $("#hidtype").val();
    var navname = "hrzx";
    navname = "zixun";

//    if (type == 1) {
//        navname = "hrzx";
//    }
//    else if (type == 2) {
//        navname = "hrxz";
//    }
//    else if (type == 3) {
//        navname = "hrzp";
//    }
//    else if (type == 4) {
//        navname = "hrzf";
//    }
//    else if (type == 5) {
//        navname = "hrzt";
//    }
//    else if (type == 35) {
//        navname = "hrfs";
//    }
//    else if (type == 40) {
//        navname = "xwk";
//    }
//    else if (type == 42) {
//        navname = "hrsd";
//    }
//    else if (type == 44) {
//        navname = "hrcz";
//    }
//    else if (type == 45) {
//        navname = "hrzb";
    //    }
    if (type == 1) {
        navname = "zixun";
    }
    else if (type == 2) {
        navname = "xiezhen";
    }
    else if (type == 3) {
        navname = "zuopin";
    }
    else if (type == 4) {
        navname = "zhuanfang";
    }
    else if (type == 5) {
        navname = "hrzt";
    }
    else if (type == 35) {
        navname = "hrfs";
    }
    else if (type == 40) {
        navname = "xingwangka";
    }
    else if (type == 42) {
        navname = "whjj/redian";
    }
    else if (type == 44) {
        navname = "chengzhang";
    }
    else if (type == 45) {
        navname = "whjj/wanghongzhubo";
    }
    else if (type == 46) {
        navname = "whjj/weibo";
    }
    else if (type == 47) {
        navname = "whjj/weixin";
    }
    else if (type == 354) {
        navname = "zhinan";
    }

    var stype = $("#hidstyle").val();
    var curls="/" + navname + "_" + page;

    if (stype > 0) {
        curls += "_" + stype;

        if (stype >= 359 && stype <= 362) {

            curls = "/zhinan/";

            switch (stype*1) {
                case 359:
                    curls += "clys";
                    break;
                case 360:
                    curls += "fzdp";
                    break;
                case 361:
                    curls += "hzjq";
                    break;
                case 362:
                    curls += "hdth";
                    break;
                case 369:
                    curls += "psdp";
                    break;
            }

            curls += "/";

            if (page > 1) {
                curls += "index_"+page+".html";
            }
        }

    }
    window.location.href = curls;
    
}

var num = 500;
var type = $("#hidtype").val();

//评论
$(document).on('keyup', '.review', function () {
    var str_leng = GetCharLength($.trim($(this).val()));
    var balance_num = num - str_leng;
    $(this).parent().find("em").html(balance_num);
});


//评论操作
$(document).on('click', '.review_btn', function () {
    var obj = $(this).parents(".activity-details-d-form");
    var content = $.trim(obj.find(".review").val());

    var balance_num = obj.find("em").html();

    if (content == "") {
        FunMsg("评论内容不能为空");
    }
    else if (balance_num * 1 < 0) {
        FunMsg("评论内容不能大于" + num + "个字符");
    }
    else {
        AddReview_Reply_Content($(this), escape(content), type, 0, 0, 0, 0);
    }
});

//--------------------------------------------------------------------------------------第一级（评论内容）

//展开回复输入框
$(document).on('click', '.reply_article', function () {
    var o = $(this);

    var html = '<div class="activity-details-d-form">';
    html += '<textarea class="reply_content"></textarea>';
    html += '<p>';
    html += '还可输入<em>' + num + '</em>个字符';
    html += '<button class="reply_down_btn">回复</button>';
    html += '</p>';
    html += '</div>';

    var reply_len = $(o).parents(".item-content").find(".activity-details-d-form").length;

    if (reply_len == 0) {
        $(o).parent().after(html);
    }

    //评论回复
    var reply_obj = $(".reply_content");

    reply_obj.on('keyup', function () {

        var str_leng = GetCharLength($.trim($(this).val()));
        var balance_num = num - str_leng;
        $(this).parent().find("em").html(balance_num);
    });

});

//回复
$(document).on('click', '.reply_down_btn', function () {
    var obj = $(this).parents(".item-content");
    var par_obj = obj.find(".hidval_first");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    var content = $.trim(obj.find(".reply_content").val());
    var balance_num = obj.find(".activity-details-d-form").find("em").html();

    if (content == "") {
        FunMsg("回复内容不能为空");
    }
    else if (balance_num * 1 < 0) {
        FunMsg("回复内容不能大于" + num + "个字符");
    }
    else {
        AddReview_Reply_Content($(this), escape(content), type, reply_id, 0, 0, pid);
    }
});


//删除
$(document).on('click', '.reply_del', function () {
    var par_obj = $(this).parents(".item-content").find(".hidval_first");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    AddReview_Reply_Content($(this), "", type, reply_id, 0, reply_id, pid);
});


//喜欢
$(document).on('click', '.laud_up', function () {
    var par_obj = $(this).parents(".item-content").find(".hidval_first");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    AddReview_Reply_Content($(this), "", type, reply_id, 1, 0, pid);
});

//--------------------------------------------------------------------------------------第二级（回复内容）

//展开回复输入框
$(document).on('click', '.reply_under_article', function () {
    var o = $(this);

    var html = '<div class="activity-details-d-form">';
    html += '<textarea class="reply_under_content"></textarea>';
    html += '<p>';
    html += '还可输入<em>' + num + '</em>个字符';
    html += '<button class="reply_under_down_btn">回复</button>';
    html += '</p>';
    html += '</div>';

    var reply_len = $(o).parents(".item-content").find(".activity-details-d-form").length;

    if (reply_len == 0) {
        $(o).parent().after(html);
    }

    //评论回复
    var reply_obj = $(".reply_under_content");

    reply_obj.on('keyup', function () {

        var str_leng = GetCharLength($.trim($(this).val()));
        var balance_num = num - str_leng;
        $(this).parent().find("em").html(balance_num);
    });

});

//回复
$(document).on('click', '.reply_under_down_btn', function () {
    var obj = $(this).parents(".item-content");
    var par_obj = obj.find(".hidval");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    var content = $.trim(obj.find(".reply_under_content").val());
    var balance_num = obj.find(".activity-details-d-form").find("em").html();

    if (content == "") {
        FunMsg("回复内容不能为空");
    }
    else if (balance_num * 1 < 0) {
        FunMsg("回复内容不能大于" + num + "个字符");
    }
    else {
        AddReview_Reply_Content($(this), escape(content), type, reply_id, 0, 0, pid);
    }
});


//删除
$(document).on('click', '.reply_under_del', function () {
    var par_obj = $(this).parents(".item-content").find(".hidval");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    AddReview_Reply_Content($(this), "", type, reply_id, 0, reply_id, pid);
});


//喜欢
$(document).on('click', '.laud_under', function () {
    var par_obj = $(this).parents(".item-content").find(".hidval");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    AddReview_Reply_Content($(this), "", type, reply_id, 1, 0, pid);
});


//实际操作，（content：评论或回复内容，当执行喜欢或取消喜欢操作时可为空；type：1：兄弟党，2：美女党，replyid：被回复的评论ID；islike：0：评论或回复操作，1：喜欢；id：被删除的评论或回复的ID；pid：被回复的最顶级的父级评论的ID）
function AddReview_Reply_Content(obj, content, type, replyid, islike, id, pid) {

    $.post("/templatecs/handle/articlehandle.ashx", { action: "Add_Review_Reply", content: content, type: type, replyid: replyid, islike: islike, id: id, pid: pid, articleid: $("#hidid").val() }, function (data) {
        if (data == "nologin") {
            Login_TC();
        }
        else if (data == "no") {
            FunMsg("系统繁忙，请稍后重试");
        }
        else if (data == "repet") {
            FunMsg("您已经点过赞了");
        }
        else if (data == "noarticle") {
            FunMsg("当前文章不存在");
        }
        else if (data == "noreview") {
            FunMsg("当前评论或回复不存在");
        }
        else {

            $(".review").val("");
            $(".reply_content").val("");
            $(".activity-details-d-form").find("em").html(num);
            $(".activity-details-d-form").find("em").html(num);

            //喜欢
            if (islike > 0) {
                obj.addClass("lauded");
                obj.html(" " + data);
            }
            else if (id > 0) {//删除
                if (data.indexOf("del" >= 0)) {
                    var arr = data.split(",");

                    if (arr.length >= 2) {
                        if (arr.length == 2) {
                            var review_cnt = arr[1];

                            $(".review_cnt").html("(" + review_cnt + ")");
                        }
                        else {
                            var review_cnt = arr[arr.length - 1];

                            $(".review_cnt").html("(" + review_cnt + ")");

                            for (var i = 1; i < arr.length - 1; i++) {
                                var article_obj_first = $(".hidval_first[data-id='" + arr[i] + "']");
                                article_obj_first = article_obj_first.parent().parent();
                                var article_obj_under = $(".hidval[data-id='" + arr[i] + "']");
                                article_obj_under = article_obj_under.parent().parent();

                                //删除指定的回复
                                if (article_obj_first.length == 0) {
                                    article_obj_under.remove();

                                }
                                else {//删除与该评论有关的所有的回复
                                    article_obj_first.remove();

                                    var review_type = 0;
                                    var max_min_id = 0;
                                    GetList(1, type, review_type, review_type, max_min_id);
                                }

                            }
                        }
                    }
                }
            }
            else {
                //window.location.href = window.location.href;
                var review_type = 0;
                var max_min_id = 0;

                //加载最新评论
                if (replyid == 0) {
                    review_type = 0;
                    max_min_id = 0;
                    GetList(1, type, review_type, review_type, max_min_id);
                }
                else//加载最新回复
                {
                    review_type = 1;

                    var article_obj_first = $(".hidval_first[data-id='" + replyid + "']");
                    article_obj_first = article_obj_first.parent();
                    var article_obj_under = $(".hidval[data-id='" + replyid + "']");
                    article_obj_under = article_obj_under.parent().parent().parent();

                    //不是第一级的回复
                    if (article_obj_first.length == 0) {
                        max_min_id = article_obj_under.find(".activity-details-discuss-item:last").find(".hidval").attr("data-id");
                    }
                    else {//是第一级的回复
                        max_min_id = article_obj_first.find(".activity-details-discuss-item:last").find(".hidval").attr("data-id");
                        if (max_min_id == undefined) {
                            max_min_id = replyid;
                        }

                    }


                    GetList(1, type, review_type, replyid, max_min_id);
                }
            }
        }
    });

}


//加载评论或回复的列表
//type：党派类型，1：兄弟党，2：美女党；review_type，0：评论，1：回复；replyid，评论或被回复的ID（评论ID可为0）；max_min_rid，评论或回复的ID（评论用当前页面中，当前类型下的所有评论中最大的ID，回复用最小的ID，加载更多用最小的评论的ID）
function GetList(page, type, review_type, replyid, max_min_rid) {
    $.post("/templatecs/handle/articlehandle.ashx", { action: "GetReviewContent", page: page, type: type, replyid: replyid, max_min_rid: max_min_rid, articleid: $("#hidid").val() }, function (data) {

        if (data == "no") {
            FunMsg("系统繁忙，请稍后重试");
        }
        else {
            var arr = data.split("¤");

            var review_obj = $(".activity-details-discuss");

            var htmls = arr[0];
            var page_html = arr[1];
            var review_cnt = arr[2];
            
            $(".review_cnt").html("("+review_cnt+")");

            //评论
            if (review_type == 0) {
                review_obj.find(".activity-details-discuss-item").remove();
                review_obj.find(".activity-details-d-form").after(htmls);
                review_obj.find(".pagination-box").html(page_html);
            } //回复
            else {
                var article_obj_first = $(".hidval_first[data-id='" + max_min_rid + "']");
                article_obj_first = article_obj_first.parent();
                var article_obj_under = $(".hidval[data-id='" + max_min_rid + "']");
                article_obj_under = article_obj_under.parent().parent();

                //让回复框消失
                article_obj_under.parent().find(".activity-details-d-form").remove();

                //加载回复内容，不是第一级回复
                if (article_obj_first.length == 0) {
                    article_obj_under.after(htmls);
                }
                else {//是第一级回复
                    article_obj_first.find(".item-btns").after(htmls);
                }
            }
        }
    });
}


//分页
function GetList_page(page) {
    $.post("/templatecs/handle/articlehandle.ashx", { action: "GetReviewContent", page: page, type: $("#hidtype").val(), articleid: $("#hidid").val() }, function (data) {

        if (data == "no") {
            FunMsg("系统繁忙，请稍后重试");
        }
        else {
            var arr = data.split("¤");

            var review_obj = $(".activity-details-discuss");

            var htmls = arr[0];
            var page_html = arr[1];
            var review_cnt = arr[2];

            $(".review_cnt").html("("+review_cnt+")");

            review_obj.find(".activity-details-discuss-item").remove();
            review_obj.find(".activity-details-d-form").after(htmls);
            review_obj.find(".pagination-box").html(page_html);
        }
    });
}


///////////////////////////////////////////////////////举报///////////////////////////////////////////////////
//第一级举报
$(document).on('click', '.reply_report', function () {
    var par_obj = $(this).parents(".item-content").find(".hidval_first");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    ToRrport(reply_id,type,pid,1,0);
});

//第二级举报
$(document).on('click', '.reply_under_report', function () {
    var par_obj = $(this).parents(".item-content").find(".hidval");

    var reply_id = par_obj.attr("data-id");
    var type = par_obj.attr("data-type");
    var pid = par_obj.attr("data-pid");

    ToRrport(reply_id, type, pid, 1, 0);
});
