
(function ($) {
    if (!$) {
        return;
    }

    $(function () {
        var h = $(window).height();
        $(".ah").each(function () {
            var rh = $(this).data("rh");
            if (rh == undefined) {
                rh = 0;
            }
            $(this).height(h + rh);
        });
    });
})(jQuery);