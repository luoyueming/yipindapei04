
//翻页
function GoPage(page) {
    if (page <= 0) {
        page = $(".page_list_current").html();

        if (page == null || page == undefined) {
            page = 1;
        }
    }

    window.location.href = "/hren/search" + "_" + page + "_" + $("#hidtype").val();

}

//给搜索框赋初始值
$("#keywordss").val($("#hidtype").val());