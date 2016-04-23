
(function ($) {
    if (!$) {
        return;
    }

    $(function () {
        $(".ibox .ibox-title").drag(function (ev, pos) {
            $(this).parent().css({
                top: pos.offsetY,
                left: pos.offsetX
            });
        });
    });
})(jQuery);