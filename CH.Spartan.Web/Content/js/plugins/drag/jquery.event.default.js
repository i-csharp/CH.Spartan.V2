
(function ($) {
    if (!$) {
        return;
    }

    $(function () {
        $(".ibox .ibox-title.drag").drag(function (ev, pos) {
            $(this).css({ "cursor": "move" }).parent().css({
                top: pos.offsetY,
                left: pos.offsetX
            });
        });
    });
})(jQuery);