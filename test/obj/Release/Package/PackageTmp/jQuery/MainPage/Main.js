
var isChange = false, canModify = false, controller = {}, _power = [];

window.onload = function () {

    while (!jQuery) {
        continue;
    }

    do_SetAjaxInit();

    $(document).ajaxStart(function () {
        $.openLoadingNoty();
    });

    $(document).ajaxComplete(function () {
        $.closeLoadingNoty();
        $.validator.unobtrusive.parse(document);
    });

    if ($.isFunction(do_InitPage))
        do_InitPage();
}

function do_InitPage() {

    isChange = false;
    canModify = false;

    $(".main-form").find("input,select,textarea")
        .attr("disabled", "disabled");

    $(".no-disabled,input[type=hidden]").removeAttr("disabled");

    if (typeof (registerFunction) != "undefined") {
        do_GetPagePower({
            complete: function () {
                controller = registerFunction();
                controller.do_PageOK();

                if ($(".view-title").length > 0) {
                    $("#view-title").text("–" + $(".view-title").text());
                    document.title = $(".view-title").text() + " - " + $("title").data("main-title");
                }
                else if (_power != null && _power["name"] != null) {
                    $("#view-title").text("–" + _power["name"]);
                    document.title = _power["name"] + " - " + $("title").data("main-title");
                }
                else {
                    $("#view-title").text("");
                }
            }
        });
    }
}

function do_SetAjaxInit(options) {

    var defaults = {
        beforeSend: function (xhr, settings) {
            if (!settings.url.match(/^(http|https)/)) {
                settings.url = (_root + settings.url).replaceAll(_root, _root);
            }
        }
    };

    var settings = $.extend(defaults, options);

    $.ajaxSetup(settings);
}

function do_PageAjaxSuccess() {
    do_InitPage();
}

function do_AjaxFail(xhr) {
    $.closeLoadingNoty();
    $.openSucessNoty(xhr.responseJSON.customerMessage, { dismissQueue: false, killer: true });
}

function do_GetPagePower(options) {

    var defaults = {
        complete: function () {

        }
    }

    var settings = $.extend(defaults, options);

    $.proxies.servicesapi.getprogrampower().done(function (response) {
        _power = response.data;
    }).complete(function () {
        settings.complete();
    });
}

function do_PowerCheck(power) {
    var p = _power[power];

    if (!p) {
        //TODO 多國語言
        $.openErrorNoty("無使用本功能權限！", { dismissQueue: false, killer: true });
    }

    return p;
}