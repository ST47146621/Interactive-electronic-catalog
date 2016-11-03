/*
    最後更動時間 2014/07/11 
    ~~~ 修正一些功能性BUG ~~~~
*/

function GridView(data) {

    var that = this;

    this.columnTemplate = [];

    // GridTable 初始化
    this.do_InitialTable = function () {

        $(".table_scroll").each(function (i, o) {
            var $this = $(this);
            $this.find(".table_scrollHead th.th_display_none").each(function () {
                var j = $(this).index();
                $this.find(".table_scrollBody tr").each(function () {
                    $(this).find("td").eq(j).addClass("hidden");
                });
            });

            var width = 0, th, tmp;
            $this.find(".table_scrollHeadInner table th:not(.th_display_none)").each(function () {
                th = $(this);
                tmp = th.data("width");
                th.width(tmp);
                width = width + tmp * 1;
            });

            $this.find(".table_scrollHeadInner").css("width", width + 40 + "px")
                .find("table").css({ "width": width + 1 + "px", "table-layout": "fixed" })
                .find("th:gt(0)").resizable({
                    minWidth: 0,
                    helper: "ui-resizable-helper",
                    stop: function (event, ui) {

                        width = width - ui.originalSize.width + ui.size.width;

                        $this.find(".table_scrollHeadInner").css("width", width + 40 + "px")
                            .find("table").css("width", width + 1 + "px");
                        $this.find(".table_scrollBody table").css("width", width + "px")
                            .find("th").eq($(this).index()).width($(this).width());

                        $(this).data("width", $(this).width());
                    }
                });

            $this.find(".table_scrollBody")
                .css("height", window.innerHeight - $this.offset().top - 40 + "px")
                .scroll(function (e) { // 綁定表格scroll事件
                    $this.find(".table_scrollHead").scrollLeft($(this).scrollLeft());
                }).find("table").css({ "width": width + "px", "table-layout": "fixed" });;

            that.do_GridTableComplete($(this)); // 初始化完成
        });

        if (data != null) {
            $.each(data, function (i, o) {
                var $this = $(".table_scroll").eq(i);
                var value = JSON.parse(data[i].val());
                if (value != null) {
                    that.do_SetGridData($this, value); // 填入資料
                    that.do_AddGridButton($this); // 新增按鈕
                }
            });
        }

        that.do_CreateColumnTemplate(); // 新增欄位複本
        that.do_ResizeGridHeight();

        return true;
    }

    // 將資料填入 GridTable
    this.do_SetGridData = function ($this, data, options) {

        var defaults = {
            complete: function ($this) {

            }
        }

        var settings = $.extend(defaults, options);

        if ($this == null)
            $this = $(".table_scroll");

        if (data != "") {

            var tables = "", dataLength = data.length - 1;

            for (var x = 1 ; x <= 1; x++) {

                for (var i = 0; i <= dataLength ; i++) {
                    tables += this.do_WriteColumnTemplate($this, data[i]);
                }
            }

            if ($this.find(".table_scrollBody .tr").length != 0)
                $(tables).insertAfter($this.find(".table_scrollBody .tr:last"));
            else
                $this.find(".table_scrollBody tbody").prepend(tables);
        }

        $this.find(".table_scrollBody tbody tr td:first-child").addClass("td-first");

        settings.complete($this);
    }

    // 新增欄位副本
    this.do_CreateColumnTemplate = function () {

        var tmp = "", name, type, defaultValue, isSelect;

        $(".table_scroll").each(function (index, obj) {
            that.columnTemplate.push(that.do_WriteColumnTemplate($(this)));
        });
    }

    // 格式化欄位副本
    this.do_WriteColumnTemplate = function ($this, data) {

        var tables = "", name, type, pattern, defaultValue, required, readonly, text;
        tables += "<tr class='tr'><td class='td-first'>";
        tables += '<button class="btn btn-xs btn-danger minus-btn" type="button" ><span class="glyphicon glyphicon-minus"></span></button></td>';

        $this.find(".table_scrollHeadInner th:gt(0)").each(function () {

            name = $(this).data("name"); // 名稱
            type = $(this).data("type"); // 其他型態
            pattern = $(this).data("pattern"); // 貨幣型態
            defaultValue = $(this).data("defaultValue"); // 預設值
            required = $(this).data("required"); // 是否必須輸入
            readonly = $(this).data("readonly"); // 是否唯讀

            text = (data ? data[name] : "")

            if (text == null) text = "";

            if ($(this).hasClass("th_display_none")) {
                tables += "<td class='hidden td_" + name;
            }
            else {
                tables += "<td class='td_" + name;
            }

            if (pattern != "") { // 貨幣格式需而外處理
                tables += " text-right money-td' data-pattern ='" + pattern + "'";
                text = text.toString().toDecimal(pattern, defaultValue);
            }
            else if (type != "") {
                if (type == "date") {
                    tables += " date-td'";
                    if (text.length > 0)
                        text = text.toDateString();
                    else
                        text = "";
                    // TODO 
                }
                else if (type == "select") {
                    tables += " select-td'";

                    var selector = $("select.select_" + name).clone().removeClass(function (index, css) {
                        return (css.match(/(^|\s)select_\S+/g) || []).join(' ');
                    }).addClass("select_" + name);

                    var defaultValue = selector.children("option[selected]").val();
                    selector.children("option").removeAttr("selected");
                    selector.children("option[value='" + text + "']").attr("selected", "selected");

                    if (selector.children("option[selected]").length == 0)
                        selector.children("option[value='" + defaultValue + "']").attr("selected", "selected");

                    text = selector.get(0).outerHTML;
                    // TODO
                }
                else if (type == "checkbox") {
                    tables += " checkbox-td'";
                    // TODO
                }
            }
            else {
                if (typeof(text) == "string") {
                    text = text.isDateTimeFormat();
                }
                tables += "'";
            }

            tables += "data-required='" + required + "' data-readonly='" + readonly + "' data-name='" + name + "'";

            if (pattern != "")
                tables += " data-oldvalue='" + text + "'>" + text.toDecimal(pattern, defaultValue) + "</td>";
            else {

                // 檢查是不是 HTML
                // 是，不指定 default
                // 否，指定 default

                if (/<[a-z][\s\S]*>/i.test(text)) {
                    tables += " data-oldvalue=''>" + text + "</td>";
                }
                else {
                    tables += " data-oldvalue='" + text + "'>" + text + "</td>";
                }
            }

        });

        tables += "</tr>";

        return tables;
    }

    // GridTable 初始化完成
    this.do_GridTableComplete = function ($this) {
        $this.css("visibility", "visible");
    }

    // GridTable 中加入新增或刪除按鈕
    this.do_AddGridButton = function ($this) {

        var plusicon = '<button class="btn btn-xs btn-danger plus-btn" type="button"><span class="glyphicon glyphicon-plus"></span></button>';
        var length = $this.find(".tr").length;

        if (length > 0) {
            $this.find(".tr").eq(length - 1).next("tr")
                .children("td:first-child").html(plusicon);
        }
        else {
            $this.find("tbody tr:first td:first-child").html(plusicon);
        }
    }

    // GridTable 綁定點擊表身事件
    this.do_BindColumnClick = function (e, options) {

        var defaults = {
            skipValid: false,
            before: function (last_td) {
                return true;
            },
            success: function (get_td, inp) {

            }
        }

        var settings = $.extend(defaults, options);

        var $this = $(e.currentTarget); //get td

        if (!defaults.skipValid) {
            if (!this.do_CheckGridModify()) {
                if ($this.hasClass("select-td")) {
                    $(e.target).blur(); //get select and close list
                }
                return false;
            }
        }

        var last_inp = $("#edit_inp"),
            last_td = last_inp.closest("td"),
            isReadonly;

        if ($this.has("#edit_inp").length > 0)
            return false;

        if (settings.before(last_td) != false) {

            if ($this.hasClass("select-td")) { //after check select return
                return false;
            }

            // TODO: 檢查 Td 的值是否有 Change

            if (last_td.data("oldvalue") == last_inp.val()) {
                last_td.data("oldvalue", last_inp.val());
                last_td.change().data("changed", false);
            }
            else {
                last_td.blur().data("changed", true);
            }

            if (last_td.hasClass("money-td"))
                last_inp.val(last_inp.val().toDecimal(last_td.data("pattern")));

            last_td.text(last_inp.val())
                .removeData("hotkey");

            var get_tr = $this.closest("tr"),
                html = $('<div class="input-group input-group-sm"><input type="text" id="edit_inp" class="form-control" placeholder="Text"><span class="input-group-btn"><button class="btn btn-default" type="button"><span class="glyphicon glyphicon-pencil"></span></button></span></div>'),
                inp = html.find("#edit_inp");

            isReadonly = $this.data("readonly");
            if (isReadonly != undefined) {
                if (isReadonly.toBool()) {
                    inp.addClass("isReadonly")
                    inp.attr("readonly", isReadonly.toBool());
                }
            }

            if ($this.hasClass("date-td")) { //data td
                html/*.find(".input-group")*/.addClass("date").find("span.glyphicon").toggleClass("glyphicon-pencil glyphicon-calendar");
            }

            inp.val($this.text().replace(/^(\s+)|(\s+)$/g, ""));
            $this.html(html);
            inp.focus();

            // TODO: 目前先用 css 來取代 addClass (效能問題)
            // $(".edit").removeClass("edit");
            // get_tr.addClass("edit"); // 會有效能問題，因為DOM數量太多導致查找速度變慢。
            // ==============================================
            last_td.closest("tr").css("background-color", "");
            get_tr.css("background-color", "#FF8040!important");

            settings.success($this, inp);
        }
    }

    // GridTable 綁定INPUT KeyDown 事件
    this.do_BindEditInpKeyDown = function (e, options) {

        var defaults = {
            before: function (e, get_tr, get_td, inp) {

            }
        }

        var settings = $.extend(defaults, options);

        var inp = $(e.target), status = false,
        get_td = inp.closest("td"),
        get_tr = get_td.closest("tr");

        settings.before(e, get_tr, get_td, inp);

        switch (e.which) {
            case 13:
            case 9:
                {
                    var next_td = get_td.nextAll("td:visible:not(.select-td):eq(0)");

                    get_td.data("hotkey", 13);

                    if (next_td.length == 0) {
                        var next_tr = get_tr.next("tr"),
                            target = next_tr.children("td:visible:eq(1)");
                        if (target.length == 0) {
                            next_tr.find(".plus-btn").click();
                        }
                        else
                            target.click()
                    }
                    else
                        next_td.click();

                    status = true;
                    break;
                }
            case 37:
                {
                    if (!inp.is(".isReadonly"))
                        if (inp.getCursorPosition() != 0) return;
                    get_td.prevAll("td:visible:eq(0)").click();

                    status = true;
                    break;
                }
            case 38:
                {
                    var get_class = get_td[0]
                        .className.match(/^td_{1}[\w]+/).join();
                    get_tr.prev("tr").children("." + get_class).click();

                    status = true;
                    break;
                }
            case 40:
                {
                    var get_class = get_td[0]
                        .className.match(/^td_{1}[\w]+/).join(),
                        next_tr = get_tr.next("tr"),
                        target = next_tr.children("." + get_class);

                    if (target.length == 0)
                        next_tr.find(".plus-btn").click();
                    else
                        target.click();

                    status = true;
                    break;
                }
        }

        if (status) {
            e.preventDefault();
            return true;
        }
    }

    // GridTable 綁定新增按鈕事件
    this.do_BindPlusButton = function (e, options) {

        var defaults = {
            skipValid: false,
            before: function (get_tr, prev_tr) {

            },
            success: function (get_tr) {

            }
        }

        var settings = $.extend(defaults, options);

        if (!settings.skipValid) {
            var can = this.do_CheckGridModify();
            if (!can)
                return false;
        }

        var $this = $(e.target), isValid = true,
          tmp_tr = $this.closest("tr"),
          prev_tr = tmp_tr.prev(".tr");

        settings.before(tmp_tr, prev_tr);

        prev_tr.children("td:gt(0):visible").each(function () {
            if ($(this).data("required").toBool()) {
                var value;
                var inp = $(this).find("input");

                if (inp.length > 0) {
                    value = inp.val();
                }
                else {
                    var select = $(this).find("select");

                    if (select.length > 0)
                        value = select.val();
                    else {
                        value = $(this).text();
                    }
                }

                if (value.length == 0) {
                    isValid = false;
                    $(this).click();
                    return false;
                }
            }
        })

        if (isValid == false)
            return false;

        var index = $this.parents(".table_scroll")
            .index(".table_scroll");

        var get_tr = $(this.columnTemplate[index]);

        get_tr.insertBefore(tmp_tr);
        get_tr.children("td:not(:hidden):eq(1)").click();

        settings.success(get_tr);
    }

    // GridTable 綁定刪除按鈕事件
    this.do_BindMinusButton = function (e, options) {
        var defaults = {
            skipValid: false,
            before: function (get_tr) {
                var defferred = $.Deferred();
                return defferred.resolve();
            },
            success: function (get_tr) {
                get_tr.remove();
            },
            cancel: function () {
                return;
            }
        }

        var $this = $(e.target),
            isValid = true;

        var settings = $.extend(defaults, options);

        if (!settings.skipValid) {
            var can = this.do_CheckGridModify();
            if (!can)
                return false;
        }

        var promise = settings.before($this.closest("tr"));

        promise.done(function (res) {
            if (isValid)
                return $.oepnDeleteConfirmNoty($this, settings.success, settings.cancel);
            else {
                return false;
            }
        }).fail(function (xhr) {
            var noty = $.openAjaxErrorNoty(xhr);
        })
    }

    // GridTable 綁定RESIZE事件
    this.do_ResizeGridHeight = function () {
        var resizeTimer;

        function resizeFunction() {
            var innerHeight = window.innerHeight;
            var height = 0;
            $(".table_scroll").each(function (i, o) {
                var $this = $(this);
                if ($this.parent("div").is(":visible")) {
                    height = innerHeight - $this.offset().top - 40 + "px";
                    return false;
                }
            });
            if (height <= 0) {
                var tab = $(".table_scroll").parent("div");
                if (tab.is(".tab-pane")) {
                    height = innerHeight - tab.siblings(".tab-pane.active").offset().top - 40
                }
            }
            $(".table_scroll").each(function (i, o) {
                var $this = $(this);
                if ($this.is("table.view-table td .table_scroll")) {
                    $this.find(".table_scrollBody").height(0); //避免影響td高度取得
                    $this.find(".table_scrollBody").height($this.closest("td").height() - 25 + "px");
                    $this.width(0);
                    $this.width($this.closest("td").width() + "px");
                }
                else {
                    if ($this.parent("div").is(":visible")) {
                        $this.find(".table_scrollBody").height(
                            innerHeight - $this.offset().top - 40 + "px");
                    }
                    else {
                        $this.find(".table_scrollBody").height(height);
                    }
                }
            });
        };

        $(window).resize(function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(resizeFunction, 550);
        });

        resizeFunction();
    }

    // GridTable 清空GRIDTABLE
    this.do_ClearGridTable = function (target) {
        if (target == null)
            target = ".table_scroll";

        $(target).each(function (i, o) {
            $(this).find(".plus-btn").remove();
            $(this).find(".tr").each(function () {
                $(this).removeClass("tr").html("<td class='td-first'></td>");
            })
            that.do_AddGridButton($(this));
        })
    }

    // GridTable 檢查是否有修改按鈕
    this.do_CheckGridModify = function () {
        if (canModify == false) {
            $.openSucessNoty("若要進行編修，請先按修改按鈕", { killer: true });
            return false;
        }
        return true;
    }

    // GridTable 轉換成 Input
    this.do_SetSaveInput = function (tableNames, targetRow) {

        $(".save_inp").remove();

        if (targetRow == null)
            targetRow = ".tr";

        var outputHtml = "";
        var tmp_page_li = $("ul.nav-tabs li.active");

        $.each(tableNames, function (i, o) {
            var table_scroll = $(".table_scroll").eq(i);

            if (table_scroll.parent("div").is("tab-pane")) {
                $("a[href='#" + table_scroll.parent("div").attr("id") + "']").click();
            }

            $(".plus-btn", table_scroll).click();

            table_scroll.find(targetRow).each(function (index) {

                var $this = $(this), get_index = index;

                $(this).children("td").each(function () {

                    if ($(this).hasClass("td-first"))
                        return;

                    var name = $(this).data("name");

                    if ($(this).hasClass("select-td")) {
                        var selector = $(this).find("select");
                        selector.attr("name", tableNames[i] + "[" + get_index + "]." + name);
                    }
                    else if ($(this).hasClass("-td")) {
                    }
                    else {
                        // 單純 TD

                        var tmp_inp = $("<input type='text' class='save_inp hidden' />");
                        var value = $(this).text().replace(/^(\s+)|(\s+)$|(,)/g, "");

                        if ($(this).hasClass("date-td")) {
                            tmp_inp.attr("value", value)
                                .attr("name", tableNames[i] + "[" + get_index + "]." + name);
                        }
                        else {
                            tmp_inp.attr("value", value)
                                .attr("name", tableNames[i] + "[" + get_index + "]." + name);
                        }

                        outputHtml += tmp_inp.get(0).outerHTML;
                    }
                })
            })
        });

        if (tmp_page_li.get(0) != null) {
            tmp_page_li.find("a").click();
        }
        $(".main-form").append(outputHtml);
    }

    this.do_GetGridTdSearchData = function (type, inp) {
        var index = $(".tr", inp.closest("table")).index(inp.closest(".tr"));
        switch (type) {
            case "Pid":
                return {
                    "auto-action": "sl00", "auto-target": ".table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Pid$Pid,.table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Pname$name",
                    "search-set": ".tr.edit td.td_Pname$name"
                };
                break;
            case "Org":
                return {
                    "auto-action": "sl306", "auto-target": ".table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Org$Org,.table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Orgname$name",
                    "search-set": ".tr.edit td.td_Orgname$name"
                };
                break;
            case "Part":
                return {
                    "auto-action": "sl305", "auto-target": ".table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Part$Part,.table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Partname$name",
                    "search-set": ".tr.edit td.td_Partname$name"
                };
                break;
        }
    }

    this.do_GetGridSearchIconData = function (type, inp) {
        var index = $(".tr", inp.closest("table")).index(inp.closest(".tr"));
        switch (type) {
            case "Pid":
                return {
                    "name": "Pid", "senddata": "#edit_inp$Pid", "search-target": ".table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Pid",
                    "right-view": "SAL00View"
                };
                break;
            case "Org":
                return {
                    "name": "Org", "senddata": "#edit_inp$Org", "search-target": ".table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Org",
                    "right-view": "SAL306View"
                };
                break;
            case "Part":
                return {
                    "name": "Part", "senddata": "#edit_inp$Part", "search-target": ".table_scrollBody:has(#edit_inp) .tr:eq(" + index + ") td.td_Part",
                    "right-view": "SAL305View"
                };
                break;
        }
    }

    // plugin 

    // 表格自動遞增序號(No)
    this.AutoInCreaseNo = function (get_tr, td_selector) {
        var prev_tr = get_tr.prev(".tr");
        var index = 0;

        if (prev_tr.length > 0) {
            index = prev_tr.find(td_selector).text() * 1;
        }

        index = index + 1;
        get_tr.find(td_selector).text(index);
    }
}

