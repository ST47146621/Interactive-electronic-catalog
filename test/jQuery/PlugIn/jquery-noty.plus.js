
(function ($, undefined) {

    // TODO: 多國語言

    // 成功視窗
    $.openSucessNoty = function (text, options) {
        text = text || "儲存成功";
        var defaul_options = {
            type: "success",
            text: "<h4><b>" + text + "</b></h4>",
            dismissQueue: false,
            animation: {
                open: { height: 'toggle' },
                close: { height: 'toggle' },
                easing: 'swing',
                speed: 500
            },
            timeout: 1500
        }
        var final_options = $.extend(defaul_options, options);

        var sucessNoty = noty(final_options);
        return sucessNoty;
    }

    // 失敗視窗
    $.openErrorNoty = function (text, options) {

        text = text || "作業失敗";

        var defaul_options = {
            type: "error",
            text: text,
            dismissQueue: false,
            killer: true,
            animation: {
                open: { height: 'toggle' },
                close: { height: 'toggle' },
                easing: 'swing',
                speed: 0
            },
            timeout: 1500
        }

        var final_options = $.extend(defaul_options, options);

        var errorNoty = noty(final_options);
        return errorNoty;
    }

    // Ajax 失敗
    $.openAjaxErrorNoty = function (xhr, options) {
        var result = xhr.responseJSON;
        var errorMessage = (typeof(result) != "undefined") ? result.customerMessage : "系統發生錯誤";

        var defaults = {
            type: "error",
            text: errorMessage
        };

        var settings = $.extend(defaults, options);
        var errorNoty = noty(settings);
        return errorNoty;
    }

    // 刪除確認視窗
    $.oepnDeleteConfirmNoty = function ($this, success, cancel) {
        var text = "<h4><b>確定刪除此筆明細資料？</b></h4>";
        if ($this.is("#delete-btn"))
            text = "<h4><b>確定刪除此筆資料？</b></h4>";
        if (typeof (cancel) == "undefined")
            cancel = function () { };

        noty({
            text: text,
            modal: true,
            animation: {
                open: { height: 'toggle' },
                close: { height: 'toggle' },
                easing: 'swing',
                speed: 0
            },
            buttons: [
              {
                  addClass: 'btn btn-primary ',
                  text: '<span class="glyphicon glyphicon-ok"></span>',
                  onClick: function ($noty) {
                      if ($this.is("#delete-btn")) {
                          success($this);
                      }
                      else {
                          var get_tr = $this.closest("tr");
                          success(get_tr);
                      }
                      $noty.close();
                  }
              },
              {
                  addClass: 'btn btn-danger',
                  text: '<span class="glyphicon glyphicon-remove"></span>',
                  onClick: function ($noty) {
                      cancel();
                      $noty.close();
                  }
              }
            ],
            closeWith: ['button'],
            callback: {
                onShow: function () {
                    $this.attr("disabled", "disabled");
                },
                onClose: function () {
                    $this.removeAttr("disabled");
                }
            }
        });
    }


    $.oepnConfirmNoty = function (text, options) {

        var defaul_options = {
            type: "confirm",
            text: text,
            modal: true
        }

        var final_options = $.extend(defaul_options, options);

        var confirmNoty = noty(final_options);
        return confirmNoty;
    }

    $.openChangeConfirmNoty = function ($this, success, cancel) {

        var confirmNoty = noty({
            type: "confirm",
            text: "<h4>資料尚未儲存，是否放棄編輯?</h4>",
            modal: true,
            buttons: [
                {
                    addClass: 'btn btn-primary ',
                    text: '<span class="glyphicon glyphicon-ok"></span>',
                    onClick: function ($noty) {
                        if (typeof (success) == "function")
                            success($noty);
                        $noty.close();
                    }
                },
                {
                    addClass: 'btn btn-danger',
                    text: '<span class="glyphicon glyphicon-remove"></span>',
                    onClick: function ($noty) {
                        if (typeof (cancel) == "function")
                            cancel($noty);
                        $noty.close();
                    }
                }
            ]
        });
        return confirmNoty;
    }

    var $loadingNoty;
    $.openLoadingNoty = function () {
        $loadingNoty = noty({
            text: '<h4><span class="glyphicon glyphicon-refresh" style="font-size: 20px; margin-right: 5px;"></span>Loading</h4>',
            type: "information",
            //modal: true,
            animation: {
                open: { height: 'toggle' },
                close: { height: 'toggle' },
                easing: 'swing',
                speed: 0
            },
            closeWith: [],
            callback: {
                onShow: function () {
                    $(this.$message).find("span").rotateAnimate();
                    $("body").addClass("hasCustomerModal").append('<div class="CustomerModal-Bg"></div>')
                },
                onClose: function () {
                    $("body").removeClass("hasCustomerModal").find(".CustomerModal-Bg").remove();
                }
            }
        });
    }

    $.closeLoadingNoty = function () {
        if (typeof ($loadingNoty) != "undefined") {
            $loadingNoty.close();
        }
    }

    $.fn.rotateAnimate = function () {
        var target = $(this), degree = 0, timer;
        rotate();
        function rotate() {
            target.css({
                "transform": 'rotate(' + degree + 'deg)',
                '-ms-transform': 'rotate(' + degree + 'deg)',
                '-webkit-transform': 'rotate(' + degree + 'deg)',
                '-moz-transform': 'rotate(' + degree + 'deg)'
            });
            timer = setTimeout(function () {
                ++degree; rotate();
            }, 5);
        }
    }
})(jQuery);