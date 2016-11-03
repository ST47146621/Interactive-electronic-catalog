
(function do_InitialPageKeyPress($, undefined) {

    $(document).delegate("input,select,textarea", "change", function () {
        isChange = true;
    });

    $(document).delegate("input:text:visible", "keydown", function (e) {

        var enable = false, index = 0;

        switch (e.keyCode) {
            case 13: {
                enable = true;
                index = $(this).index("input:text:visible") + 1;
                $(this).data("hotkey", 13);
                break;
            }
            case 37: {
                if ($(this).getCursorPosition() == 0) {
                    enable = true;
                    index = $(this).index("input:text:visible") - 1;
                }
                break;
            }

        }

        if (enable) {
            var inp = $("input:text:visible").eq(index);
            if (inp.hasClass("fmoney"))
                inp.select();
            else
                inp.focus();

            e.preventDefault();
        }
        else {
            return;
        }
    });

})(jQuery);