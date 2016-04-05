$(document).ready(function () {

    //Slide close on ESC
    $(document).keyup(function (e) {
        if (e.keyCode === 27 || e.which == 27) {
            var allow = true;
            if ($("#slidepanel-login").css("display") == "block") {
                $("#slideit").trigger("click");
                allow = false;
            }
            if ($("#slidepanel-signup").css("display") == "block" && allow) {
                $("#slideit1").trigger("click");
            }
        }

    });
    // Expand Panel LOGIN
    $("#slideit").click(function (event) {
        if ($("div#slidepanel-login").hasClass('opened')) {
            $("div#slidepanel-login").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#slidepanel-login").slideDown('slow').toggleClass('closed opened');
            if ($("div#slidepanel-signup").hasClass('opened')) {
                $("div#slidepanel-signup").slideUp('slow').toggleClass('closed opened');
            }
            if ($("div#languagelist").hasClass('opened')) {
                $("div#languagelist").slideUp('slow').toggleClass('closed opened');
            }
            $(".LoginForm #UserName").focus();

        }
        event.stopImmediatePropagation();
    });

    $("#slidepanel-login").click(function (event) {
        event.stopImmediatePropagation();
    });

    $("#slidepanel-signup").click(function (event) {
        event.stopImmediatePropagation();
    });

    $(document).click(function () {
        $("#slidepanel-login").slideUp('slow').removeClass('closed');
        $("#slidepanel-signup").slideUp('slow').removeClass('closed');
        $("#slidepanel-login").slideUp('slow').removeClass('opened');
        $("#slidepanel-signup").slideUp('slow').removeClass('opened');
    });

    // Expand Panel SIGNUP
    $("#slideit1").click(function (event) {
        if ($("div#slidepanel-signup").hasClass('opened')) {
            $("div#slidepanel-signup").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#slidepanel-signup").slideDown('slow').toggleClass('closed opened');
            if ($("div#slidepanel-login").hasClass('opened')) {
                $("div#slidepanel-login").slideUp('slow').toggleClass('closed opened');
            }
            if ($("div#languagelist").hasClass('opened')) {
                $("div#languagelist").slideUp('slow').toggleClass('closed opened');
            }
            $(".signup #UserName").focus();
        }
        event.stopImmediatePropagation();
    });

    //Expand My Language LANGUAGE
    $("#language-change").click(function () {
        if ($("div#languagelist").hasClass('opened')) {
            $("div#languagelist").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#languagelist").slideDown('slow').toggleClass('closed opened');
            if ($("div#slidepanel-login").hasClass('opened')) {
                $("div#slidepanel-login").slideUp('slow').toggleClass('closed opened');
            }
            if ($("div#slidepanel-signup").hasClass('opened')) {
                $("div#slidepanel-signup").slideUp('slow').toggleClass('closed opened');
            }
        }
    });


    //Expand search result
    $("#search-filter").click(function () {
        if ($("div#divsearchfiler").hasClass('opened')) {
            $("div#divsearchfiler").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#divsearchfiler").slideDown('slow').toggleClass('closed opened');
        }
    });

    //Expand My vaccancy
    $("#my-filter").click(function () {
        if ($("div#divmyvacancy").hasClass('opened')) {
            $("div#divmyvacancy").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#divmyvacancy").slideDown('slow').toggleClass('closed opened');
        }
    });



    $("#slideit2").click(function () {
        if ($("div#slidepanel2").hasClass('opened')) {
            $("div#slidepanel2").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#slidepanel2").slideDown('slow').toggleClass('closed opened');
        }
        //		$("div#slidepanel").slideDown("slow");

    });


    // Expand Panel
    $("#slideit3").click(function () {
        if ($("div#slidepanel3").hasClass('opened')) {
            $("div#slidepanel3").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#slidepanel3").slideDown('slow').toggleClass('closed opened');
        }
        //		$("div#slidepanel").slideDown("slow");

    });


    // Expand Panel
    $("#slideit4").click(function () {
        if ($("div#slidepanel4").hasClass('opened')) {
            $("div#slidepanel4").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#slidepanel4").slideDown('slow').toggleClass('closed opened');
        }
        //		$("div#slidepanel").slideDown("slow");

    });

    // Expand Panel
    $("#slideit5").click(function () {
        if ($("div#slidepanel5").hasClass('opened')) {
            $("div#slidepanel5").slideUp('slow').toggleClass('closed opened');
        } else {
            $("div#slidepanel5").slideDown('slow').toggleClass('closed opened');
        }
        //		$("div#slidepanel").slideDown("slow");

    });




    // Collapse Panel
    $("#closeit").click(function () {
        $("div#slidepanel").slideUp("slow");
    });

    // Switch buttons from "Log In | Register" to "Close Panel" on click
    $("#toggle a").click(function () {
        $("#toggle a").toggle();
    });

});