var calendar;

function do_SetCalendar(target) {
    if (target == null)
        target = $("#calendar_info");
    calendar = target.fullCalendar({
        theme: true,
        height: 400,
        timeFormat: 'HH:mm',
        header: {
            left: 'prev,next today ',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            prev: '&lt;',
            next: '&gt;',
            today: '今天',
            month: '月',
            week: '週',
            day: '日'
        },
        allDayText: "全天",
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月',
            '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['1', '2', '3', '4', '5', '6', '7',
            '8', '9', '10', '11', '12'],
        dayNames: ['星期日', '星期一', '星期二', '星期三',
            '星期四', '星期五', '星期六'],
        dayNamesShort: ['日', '一', '二', '三',
            '四', '五', '六'],
        columnFormat: {
            month: 'dddd',
            week: 'M/d ddd',
            day: 'M/d dddd'
        },
        titleFormat: {
            month: 'yyyy 年 MM 月',
            week: " yyyy 年 MMM 月 d 日{ '&#8212;'[ yyyy 年] [ MMM 月] d 日}",
            day: ' yyyy 年 MMM 月 d 日, dddd'
        },
        editable: false,
        weekMode: 'variable',
        events: function (start, end, callback) {
            $.proxies.calendarapi.getcalendarevent(new Date(start).toISOString(), new Date(end).toISOString()).done(function (response) {
                callback(response.data);
                $(window).resize(); //載入完成後重新設定大小
            });
        },
        eventClick: function (event) {
            if (event.url) {
                var url = target.data("url") + "?key=" + event.url;
                WinOpen(url, "Calendar-" + event.url, 0.7, 0.8);
                return false;
            }
        },
        viewRender: function (view, element) {
            //按鈕動態樣式(active、disabled等)
            $(".fc-header .fc-header-right .active", target).removeClass("active");
            $(".fc-header .fc-header-right .fc-button-" + view.name, target).toggleClass("ui-state-active active");
            $(".fc-header .fc-header-left .disabled", target).removeClass("disabled");
            $(".fc-header .fc-header-left .ui-state-disabled", target).toggleClass("ui-state-disabled disabled");
            $(".fc-day-number", target).each(function () {
                var $this = $(this);
                var date = new Date($this.parents(".fc-day").data("date"));
                if (date >= new Date(new Date().getNow())) {
                    $this.prepend("<span class='glyphicon glyphicon-plus addCalendar-icon'></span><span class='padding-right-5'><span>");
                }
            });
        }
    });

    //重新跟換標題按鈕樣式(更換為bootstrap的class)
    $(".fc-header-left", target).append($(".fc-button-append-left"));
    $(".fc-header-left,.fc-header-right", target).each(function () {
        var header = $(this);
        header.append("<div class='btn-toolbar' role='toolbar'><div class='btn-group'></div></div>");
        header.children("span").each(function () {
            if ($(this).is(".fc-header-space")) {
                header.find(".btn-toolbar").append("<div class='btn-group'></div>");
            }
            else {
                $(".btn-group:last", header).append($(this));
            }
        });
    });
    $(".fc-header-right .btn-toolbar", target).addClass("pull-right");
    $(".fc-header-space").remove();
    $(".fc-button", target).toggleClass("fc-button btn btn-default");
    $(".ui-icon", target).toggleClass("ui-icon glyphicon");
    $(".ui-icon-circle-triangle-w", target).toggleClass("ui-icon-circle-triangle-w glyphicon-chevron-left");
    $(".ui-icon-circle-triangle-e", target).toggleClass("ui-icon-circle-triangle-e glyphicon-chevron-right");
    $(".fc-icon-wrap", target).each(function () {
        $(this).parent().html($(this).html());
    });
    $(".ui-state-default,.ui-corner-left,.ui-corner-right", target).removeClass("ui-state-default ui-corner-left ui-corner-right");
}