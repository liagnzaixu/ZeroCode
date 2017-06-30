(function($) {
    $.zc = $.zc || { version: 1.0 };
    $.zc.easyui = $.zc.easyui || {};
    $.zc.tools = $.zc.tools || {};
})(jQuery);

(function($) {
    $.zc.easyui = {
        loadFilter: function(data) {
            return { "rows": data.Rows, "total": data.Total };
        }
    };

    $.zc.tools = {

        getUrlParam: function(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg); //匹配目标参数
            if (r != null) return decodeURI(r[2]);
            return null; //返回参数值
        },

        //获取浏览器类型
        getBrowserType: function() {
            var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
            var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器  
            var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器  
            var isEdge = userAgent.indexOf("Windows NT 6.1; Trident/7.0;") > -1 && !isIE; //判断是否IE的Edge浏览器  
            var isFF = userAgent.indexOf("Firefox") > -1; //判断是否Firefox浏览器  
            var isSafari = userAgent.indexOf("Safari") > -1 && userAgent.indexOf("Chrome") == -1; //判断是否Safari浏览器  
            var isChrome = userAgent.indexOf("Chrome") > -1 && userAgent.indexOf("Safari") > -1; //判断Chrome浏览器  

            if (isIE) {
                var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
                reIE.test(userAgent);
                var fIEVersion = parseFloat(RegExp["$1"]);
                if (fIEVersion == 7) {
                    return "IE7";
                } else if (fIEVersion == 8) {
                    return "IE8";
                } else if (fIEVersion == 9) {
                    return "IE9";
                } else if (fIEVersion == 10) {
                    return "IE10";
                } else if (fIEVersion == 11) {
                    return "IE11";
                } else {
                    return "IEUnknown";
                } //IE版本过低  
            } //isIE end  

            if (isFF) {
                return "FF";
            }
            if (isOpera) {
                return "Opera";
            }
            if (isSafari) {
                return "Safari";
            }
            if (isChrome) {
                return "Chrome";
            }
            if (isEdge) {
                return "Edge";
            }
            return "unknown";
        },

        //获取浏览器版本  
        getBrowserVer: function () {
            var browser = "";
            var version = "";
            var userAgent = navigator.userAgent;
            var tempArray = "";
            var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器  
            var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器  
            var isEdge = userAgent.toLowerCase().indexOf("edge") > -1 && !isIE; //判断是否IE的Edge浏览器
            var isIE11 = (userAgent.toLowerCase().indexOf("trident") > -1 && userAgent.indexOf("rv") > -1);


            if (/[Ff]irefox(\/\d+\.\d+)/.test(userAgent)) {
                tempArray = /([Ff]irefox)\/(\d+\.\d+)/.exec(userAgent);
                browser = tempArray[1];
                version = tempArray[2];
            } else if (isIE) {
                browser = "IE";
                var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
                reIE.test(userAgent);
                var fIEVersion = parseFloat(RegExp["$1"]);
                if (fIEVersion == 7)
                { version = "7"; }
                else if (fIEVersion == 8)
                { version = "8"; }
                else if (fIEVersion == 9)
                { version = "9"; }
                else if (fIEVersion == 10)
                { version = "10"; }
                else {
                    version = "0";
                }
            } else if (isEdge) {
                browser = "Edge";
                version = "Edge";
            } else if (isIE11) {
                browser = "IE";
                version = "11";
            } else if (/[Cc]hrome\/\d+/.test(userAgent)) {
                tempArray = /([Cc]hrome)\/(\d+)/.exec(userAgent);
                browser = tempArray[1];
                version = tempArray[2];
            } else if (/[Vv]ersion\/\d+\.\d+\.\d+(\.\d)* *[Ss]afari/.test(userAgent)) {
                tempArray = /[Vv]ersion\/(\d+\.\d+\.\d+)(\.\d)* *([Ss]afari)/.exec(userAgent);
                browser = tempArray[3];
                version = tempArray[1];
            } else if (/[Oo]pera.+[Vv]ersion\/\d+\.\d+/.test(userAgent)) {
                tempArray = /([Oo]pera).+[Vv]ersion\/(\d+)\.\d+/.exec(userAgent);
                browser = tempArray[1];
                version = tempArray[2];
            } else {
                browser = "unknown";
                version = "unknown";
            }
            //返回变量browser可以获取浏览器类型
            return version;
        },
     
        //判断是否是IE浏览器  
        isIE: function (){  
            var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串 
            var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器  
            var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器  
            return isIE;
        },
     
        //获取IE浏览器版本  
        versionOfIE: function() {
            var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
            var isOpera = userAgent.indexOf("Opera") > -1; //判断是否Opera浏览器  
            var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera; //判断是否IE浏览器  
            var isEdge = userAgent.indexOf("Windows NT 6.1; Trident/7.0;") > -1 && !isIE; //判断是否IE的Edge浏览器  
            if (isIE) {
                var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
                reIE.test(userAgent);
                var fIEVersion = parseFloat(RegExp["$1"]);
                if (fIEVersion == 7) {
                    return "IE7";
                } else if (fIEVersion == 8) {
                    return "IE8";
                } else if (fIEVersion == 9) {
                    return "IE9";
                } else if (fIEVersion == 10) {
                    return "IE10";
                } else if (fIEVersion == 11) {
                    return "IE11";
                } else {
                    return "unknown";
                } //IE版本过低  
            } else if (isEdge) {
                return "Edge";
            } else {
                return "unknown"; //非IE  
            }
        },

        //获取操作系统
        getOS : function() {
            var os = navigator.platform;
            var userAgent = navigator.userAgent;
            var info = "";
            var tempArray = "";
            //判断操作系统  
            if (os.indexOf("Win") > -1) {
                if (userAgent.indexOf("Windows NT 5.0") > -1) {
                    info += "Win2000";
                } else if (userAgent.indexOf("Windows NT 5.1") > -1) {
                    info += "WinXP";
                } else if (userAgent.indexOf("Windows NT 5.2") > -1) {
                    info += "Win2003";
                } else if (userAgent.indexOf("Windows NT 6.0") > -1) {
                    info += "WindowsVista";
                } else if (userAgent.indexOf("Windows NT 6.1") > -1 || userAgent.indexOf("Windows 7") > -1) {
                    info += "Win7";
                } else if (userAgent.indexOf("Windows NT 6.2") > -1 || userAgent.indexOf("Windows 8") > -1) {
                    info += "Win8";
                } else if (userAgent.indexOf("Windows NT 6.3") > -1 || userAgent.indexOf("Windows 8.1") > -1) {
                    info += "Win8.1";
                } else if (userAgent.indexOf("Windows NT 10.0") > -1 || userAgent.indexOf("Windows 10") > -1) {
                    info += "Win10";
                } else {
                    info += "Other";
                }
            } else if (os.indexOf("Mac") > -1) {
                info += "Mac";
            } else if (os.indexOf("X11") > -1) {
                info += "Unix";
            } else if (os.indexOf("Linux") > -1) {
                info += "Linux";
            } else {
                info += "Other";
            }
            return info;
        },
    };
})(jQuery);