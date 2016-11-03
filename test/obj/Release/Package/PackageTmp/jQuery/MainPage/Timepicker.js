function do_SetTimePicker(target) {
    if (target.hasClass("hasTimepicker") == false) {
        target.timepicker({
            minTime: '00:00',
            //maxTime: '24:00',
            timeFormat: 'H:i'
        }).addClass("hasTimepicker")
            .on("changeTime", function (e) {
                var $this = $(this);
                if ($this.val() == "24:00") {
                    $this.val("23:59");
                }
            });

        target.nextAll(".time-picker-icon").unbind("click").bind("click", function () {
            var inp = $(this).prevAll("input:visible").click().focus();
        });
    }
}