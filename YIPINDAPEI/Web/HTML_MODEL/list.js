// JScript 文件

//页面转向GO按钮上的JS
function GoPage(page) {
    var cntypename = "chuanyidapei/list";

    var curls = "/" + cntypename + "_" + page + ".htm";

    if (page == 1) {
        curls = "/" + cntypename + ".htm";
    }

    window.location.href = curls;
}


function GoPage_First(page) {
    $.post("/templatecs/handle/Create_html_handle.ashx", { action: "get_pagelist", total: $("#hidt").val(), page: $("#hidp").val() }, function (data) {
        $(".pagination-box").html(data);
    });

}
GoPage_First($("#hidp").val());
