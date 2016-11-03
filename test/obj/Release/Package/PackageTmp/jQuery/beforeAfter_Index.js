
       $('#container').beforeAfter({
           animateIntro: true,
           introDelay: 300,
           introDuration: 500,
           showFullLinks: false
       });
/*背景 可鎖定*/
var timer;
var speed = 100;

$(".plus-button").hover(function () {
    clearTimeout(timer);
    clearTimeout(timer2);
    var button = $(this);
}, function () {
    timer = setTimeout(autoShow, speed);
    timer2 = setTimeout(autoShow2, speed2);
});
var i = 0, j = 0, k = 0;
function autoShow() {
    $("#v1 .v1-3,#v2 .v1-3").animate({ "background-position": "-=4px" }, 80);
    $(".plus-button").animate({ "left": "-=4px", }, 80);
    i += 4;
    j += 4;
    if (k == 1) {
        if (i >= 2000) {
            $(".pb1").animate({ "top": "+=1000px" }, 80);
            $(".pb1").animate({ "left": "+=2000px" }, 80);
            $(".pb1").animate({ "top": "-=1000px" }, 80);
            i = 0;
        }
    }
    if (k == 0) {
        if (i >= 1000) {
            $(".pb1").animate({ "top": "+=1000px" }, 80);
            $(".pb1").animate({ "left": "+=2000px" }, 80);
            $(".pb1").animate({ "top": "-=1000px" }, 80);
            k = 1;
            i = 0;
        }
    }
    if (j >= 2000) {
        $(".pb2").animate({ "top": "+=1000px" }, 80);
        $(".pb2").animate({ "left": "+=2000px" }, 80);
        $(".pb2").animate({ "top": "-=1000px" }, 80);
        j = 0
    }
    timer = setTimeout(autoShow, speed);
}
autoShow();


/*背景 天地*/
var timer1;
var speed1 = 100;

function autoShow1() {
    $("#v1 .v1-1,#v2 .v1-1").animate({ "background-position": "-=2px" }, 80);
    $("#v1 .v1-4,#v2 .v1-4").animate({ "background-position": "+=1px" }, 80);
    timer1 = setTimeout(autoShow1, speed1);
}
autoShow1();


var timer2;
var speed2 = 3000;
function autoShow2() {
    $(".plus-button").animate({ "width": "30px" }, 100).animate({ "background-position": "-45px" }, 0);
    $(".plus-button").animate({ "width": "21px" }, 50).animate({ "background-position": "20px" }, 0);
    timer2 = setTimeout(autoShow2, speed2);
}
autoShow2();


$("#v1 .v1-8").hide(); $("#v2 .v1-8").hide();
$("#v1 .v1-5").mouseenter(function () { $("#v1 .v1-8").show(300); $("#v2 .v1-8").show(); });
$("#v1 .v1-5").mouseleave(function () { $("#v1 .v1-8").hide(); $("#v2 .v1-8").hide(); });
$("#v1 .v1-5").click(function () { $("#v1 .message").show(); $("#v2 .message").show(); });
$("#v1 .v1-7").click(function () { $("#v1 .message").hide(); $("#v2 .message").hide(); });
$("#v1 .v1-7").mouseenter(function () { $("#v1 .v1-7").css({ "background-position": "0px" }); });
$("#v1 .v1-7").mouseleave(function () { $("#v1 .v1-7").css({ "background-position": "15px" }); });

$("#v2 .v1-5").mouseenter(function () { $("#v1 .v1-8").show(); $("#v2 .v1-8").show(); });
$("#v2 .v1-5").mouseleave(function () { $("#v1 .v1-8").hide(); $("#v2 .v1-8").hide(); });
$("#v2 .v1-5").click(function () { $("#v1 .message").show(); $("#v2 .message").show(); });
$("#v2 .v1-7").click(function () { $("#v1 .message").hide(); $("#v2 .message").hide(); });
$("#v2 .v1-7").mouseenter(function () { $("#v2 .v1-7").css({ "background-position": "0px" }); });
$("#v2 .v1-7").mouseleave(function () { $("#v2 .v1-7").css({ "background-position": "15px" }); });
$("#down .db").mouseenter(function () { var db = $(this); db.animate({ "background-position": "+=162px" }, 100) });
$("#down .db").mouseleave(function () { var db = $(this); db.animate({ "background-position": "-=162px" }, 100) });
$("#down .db").click(function () { var db = $(this); $("#down .db2").css({ "background-position": "-224px" }); $("#down .db").css({ "background-position": "-324px" }); db.css({ "background-position": "0px" }); });

$("#down .db2").mouseenter(function () { var db2 = $(this); db2.animate({ "background-position": "+=112px" }, 100) });
$("#down .db2").mouseleave(function () { var db2 = $(this); db2.animate({ "background-position": "-=112px" }, 100) });
$("#down .db2").click(function () { var db2 = $(this); $("#down .db2").css({ "background-position": "-224px" }); $("#down .db").css({ "background-position": "-324px" }); db2.css({ "background-position": "0px" }); });
