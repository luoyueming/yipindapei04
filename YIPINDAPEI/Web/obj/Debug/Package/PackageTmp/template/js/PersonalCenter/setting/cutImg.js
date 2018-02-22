//当前logo的隐藏域
var hidelogo = "#hidlogo";
//显示logo的img标签的ID
var show_img_id = "#logo_img";

//点击更换头像的时候进行操作
$(document).on('click','#updateHeadImg',function () {
    $("#uploads-img").show();
    var iframeHeadImg = '/PersonalCenter/setting/HeadImg.aspx';
    window.open(iframeHeadImg, "headimg");
    //saveHeadImg();
});

//点击关闭的时候进行操作
$(document).on('click','#closeHeadImg',function () {
    $("#uploads-img").hide();
});

/**************************************************************************************************************
*************用户头像功能开始
***************************************************************************************************************/
//等比缩放图片
function DrawImage(width, height, FitWidth, FitHeight) {
    var cheight = 0, cwidth = 0;
    if (width > 0 && height > 0) {
        if (width / height >= FitWidth / FitHeight) {
            if (width > FitWidth) {
                cwidth = FitWidth;
                cheight = (height * FitWidth) / width;
            }
            else {
                cwidth = width;
                cheight = height;
            }
        }
        else {
            if (height > FitHeight) {
                cheight = FitHeight;
                cwidth = (width * FitHeight) / height;
            }
            else {
                cwidth = width;
                cheight = height;
            }
        }
    }
    return "{height:" + cheight + ",width:" + cwidth + "}";
}
//截图控制效果
function preview(img, selection) {
    var scaleX = 224 / selection.width;
    var scaleY = 224 / selection.height;

    var w = Math.round(scaleX * img.width);
    var h = Math.round(scaleY * img.height);
    var x = Math.round(scaleX * selection.x1);
    var y = Math.round(scaleY * selection.y1);

    $("#w").val(w);
    $("#h").val(h);
    $("#x").val(x);
    $("#y").val(y);

    //动态小头像 获取当前选中框的宽度，高度，左边框，右边框
    $('#smallImg160 > div > img').css({
        width: w + 'px',
        height: h + 'px',
        marginLeft: '-' + x + 'px',
        marginTop: '-' + y + 'px'
    });
    $('#smallImg80 > div > img').css({
        width: (w / 2) + 'px',
        height: (h / 2) + 'px',
        marginLeft: '-' + (x / 2) + 'px',
        marginTop: '-' + (y / 2) + 'px'
    });
    $('#smallImg40 > div > img').css({
        width: (w / 4) + 'px',
        height: (h / 4) + 'px',
        marginLeft: '-' + (x / 4) + 'px',
        marginTop: '-' + (y / 4) + 'px'
    });
}

saveHeadImg = function () {
    $("#uploads-img").find("a[data-name=save]").click(function () {
        FunMsg("请选择图片");
    });
}