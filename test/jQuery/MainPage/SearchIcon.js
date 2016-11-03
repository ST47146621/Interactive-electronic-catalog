var _openWindow;

//綁定查詢按鈕
(function do_BindSearch() { //---> 綁定搜尋按鈕
    $(document).on("mousedown", ".SearchIcon,#query-btn", function (e) {
        do_OpenSearchView(e, $(this));
    });
})()

//查詢權限判斷
function do_SearchPowerCheck(name, options) {
    var defaults = {
        success: do_OpenUrlView,
        target: $(this)
    }

    var settings = $.extend(defaults, options);

    var p = do_PowerCheck("search");
    if (p) {
        $.proxies.servicesapi.getdetailurl(name)
            .done(function (res) {
                settings.success(res.data, settings.target);
            })
            .fail(function (xhr) {
                $.openAjaxErrorNoty(xhr);
            });
    }
}
//開啟查詢頁面
function do_OpenSearchView(e, $this) {
    var $obj = $($this.data("search-target"));
    var check = true;

    if ((/^(false|1)$/i).test($obj.data("check")) || (/^(false|1)$/i).test($obj.attr("data_check"))) {
        check = false;
    }
    
    if ($this.prevAll("input:eq(0)").hasClass("key-field") || $this.is("#query-btn") || $this.is(".btn-item")) {
        check = false;
    }

    if (check == true && canModify == false)
        return false;

    var name = $this.data("name");
    if (e.button == 2) { // 右鍵
        this.oncontextmenu = function (e) {
            return false;
        };
        if ($this.data("right-view") != null)
            name = $this.data("right-view");
        else {
            window.setInterval(function () {
                this.oncontextmenu = function (e) {
                    return true;
                };
            }, 2000);
            return false;
        }
    }

    do_SearchPowerCheck(name, { target: $this });

    window.setTimeout(function () {
        this.oncontextmenu = function (e) {
            return true;
        };
    }, 2000);
}

function do_OpenUrlView(result, $this) {
    if (result != false) {
        var data = result.url;
        data += "?";
        if ($this.prevAll("input:eq(0)").hasClass("key-field") || $this.is("#query-btn")) {
            data += "target=main";
        }
        else if ($this.data("sub-program") != null) {
            data += "sub=" + $this.data("sub");
        }
        var sendData = $this.data("senddata");
        if (sendData != null) {

            if ($this.closest("tr").hasClass("tr")) {
                $(".edit").removeClass("edit");
                $this.closest("tr").addClass("edit");
            }

            var argument = sendData.split(",");
            var arg;
            for (var i = 0; i <= argument.length - 1; i++) {
                var arr = argument[i].split("$");

                if (/^\@/.test(arr[0])) {
                    arg = arr[0].replaceAll("@", "");
                }
                else if ($(arr[0])[0] == null) { //若無選取到物件，則讓參數為空字串
                    arg = "";
                }
                else {
                    if ($(arr[0]).get(0).tagName == "TD")
                        arg = $(arr[0]).text().trim();
                    else
                        arg = $(arr[0]).val().trim();
                }
                data += "&" + arr[1] + "=" + encodeURIComponent(arg);
            }
        }
        var searchTarget = $("#searchTarget")
        if ($("#searchTarget")[0] != null)
            searchTarget.text($this.data("search-target"));
        else
            $("body").append("<span id='searchTarget' class='hidden'>" + $this.data("search-target") + "</span>");
        WinOpen(data, $this.data("name"), result.width, result.height);
    }

}
//取得點擊打開查詢頁面的物件
function getSearchTarget() {
    return $("#searchTarget").text();
}
//Ajax查詢頁面資料
function do_ajax_page(key, compare) {
    if (compare == null)
        compare = "0";

    var refresh = $("#refresh-page");
    var url = refresh.attr("href").split("?")[0];

    var str = url + "?Flag=" + compare;
    if (typeof (key) == "undefined" || key == null || key.length == 0) {
        str = refresh.closest("form").attr("action").split("?")[0];
    }
    else {
        if (typeof (key) == "number")
            key += "";
        if (typeof (key) == "string") {
            str = str + "&Key=" + encodeURIComponent(key);
        } else if (typeof (key) == "object") {
            str = str + "&Key=" + encodeURIComponent(key[0]);
            for (var i = 1; i <= key.length - 1; i++) {
                str = str + "&Key" + (i + 1) + "=" + encodeURIComponent(key[i]);
            }
        }
    }

    refresh.attr({
        href: str
    });

    refresh.click();
}

function SetSearchData(target, data) {
    $("#searchTarget").text(null);
    var $obj = $(target);

    if (typeof _openWindow != 'undefined')
        _openWindow.close();

    var check = true;

    if ((/^(false|1)$/i).test($obj.data("check")) || (/^(false|1)$/i).test($obj.attr("data_check"))) {
        check = false;
    }

    if (check == true && canModify == false)
        return;

    //主要的Key資料 (ex:mid、code、pid)
    var mainData = data[0][0];
    //次要的資料 (ex:name、abbr、spec、mark)
    var subData = data[0][1];

    if (/(word)|(code)|(date)|(payment)|(memo)|(mark)|(addr)|(add)|(title)/gi.test(target) && $obj.data("search-set") == null) {
        $obj.val(mainData).text(mainData).click().focus();
    }
    else {
        if ($obj.get(0).tagName == "TD") { // td
            var set = $obj.data("search-set").match(/[.#_\sa-zA-Z\d\(\):$]+/g);
            var arr;
            $obj.text(mainData).removeClass("autofill-error input-validation-error").removeData("error-msg");
            for (var i = 0; i <= set.length - 1; i++) {
                arr = set[i].match(/[.#_\sa-zA-Z\d\(\):]+/g);
                $(arr[0]).text(subData[arr[1]]).click();
            }
        }
        else { // input

            $obj.val(mainData).focus();

            if ($obj.hasClass("autoFill_cid") || $obj.hasClass("autoFill_sid")) {
                $obj.change();
                return true;
            }
            else {
                var set = $obj.data("search-set").match(/[.#_\sa-zA-Z\d:$]+/g);
                var arr;
                for (var i = 0; i <= set.length - 1; i++) {
                    arr = set[i].match(/[.#_\sa-zA-Z\d:]+/g);
                    targetDOM = $(arr[0]);
                    targetDOM.val(subData[arr[1]]);
                    targetDOM.change();
                }
                $obj.focus();
            }
        }
    }
}

function SetMultiRowData(target, data) {
    $("#searchTarget").text(null);
    var $obj = $(target);
    if ($obj.get(0).tagName == "INPUT") { // input
        SetSearchData(target, data);
        return;
    }

    if (typeof _openWindow != 'undefined')
        _openWindow.close();

    var check = true;

    if ((/^(false|1)$/i).test($obj.data("check")) || (/^(false|1)$/i).test($obj.attr("data_check"))) {
        check = false;
    }

    if (check == true && canModify == false) {
        return;
    }

    if ($obj.find("input").val() != "") {
        $(".plus-btn", $(target).closest("table")).click();
    }

    var targetMainTd = target.split(/\s/)[target.split(/\s/).length - 1];
    var targetTd = ".tr:has(#edit_inp) " + targetMainTd;
    var dataLength = data.length - 1;
    var td, MainData, SubData;

    //多選要使用非同步來加快速度
    $.ajaxSetup({ async: true });

    for (var i = 0; i <= dataLength; i++) {
        MainData = data[i][0];
        td = $(targetTd);
        td.text(MainData).click();
        td.closest("tr").addClass("MultiRow");

        var set = td.data("search-set").match(/[.#_\sa-zA-Z\d\(\):$]+/g);
        var subData = data[i][1];
        var arr;
        for (var j = 0; j <= set.length - 1; j++) {
            arr = set[j].match(/[.#_\sa-zA-Z\d\(\):]+/g);
            $(arr[0]).text(subData[arr[1]]).click();
        }

        if (i != dataLength) {
            $(".plus-btn", td.closest("table")).click();
        }
    }

    //防止Ajax導致回傳順序不一問題，強制在最後一行取得焦點。
    $(".tr:last " + targetMainTd, td.closest("table")).click();

    if (typeof (controller.do_SetMultiRowData) == "function") //多行取回後自定義處理
        controller.do_SetMultiRowData(td.closest("table"));

    $(".MultiRow").removeClass("MultiRow");
    $(".edit").removeClass("edit");
}