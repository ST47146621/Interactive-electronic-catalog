/*
    最後更動時間 2014/07/11 
    ~~~ 新增 hasClasses 擴充方法 ~~~~
*/

// jQuery 擴充方法
(function ($, undefined) {

    // 取得停駐點位置
    $.fn.getCursorPosition = function () {
        var el = $(this).get(0);
        var pos = 0;
        if ('selectionStart' in el) {
            pos = el.selectionStart;
        } else if ('selection' in document) {
            el.focus();
            var Sel = document.selection.createRange();
            var SelLength = document.selection.createRange().text.length;
            Sel.moveStart('character', -el.value.length);
            pos = Sel.text.length - SelLength;
        }
        return pos;
    }

    // 擴充 hasClasses 方法
    // 可以讓你依次判斷多個 class (已哭)
    $.fn.hasClasses = function (selector) {
        var self = this;
        var selectors = selector.split(",");
        for (var i in selectors) {
            if ($(self).hasClass(selectors[i]))
                return true;
        }
        return false;
    }

    //讓 radio 或 checkbox 無法使用的方法(不會淡掉)
    $.fn.disableChecked = function () {
        this.addClass("disabled").bind("change", do_DisableChecked);
    }

    //讓 radio 或 checkbox 取消無法使用的方法
    $.fn.enableChecked = function () {
        this.removeClass("disabled").unbind("change", do_DisableChecked);
    }

    function do_DisableChecked() {
        var self = $(this);
        var target = $("input[name='" + self.attr("name") + "']");
        target.prop("checked", function () {
            return this.defaultChecked;
        });
    }

})(jQuery);

// 擴充方法(取得當天日期)
Date.prototype.getNow = function () {
    var d = new Date();
    return d.toString().toDateString();
}

Date.prototype.toDateString = function () {
    var year = this.getFullYear();
    var month = this.getMonth() + 1;
    var day = this.getDate();

    if (month < 10)
        month = "0" + month;

    if (day < 10)
        day = "0" + day;
    return year + "/" + month + "/" + day;
}

String.prototype.toDateString = function () {
    var d = new Date(this);
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var day = d.getDate();

    if (month < 10)
        month = "0" + month;

    if (day < 10)
        day = "0" + day;

    return d.toDateString();
}

String.prototype.isDateTimeFormat = function () {
    if ((new Date(this)).toString() !== "Invalid Date") {
        return this.replaceAll("-", "/").replaceAll("T", " ").replace(/\.\d*$/, "");
    }
    return this;
}

// 全部取代方法
String.prototype.replaceAll = function (oldChar, newChar) {
    var newString = this.replace(new RegExp("(" + oldChar + ")+", "g"), newChar);
    return newString;
};

// 將字串格式化為貨幣格式
String.prototype.toDecimal = function (type, defaultValue) {

    var value = this.replace(/[\s,]/g, "")
        , arr = [], sign, pattern;

    if (isNaN(value) || value.length == 0 || value == 0) {
        if (defaultValue != null)
            value = defaultValue;
        else
            value = 0;
    }

    value = Number(value);
    sign = value < 0 ? "-" : "";

    pattern = $("#" + type).text();
    if (pattern.length == 0)
        pattern = "999,999,999.999";
    if (pattern.indexOf(".") > 0)
        value = value.toFixed(pattern.split(".")[1].length);
    else
        value = value.toFixed(0);

    arr = value.split(".");
    arr[0] = arr[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
    return sign + arr.join(".");

};

// 偷懶專用(字串轉成BOOL)
String.prototype.toBool = function () {
    return (/^(true|1)$/i).test(this);
}

// 偷懶專用(布林轉布林)
Boolean.prototype.toBool = function () {
    return this == true;
}

//開啟視窗
function WinOpen(url, id, widthBase, heightBase) {

    url = (_root + url).replaceAll(_root, _root);
    widthBase = widthBase * 1;
    heightBase = heightBase * 1;

    if (widthBase === undefined || widthBase === 0)
        widthBase = 0.4;

    if (widthBase > 2 && widthBase < 800)
        widthBase = 1;

    if (heightBase === undefined || heightBase === 0)
        heightBase = 0.9;

    if (heightBase > 2 && heightBase < 600)
        heightBase = 1;

    var sFeatures;
    var w = screen.availWidth * widthBase;
    if (widthBase > 1) {
        w = widthBase;
    }
    if (screen.availWidth < widthBase) {
        w = screen.availWidth;
    }
    var h = screen.availHeight * heightBase;
    if (heightBase > 1) {
        h = heightBase;
    }
    if (screen.availHeight < heightBase) {
        h = screen.availHeight;
    }

    var left = (screen.availWidth - w) / 2;
    var top = (screen.availHeight - h) / 2 - 20;
    var rn = new Date().getMilliseconds();

    if ($("#stepId")[0] != undefined)
        id = id + $("#stepId").val();

    sFeatures = 'height=' + h + ',';
    sFeatures = sFeatures + 'width= ' + w + ',';
    sFeatures = sFeatures + 'top=' + top + ',';
    sFeatures = sFeatures + 'left=' + left + ',';
    sFeatures = sFeatures + 'fullscreen= yes';
    sFeatures = sFeatures + 'resizable= no, status= no,location= no';

    _openWindow = window.open(url, id, config = '' + sFeatures + '');
    if (typeof (_openWindow) != "undefined" && typeof (_openWindow.focus) == "function")
        _openWindow.focus();
}
