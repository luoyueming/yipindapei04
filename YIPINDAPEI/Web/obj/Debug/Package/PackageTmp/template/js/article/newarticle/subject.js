

jQuery(".txtMarquee-top").slide({ mainCell: ".bd ul", autoPlay: true, effect: "top", vis: 1, delayTime: 700 });

//验证当前页码是否符合条件
function iscurpage() {
    $(".page_list ul li a").css("color", "#ccc");
    var curpage = 1;

    curpage = $(".page_list_current").html();

    if (curpage == null || curpage == undefined) {
        curpage = 1;
    }

    jQuery(".page_list ul li a.N-commonNext:first").css("position", "absolute").css("left", "3px").css("backgroundPosition", "-251px -248px").css("color", "#212121");
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

        $(this).css("color", "#212121").css("border", "2px solid #212121").css("height", "51px").css("lineHeight", "51px");
    });
    jQuery(".page_list ul li a.N-commonNext").mouseout(function () {

        $(this).css("color", "#212121").css("border", "1px solid #f2f2f2").css("height", "53px").css("lineHeight", "53px");
    });

    $(".page_list ul li a.page_list_current").css("color", "#212121").css("background", "none");

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


    var navname = $("#hidtype").val();
    var sname = $("#hitsname").val();

    var curls = navname+"list_"+page+".html";

    if (sname != "") {
        curls = navname + "_" + page + ".html";
    }

    window.location.href = curls;

}
