
$(function () {
    //var innerHeight = window.innerHeight;
    //$("#left-side").height(innerHeight - $("#left-side").offset().top);
    $(".side-title").click(function (e) {
        $(this).find("span").toggleClass("glyphicon-plus-sign glyphicon-minus-sign");

        var target = $(this).data("tree-target");
        $(target).toggleClass("collapse");

        e.preventDefault();
        return false;
    })
})