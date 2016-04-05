jQuery.QapTcha = {
    build: function (options) {
     
        var defaults = {
            txtLock: 'Locked : Please move slider to the right',
            txtUnlock: 'Phew! You are not a robot.',
            disabledSubmit: true,
            autoRevert: false,
            PHPfile: 'php/Qaptcha.jquery.php',
            readOnly: false
        };
        if (this.length > 0)
            return jQuery(this).each(function (i) {
                /** Vars **/
                var 
opts = $.extend(defaults, options),
$this = $(this),
form = $('form').has($this),
Clr = jQuery('<div>', { 'class': 'clr' }),
bgSlider = jQuery('<div>', { id: 'bgSlider' }),
Slider = jQuery('<div>', { id: 'Slider' }),
Icons = jQuery('<div>', { id: 'Icons' }),
TxtStatus = jQuery('<div>', { id: 'TxtStatus', 'class': 'dropError', text: opts.txtLock }),
inputQapTcha = jQuery('<input>', { name: 'iQapTcha', value: generatePass(), type: 'hidden' });
                /** Disabled submit button **/
                //if(opts.disabledSubmit) form.find('input[type=\'submit\']').attr('disabled','disabled');
                /*changed by Lena*/
                if (opts.disabledSubmit) {
                    $('#btnSubmit').attr('disabled', 'disabled');
                    //$(".PageSplitPrevButton", "#clickdimensionsForm")
                }
                /** Construct DOM **/
                bgSlider.appendTo($this);
                Icons.insertAfter(bgSlider);
                Clr.insertAfter(Icons);
                TxtStatus.insertAfter(Clr);
                inputQapTcha.appendTo($this);
                Slider.appendTo(bgSlider);
                $this.show();
                //e.pageX + ", " + e.pageY
                if (opts.readOnly !== true) {
                    $("#Icons").bind("click", function (e) {
                        Slider.css("left", "153px");
                        enableAction();
                    });
                    Slider.draggable({
                        revert: function () {
                            if (opts.autoRevert) {
                                if (parseInt(Slider.css("left")) > 150) return false;
                                else return true;
                            }
                        },
                        //containment: bgSlider,
                        axis: 'x',
                        
                        containment: ".QapTcha",
                        scroll: false,
                        stop: function (event, ui) {
                            if (ui.position.left > 150) {

                                //                                 set the SESSION iQaptcha in PHP file
                                //                                /*comented by Lena*/
                                //                                 $.post(opts.PHPfile,{
                                //                                 action : 'qaptcha'
                                //                                 },
                                //function(data) {
                                //if(!data.error)
                                //{
                                Slider.draggable('disable').css('cursor', 'default');
                                inputQapTcha.val("");
                                TxtStatus.text(opts.txtUnlock).addClass('dropSuccess').removeClass('dropError');
                                Icons.css('background-position', '-16px 0');
                                $('#btnSubmit').removeAttr('disabled');
                                //}
                                //},'json');
                                enableAction();
                            }
                        }
                    });
                }
                function enableAction() {
                    Slider.draggable('disable').css('cursor', 'default');
                    inputQapTcha.val("");
                    TxtStatus.text(opts.txtUnlock).addClass('dropSuccess').removeClass('dropError');
                    Icons.css('background-position', '-16px 0');
                    //$('#btnSubmit').removeAttr('disabled');
                    if (opts.readOnly !== true) {
                        $("#btnSubmit").prop("disabled", false);
                    }
                }
                function generatePass() {
                    var chars = 'azertyupqsdfghjkmwxcvbn23456789AZERTYUPQSDFGHJKMWXCVBN';
                    var pass = '';
                    for (i = 0; i < 10; i++) {
                        var wpos = Math.round(Math.random() * chars.length);
                        pass += chars.substring(wpos, wpos + 1);
                    }
                    return pass;
                }
            });
    }
}; jQuery.fn.QapTcha = jQuery.QapTcha.build;

jQuery.QapTcha1 = {
    build: function (options) {

        var defaults = {
            txtLock: 'Locked : Please move slider to the right',
            txtUnlock: 'Phew! You are not a robot.',
            disabledSubmit: true,
            autoRevert: false,
            PHPfile: 'php/Qaptcha.jquery.php',
            readOnly: false
        };
        if (this.length > 0)
            return jQuery(this).each(function (i) {
                /** Vars **/
                var
opts = $.extend(defaults, options),
$this = $(this),
form = $('form').has($this),
Clr = jQuery('<div>', { 'class': 'clr' }),
bgSlider = jQuery('<div>', { id: 'bgSlider' }),
Slider = jQuery('<div>', { id: 'Slider' }),
Icons = jQuery('<div>', { id: 'Icons' }),
TxtStatus = jQuery('<div>', { id: 'TxtStatus', 'class': 'dropError', text: opts.txtLock }),
inputQapTcha = jQuery('<input>', { name: 'iQapTcha', value: generatePass(), type: 'hidden' });
                /** Disabled submit button **/
                //if(opts.disabledSubmit) form.find('input[type=\'submit\']').attr('disabled','disabled');
                /*changed by Lena*/
                if (opts.disabledSubmit) {
                    $('#btnSub').attr('disabled', 'disabled');
                    //$(".PageSplitPrevButton", "#clickdimensionsForm")
                }
                /** Construct DOM **/
                bgSlider.appendTo($this);
                Icons.insertAfter(bgSlider);
                Clr.insertAfter(Icons);
                TxtStatus.insertAfter(Clr);
                inputQapTcha.appendTo($this);
                Slider.appendTo(bgSlider);
                $this.show();
                //e.pageX + ", " + e.pageY
                if (opts.readOnly !== true) {
                    $("#Icons").bind("click", function (e) {
                        Slider.css("left", "153px");
                        enableAction();
                    });
                    Slider.draggable({
                        revert: function () {
                            if (opts.autoRevert) {
                                if (parseInt(Slider.css("left")) > 150) return false;
                                else return true;
                            }
                        },
                        //containment: bgSlider,
                        axis: 'x',

                        containment: ".QapTcha1",
                        scroll: false,
                        stop: function (event, ui) {
                            if (ui.position.left > 150) {

                                //                                 set the SESSION iQaptcha in PHP file
                                //                                /*comented by Lena*/
                                //                                 $.post(opts.PHPfile,{
                                //                                 action : 'qaptcha'
                                //                                 },
                                //function(data) {
                                //if(!data.error)
                                //{
                                Slider.draggable('disable').css('cursor', 'default');
                                inputQapTcha.val("");
                                TxtStatus.text(opts.txtUnlock).addClass('dropSuccess').removeClass('dropError');
                                Icons.css('background-position', '-16px 0');
                                $('#btnSub').removeAttr('disabled');
                                //}
                                //},'json');
                                enableAction();
                            }
                        }
                    });
                }
                function enableAction() {
                    Slider.draggable('disable').css('cursor', 'default');
                    inputQapTcha.val("");
                    TxtStatus.text(opts.txtUnlock).addClass('dropSuccess').removeClass('dropError');
                    Icons.css('background-position', '-16px 0');
                    //$('#btnSubmit').removeAttr('disabled');
                    if (opts.readOnly !== true) {
                        $("#btnSub").prop("disabled", false);
                    }
                }
                function generatePass() {
                    var chars = 'azertyupqsdfghjkmwxcvbn23456789AZERTYUPQSDFGHJKMWXCVBN';
                    var pass = '';
                    for (i = 0; i < 10; i++) {
                        var wpos = Math.round(Math.random() * chars.length);
                        pass += chars.substring(wpos, wpos + 1);
                    }
                    return pass;
                }
            });
    }
}; jQuery.fn.QapTcha1 = jQuery.QapTcha1.build;