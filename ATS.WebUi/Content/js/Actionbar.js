$(document).ready(function () {
    var WindowWidth = $(window).width();
    if (WindowWidth > 767) {
        console.log(222);
        ResizeActionbar();
    }

    $(window).resize(function () {
        var WindowWidth = $(window).width();
        if (WindowWidth > 767) {
            console.log(444);
            ResizeActionbar();
        }
    });
    //$(window).resize(function () {
    //    var WindowWidth = $(window).width();
    //    if (WindowWidth > 767) {
    //        clearTimeout(resizeTimeout);
    //        resizeTimeout = setTimeout(function () {
    //            ResizeActionbar();
    //        }, 100);
    //    }
    //});
});

function ResizeActionbar() {
    var str = "";
    if ($(".ActionBar").hasClass("FreezeActionbar")) {
        str = "<ul id='DropToggle' class='DropMenu WithIcon FreezeDrpMore' style=''><li class='floatleft' style='position: relative;'><a href='#' class='drpMore'>&nbsp;</a><ul class='MobRight DrpResize'></ul></li></ul>";
    }
    else {
        str = "<ul id='DropToggle' class='DropMenu WithIcon' style=''><li class='floatleft' style='position: relative;'><a href='#' class='drpMore'>&nbsp;</a><ul class='MobRight DrpResize'></ul></li></ul>";
    }
    var WdParent = $(".contnt-right").width();
    if (WdParent > 319) {
        var WdChild = $(".Searchbox").outerWidth(true) + 55;
        $('.ResizableAction > li:visible').each(function () {
            WdChild += $(this).outerWidth(true);
        });
        if (WdChild > WdParent) {
            if ($(".ResizableAction").siblings("#DropToggle").length == 0) {
                $(str).insertAfter($(".ResizableAction"));
            }
            while (WdChild > WdParent) {
                $(".ResizableAction > li:last-child").clone().appendTo("#DropToggle .DrpResize");
                $(".ResizableAction > li:last-child").remove();
                $("#DropToggle .DrpResize > li > a").each(function () {
                    $(this).addClass("WithIcon");
                });

                var WdParent = $(".contnt-right").width();
                var WdParentPlus = WdParent - 175;
                var WdChild = $(".Searchbox").outerWidth(true) + 60;
                $('.ResizableAction > li').each(function () {
                    WdChild += $(this).outerWidth(true);
                });
            }
        }
        else if (WdParent > WdChild) {
            if (WdParent > (WdChild + 150)) {
                if ($("#DropToggle .DrpResize li").length > 0) {
                    $("#DropToggle .DrpResize > li:last-child").clone().appendTo(".ResizableAction");
                    $("#DropToggle .DrpResize > li:last-child").remove();
                }
            }
        }
        DOMTreeModified();
    }
}

function DOMTreeModified() {
    $("#DropToggle").show();
    var EnableElements = ($(".DrpResize").children("li:visible").has("a:visible").length);
    if (EnableElements == "0") {
        $("#DropToggle").hide();
    }
    else {
        $("#DropToggle").show();
    }
}