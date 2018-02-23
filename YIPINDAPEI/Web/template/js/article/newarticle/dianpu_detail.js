
yo.search('search', '输入您要搜索的内容');

function search_result() {
    var key = jQuery.trim($("#keywordss").val());

    if (key == "" || key == "输入您要搜索的内容") {
        FunMsg("输入您要搜索的内容!");
        return false;
    }
    else {

        window.location.href = "/hren/search_1_" + key;
        return false;

    }
}

yo.scroll({
    id: 'scrollEight_men',
    boxTag: 'div',
    singleTag: 'div',
    leftBtn: 'scrollEight_men_left',
    rightBtn: 'scrollEight_men_right',
    loop: true,
    auto: true,
    time: 2000
});
yo.scroll({
    id: 'scrollTen_men',
    boxTag: 'div',
    singleTag: 'div',
    leftBtn: 'scrollTen_men_left',
    rightBtn: 'scrollTen_men_right',
    loop: true,
    auto: true,
    time: 2000
});
yo.scroll({
    id: 'scrollFourteen_men',
    boxTag: 'div',
    singleTag: 'div',
    leftBtn: 'scrollFourteen_men_left',
    rightBtn: 'scrollFourteen_men_right',
    loop: true,
    auto: true,
    time: 2000
});

jQuery("img[title='200-200微信红人汇二维码.jpg']").attr("src", "http://ypindapei.com/template/img/share/weibo_rcode.png").attr("title", "红人汇");
$(window).scroll(function () {
    var mTop = $('.line')[0].offsetTop;
    var sTop = $(window).scrollTop();
    var result = mTop - sTop + $(".soso-box").height() - 100;
    var n = $('.main-wrapper').height() - $(".soso-box").height() - 70;
    if (result <= 0) {
        $('.detailhot-soso').css("position", "fixed").css("top", "0px");

        console.log(n);

        if ($('.main-wrapper').height() - sTop <= 150) {
            $('.detailhot-soso').css("position", "absolute").css("top", n);


        } else {
            $('.hot-soso').css("position", "fixed").css("top", "0px").css("marginTop", '-4px');
        }
    } else {
        $('.detailhot-soso').css("position", "static").css("marginTop", '-4px');
    }

})


var b = $("iframe").width() * 0.6 + "px";
$("iframe").css("height", b);

var cindex = 0;

//换一组
change_next_div_html();

$(document).on("click", "#today_btn", function () {
    change_next_div_html();
});

function change_next_div_html() {
    var today_div_obj = $(".today_jrrd_all .today_div");
    var today_div_len = today_div_obj.length;

    if (cindex >= today_div_len) {
        cindex = 0;
    }

    $("#today_news").html(today_div_obj.eq(cindex).html());

    cindex++;
}


//初始化A标签
$(".main-content-body a").each(function () {
    var obj = $(this);
    obj.attr("target", "_self");
});

//通过点击A标签进行跳转
function gourl(ahref) {
    window.open(ahref);
}