
//綁定日期選擇器
(function do_BindDatePicker() {

    $(document).delegate(".date_picker", "focus", function () {
        if (typeof (canModify) === "undefined" || canModify == true)
            do_SetDatePicker($(this));
    });

    $(document).delegate(".input-group.date", "focus click", function () {
        if (typeof (canModify) === "undefined" || canModify == true)
            do_SetDatePicker($(this));
    });
})()

//初始化日期選擇器
function do_SetDatePicker(target) {

    if (target == null) {
        target = $(".date_picker");
    }

    var inp = target;
    if (target.is(".input-group"))
        inp = target.find("input");

    inp.each(function () {
        $(this).data("old-date", $(this).val());
    });

    if (target.hasClass("hasDatepicker") == false) {
        inp.bind("keydown", function (e) {
            if (e.keyCode == 13)
                inp.data("dateHotKey", 13);
        });
        target.datepicker({
            format: 'yyyy/mm/dd',
            autoclose: true,
            todayBtn: true,
            clearBtn: target.is(".input-group"),
            language: "zh-TW",
            keyboardNavigation: false
        }).addClass("hasDatepicker")
            .on("clearDate", function (e) {
                var $this = $(this);
                var $thisInp = $this;
                if ($this.is(".input-group"))
                    $thisInp = target.find("input");
                if ($thisInp.data("dateHotKey") == 13) {
                    $thisInp.removeData("dateHotKey");
                    target.datepicker("update", $thisInp.data("old-date"));
                }
            })
            .on("show", function (e) {
                var $this = $(this);
                var $thisInp = $this;
                if ($this.is(".input-group"))
                    $thisInp = target.find("input");
                $thisInp.removeData("dateHotKey");
            });
    }
}