
var tccurl = window.location.href.toLowerCase();

//////////////////////////////////////////////////////////////////////////////////QQ登录

if (tccurl.indexOf("sina") < 0) {
    //QQ登录状态
    var lstate = QC.Login.check();
//    alert(lstate);
    ////退出登录
    //QC.Login.signOut();


    //调用QC.Login方法，指定btnId参数将按钮绑定在容器节点中

    QC.Login({
        //btnId：插入按钮的节点id，必选
        btnId: "qqLoginBtn"
    });


//自定义登录按钮
//    QC.Login({
//        //btnId：插入按钮的节点id，必选
//        btnId: "qqLoginBtn",
//        //用户需要确认的scope授权项，可选，默认all
//        scope: "all",
//        //按钮尺寸，可用值[A_XL| A_L| A_M| A_S|  B_M| B_S| C_S]，可选，默认B_S
//        size: "A_XL"
//    }, function (reqData, opts) {//登录成功
//        //    //根据返回数据，更换按钮显示状态方法
//        //    var dom = document.getElementById(opts['btnId']),
//        //       _logoutTemplate = [
//        //    //        //openId
//        //    //            '<span>openId：' + openId + '，accessToken：' + accessToken + '</span>',
//        //    //大头像
//        //            '<span><img src="{figureurl_qq_2}" class="{size_key}"/></span>',
//        //    //小头像
//        //            '<span><img src="{figureurl_qq_1}" class="{size_key}"/></span>',
//        //    //昵称
//        //            '<span>{nickname}</span>',
//        //    //性别
//        //            '<span>{gender}</span>',
//        //    //退出
//        //            '<span><a href="javascript:QC.Login.signOut();">退出</a></span>'
//        //       ].join("");
//        //    dom && (dom.innerHTML = QC.String.format(_logoutTemplate, {
//        //        figureurl_qq_2: reqData.figureurl_qq_2,
//        //        figureurl_qq_1: reqData.figureurl_qq_1,
//        //        nickname: QC.String.escHTML(reqData.nickname), //做xss过滤
//        //        gender: reqData.gender
//        //    }));

//    }, function (opts) {//注销成功
//        //    //alert('QQ登录 注销成功');
//        //    window.location.href = "/logout";
//    });

    if (lstate) {
        //从页面收集OpenAPI必要的参数。get_user_info不需要输入参数，因此paras中没有参数
        var paras = {};

        //用JS SDK调用OpenAPI
        QC.api("get_user_info", paras)
        //指定接口访问成功的接收函数，s为成功返回Response对象
	    .success(function (s) {
	        //成功回调，通过s.data获取OpenAPI的返回数据
	        //alert("获取用户信息成功！当前用户昵称为：" + s.data.nickname);

	        //获取用户openId和accessToken
	        QC.Login.getMe(function (openId, accessToken) {

	            var uname = QC.String.escHTML(s.data.nickname); //做xss过滤，昵称
	            var sex = s.data.gender; //性别
	            var uhead = s.data.figureurl_qq_2; //大头像
	            var uhead_small = s.data.figureurl_qq_1; //小头像
	            //        var now_uhead = uhead + ";" + uhead_small;

	            //将当前登录用户信息存放到数据库
	            new_islogin(openId, uname, sex, uhead, 1);

	        });
	    })
            //指定接口访问失败的接收函数，f为失败返回Response对象
	    .error(function (f) {
	        //失败回调
	        alert("获取用户信息失败！");
	    })
            //指定接口完成请求后的接收函数，c为完成请求返回Response对象
	    .complete(function (c) {
	        //完成请求回调
	        //alert("获取用户信息完成！");
	    });
    }
}


////////////////////////////////////////////////////////////////////////////////END



//////////////////////////////////////////////////////////////////////////////////新浪微博登录

//新浪微博退出登录
//WB2.logout(function () {
//    //callback function
//});



if (tccurl.indexOf("qq") < 0 && tccurl.indexOf("wechat") < 0) {
    WB2.anyWhere(function (W) {
        W.widget.connectButton({
            id: "wb_connect_btn",
            callback: {
                login: function (o) {
//                    console.log(o);
                    //登录成功之后执行
//                    alert("ID：" + o.id + "，用户名：" + o.screen_name + "，头像地址：" + o.profile_image_url.replace("crop.0.0.180.180.50", "crop.0.0.180.180.100"));

                    var uname = o.screen_name; //做xss过滤，昵称
                    var sex = o.gender; //性别
                    var uhead = o.profile_image_url.replace("crop.0.0.180.180.50", "crop.0.0.180.180.100"); //大头像

                    //将当前登录用户信息存放到数据库
                    new_islogin(o.id, uname, sex, uhead, 2);
                },
                logout: function () {
                    //退出之后执行
//                    alert('退出之后执行');
                }
            }
        });
    });
}

////////////////////////////////////////////////////////////////////////////////END


//初始化登录图标
window.onload = function () {
    $(".loginTuceng").css("display", "block");
    $("#qqLoginBtn a img").attr('src', '/template/img/third-qq.jpg');
    $("#qqLoginBtn a img").css("marginLeft", "-26px");
    setTimeout(function () {
        $("#wb_connect_btn a img").attr('src', '/template/img/third-sina.jpg');
        $("#wb_connect_btn a img").css("marginLeft", "-26px");
    }, 500);

}
//END



//判断是否登录成功（正式）
function new_islogin(openid, uname, sex, uhead, third_logintype) {

    $.post("/templatecs/Handle/ThirdRegisterHandle.ashx", { action: "Is_Register", uname: escape(uname), sex_val: escape(sex), uhead: escape(uhead), third_logintype: third_logintype, third_uopenid: openid }, function (data) {
        if (data == "no") {
            alert("系统繁忙，请稍后重试");
        }
        else if (data == "noautho") {//还未授权登录
            alert("您还未授权登录功能");
        }
        else {
            //            var ccurl = window.location.href.toLowerCase();

            window.location.href = data;


        }
    });
}


