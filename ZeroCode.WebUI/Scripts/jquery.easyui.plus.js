var easyuiPanelOnOpen = function (left, top) {
    var iframeWidth = $(this).parent().parent().width();

    var iframeHeight = $(this).parent().parent().height();

    var windowWidth = $(this).parent().width();
    var windowHeight = $(this).parent().height();

    var setWidth = (iframeWidth - windowWidth) / 2;
    var setHeight = (iframeHeight - windowHeight) / 2;
    $(this).parent().css({/* 修正面板位置 */
        left: setWidth,
        top: setHeight
    });

    if (iframeHeight < windowHeight) {
        $(this).parent().css({/* 修正面板位置 */
            left: setWidth,
            top: 0
        });
    }
    $(".window-shadow").hide();
};
$.fn.window.defaults.onOpen = easyuiPanelOnOpen;

var easyuiPanelOnResize = function (left, top) {


    var iframeWidth = $(this).parent().parent().width();

    var iframeHeight = $(this).parent().parent().height();

    var windowWidth = $(this).parent().width();
    var windowHeight = $(this).parent().height();


    var setWidth = (iframeWidth - windowWidth) / 2;
    var setHeight = (iframeHeight - windowHeight) / 2;
    $(this).parent().css({/* 修正面板位置 */
        left: setWidth - 6,
        top: setHeight - 6
    });

    if (iframeHeight < windowHeight) {
        $(this).parent().css({/* 修正面板位置 */
            left: setWidth,
            top: 0
        });
    }
    $(".window-shadow").hide();
    //$(".window-mask").hide().width(1).height(3000).show();
};
$.fn.window.defaults.onResize = easyuiPanelOnResize;