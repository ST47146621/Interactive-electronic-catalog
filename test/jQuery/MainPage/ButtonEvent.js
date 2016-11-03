
(function ($, undefined) {

    $(document).delegate("#new-btn:visible", "click", function () {
        var result = do_PowerCheck("added");
        if (result) {
            canModify = true;

            var formSelector = $(this).data("form");
            var form = $(formSelector);

            $("#button-bar .btn-item").toggleClass("hidden");
            $("input,select,textarea", form).removeAttr("disabled");
            $("input:text,input:hidden,textarea", form).val("");
            $("input:checked", form).removeAttr("checked");
            $("select option", form).removeAttr("selected");
            $("select option:first-child", form).attr("selected", "selected");
            //$(".key-field", form).attr("readonly", "readonly");
            $(".key-field", form).next(".SearchIcon").addClass("hidden");

            if (typeof (gridTable) != "undefined")
                gridTable.do_ClearGridTable();

            if (typeof (controller.do_NewButtonClick) == "function")
                controller.do_NewButtonClick(form);

            if (typeof (controller.do_PageModify) == "function")
                controller.do_PageModify(form);
        }
    });

    $(document).delegate("#edit-btn:visible", "click", function () {
        var result = do_PowerCheck("edit");
        if (result) {
            var settings = {
                before: function (get_tr) {
                    var defferred = $.Deferred();
                    return defferred.resolve();
                }
            }

            if (typeof (controller.do_BeforeEditCheck) == "function") {
                settings.before = controller.do_BeforeEditCheck;
            }

            var promise = settings.before();

            var formSelector = $(this).data("form");
            var form = $(formSelector);

            promise.done(function () {

                canModify = true;

                $("#button-bar .btn-item").toggleClass("hidden");
                $("input,select,textarea", form).removeAttr("disabled");
                $(".key-field", form).attr("readonly", "readonly");
                $(".key-field", form).next(".SearchIcon").addClass("hidden");

                if (typeof (controller.do_PageModify) == "function")
                    controller.do_PageModify(form);
            })
            .fail(function (xhr) {
                var noty = $.openAjaxErrorNoty(xhr);
                return false;
            });
        }
    });

    $(document).delegate("#save-btn:visible", "click", function () {

        var formSelector = $(this).data("form");
        var form = $(formSelector);

        form.find(".autofill-error").each(function () {
            $(this).addClass("input-validation-error");
        });

        if (form.find(".input-validation-error").length == 0) {
            if (form.valid()) {
                controller.do_SaveButtonClick(form);
            }
        }
        else {
            $(".input-validation-error:eq(0)").click().focus();
        }
    });

    $(document).delegate("#delete-btn:visible", "click", function () {
        var result = do_PowerCheck("del");
        if (result) {
            controller.do_bindDeleteCheck($(this));
        }
    });

    $(document).delegate("#print-btn:visible", "click", function () {
        var result = do_PowerCheck("prt");
        if (result) {
            //controller.do_bindPrint();
        }

    });

    $(document).delegate("#esc-btn:visible", "click", function () {
        if (opener != null || $("#cancel-btn").data("toClose") || $(".home-link").length == 0) {
            if (isChange) {
                var $noty = $.openChangeConfirmNoty($this, function () {
                    window.close();
                });
            }
            else {
                window.close();
            }
        }
        else {
            $(".home-link").get(0).click();
        }
    });

    $(document).delegate("#cancel-btn:visible", "click", function () {

        //TODO: 多國語言
        var $this = $(this);
        if (isChange) {
            var $noty = $.openChangeConfirmNoty($this, function () {
                canModify = false;
                if ($this.data("toClose")) {
                    isChange = false;
                    $("#esc-btn").removeClass("hidden").click();
                }
                else {
                    $("#refresh-page").click();
                }
            });
        }
        else {
            canModify = false;
            if ($this.data("toClose")) {
                $("#esc-btn").removeClass("hidden").click();
            }
            else {
                $("#refresh-page").click();
            }
        }
    });

})(jQuery);

function do_beforDelete(table, keyname, keyvalue, compare, options) {
    if (compare == null)
        compare = -1;

    var defaults = {
        success: function (response) {
            do_delete([table], [keyname], keyvalue, response.data);
        }
    }
    var settings = $.extend(defaults, options);

    $.proxies.servicesapi.beforedelete({
        table: table,
        keyname: keyname,
        keyvalue: keyvalue,
        compare: compare
    }).done(function (response) {
        settings.success(response);
    }).fail(function (xhr) {
        var noty = $.openAjaxErrorNoty(xhr);
    });
}

function do_delete(tables, keynames, keyvalue, preValue, options) {
    var defaults = {
        success: function (response) {
            do_ajax_page(preValue);
        }
    }
    var settings = $.extend(defaults, options);

    $.proxies.servicesapi.deletedata({
        tables: tables,
        keynames: keynames,
        keyvalue: keyvalue
    }).done(function (response) {
        settings.success(response);
    }).fail(function (xhr) {
        var noty = $.openAjaxErrorNoty(xhr);
    });
}
