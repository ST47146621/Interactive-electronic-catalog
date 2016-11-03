
(function ($, undefined) {
    $(document).delegate(".autofill", "change blur", function (e) {

        var $this = $(this);

        if (e.type == "change") {

            var action = $this.data("auto-action");
            var value;

            if ($this.get(0).tagName == "TD") {
                if ($this.find("input").get(0) == null) {
                    value = $this.text().replace(/^(\s+)|(\s+)$|(,)/g, "");
                }
                else {
                    value = $this.find("input").val();
                }
            }
            else {
                value = $this.val();
            }

            var target = $this.data("auto-target").split(",");

            $this.removeClass("autofill-error input-validation-error").removeData("error-msg");

            $.proxies.autofillapi[action](value).success(function (res) {

                var data = res.data;
                if (data.length > 0) {
                    data = data[0];

                    $.each(target, function (index, obj) {
                        var s = obj.split("$");
                        var selector = s[0], param = s[1];
                        if ($(selector).get(0).tagName == "TD") {
                            if ($(selector).find("input").get(0) != null) {
                                $(selector).find("input").val(data[param])
                            }
                            else {
                                $(selector).text(data[param]);
                            }
                        }
                        else {
                            $(selector).val(data[param]);
                        }
                    });
                }
                else {

                    var skipMsg = "查無此項目";

                    if (value.length > 0) {
                        $this.addClass("autofill-error")
                            .data("error-msg", skipMsg);

                        $.openErrorNoty(skipMsg);
                    }

                    $.each(target, function (index, obj) {
                        var s = obj.split("$");
                        var selector = s[0];
                        if (!$(selector).is($this)) {
                            if ($(selector).get(0).tagName == "TD") {
                                if ($(selector).find("input").get(0) != null) {
                                    $(selector).find("input").val("");
                                }
                                else {
                                    $(selector).text("");
                                }
                            }
                            else {
                                $(selector).val("");
                            }
                        }
                    });

                    $this.click().focus();
                }
            })
        }

        if (e.type == "focusout") {
            var errorMsg = $this.data("error-msg");
            if (errorMsg != null) {
                if ($this.data("hotkey") == 13) {
                    $.openErrorNoty(errorMsg);
                    $this.removeData("hotkey").click().focus();
                }
            }
        }

    })
})(jQuery)

