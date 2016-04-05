function VacancyModel() { }
VacancyModel.DateFormat = 'mm/dd/yy';
VacancyModel.PopUpDialog = null;
VacancyModel.PopUpButtons = [];
VacancyModel.ScaleScore = {
    from: 0, to: 100, step: 1, smooth: true, round: 1, skin: "plastic",
    onstatechange: function (value) {
        VacancyModel.ChangeScore(value);
    }
};
VacancyModel.ChangeScore = function (value) {
    if (window['ChangeScore'] != undefined) {
        window['ChangeScore'](value);
    }
}

VacancyModel.AccFirstIcon = {
    header: "iconOpen",
    activeHeader: "iconClosed"
}
VacancyModel.AccSecondIcon = {
    header: "iconOpen12",
    activeHeader: "iconClosed12"
}
VacancyModel.AccFirstAttr =
       {
           icons: VacancyModel.AccFirstIcon,
           heightStyle: "content",
           collapsible: true,
           active: false,
           animate: true
       }
VacancyModel.AccSecondAttr =
       {
           icons: VacancyModel.AccSecondIcon,
           heightStyle: "content",
           collapsible: true,
           active: false,
           animate: true
       }

VacancyModel.Pad = function (str, max) {
    str = str.toString();
    return str.length < max ? VacancyModel.Pad("0" + str, max) : str;
}
VacancyModel.RegisterDialog = function (dialogId, AutoOpen, AllowResizable, Modal) {

    VacancyModel.PopUpButtons = [];
    $('#' + dialogId).dialog({
        autoOpen: AutoOpen,
        resizable: AllowResizable,
        position: ['center', 'middle'],
        modal: Modal,
        buttons: VacancyModel.PopUpButtons,
        close: function () {
            $(this).dialog("close");
        }
    });
    VacancyModel.PopUpDialog = $('#' + dialogId);
}
VacancyModel.DeactivationConfirm = function (element, msg) {
    if ($(element).is(':unchecked')) {
        VacancyModel.ConfirmDialog("ConfirmDiv", "Deactivate", 400, msg, "Deactivate", "Cancel", "Deactivate", element, null);
    }
}
VacancyModel.MakeFormReadOnlybyFormObject = function (element) {
    element.find('input:text').attr('readonly', 'readonly');
    element.find('option').attr('disabled', 'disabled');
    element.find('textarea').attr('readonly', 'readonly');
    element.find('input:checkbox').attr("disabled", true);

}
VacancyModel.MakeFormReadOnly = function (formId) {
    $('#' + formId + ' input:text').attr('readonly', 'readonly');
    $('#' + formId + ' option').attr('disabled', 'disabled');
    $('#' + formId + ' textarea').attr('readonly', 'readonly');
    $('#' + formId + ' input:checkbox').attr("disabled", true);
    $('#' + formId + ' a.permission-link').hide();
}

VacancyModel.MakeVacancyFormReadOnly = function (formId) {
    $("textarea[iseditable*='NotEditable'],input[iseditable*='NotEditable']").each(function (index) {
        $(this).attr('readonly', 'readonly');
    });


    $("select[iseditable*='NotEditable']").each(function (index) {

        //$(this).find('option:not(:selected)').attr('disabled', 'disabled');
        $(this).find('option:not(:selected)').remove();
    });
}

VacancyModel.DestroyDialog = function () {
    VacancyModel.PopUpDialog.dialog("destroy");
    VacancyModel.PopUpDialog.html("");
}

VacancyModel.AddButtons = function (Text, Functions) {
    VacancyModel.PopUpButtons.push({
        text: Text,
        click: Functions
    });
}
VacancyModel.AddCloseButton = function (Text) {
    VacancyModel.PopUpButtons.push({
        text: Text,
        click: function () { $(this).dialog("close"); }
    });
}
VacancyModel.CloseDialog = function () {
    VacancyModel.DestroyDialog();
    VacancyModel.RegisterDialog('dialog', false, false, true);
}
VacancyModel.OpenDialog = function (DialogTitle, DialogWidth, CreateCloseButton, DialogMessage, Link) {
    VacancyModel.PopUpDialog.dialog({ title: DialogTitle });

    VacancyModel.PopUpDialog.dialog({ width: DialogWidth });

    if (Link != undefined) {

        var $title = VacancyModel.PopUpDialog.dialog().prev().children('span')


        $title.append($(Link));
    }
    if (CreateCloseButton == true) {

        VacancyModel.AddCloseButton("OK");
        VacancyModel.RefreshDialogButton();
    }
    VacancyModel.PopUpDialog.html(DialogMessage);
    VacancyModel.PopUpDialog.dialog('open');
}
VacancyModel.ConfirmDialog = function (DialogId, DialogTitle, DialogWidth, DialogMessage, ConfirmButtonText, CancelButtonText, ResultFunction, Myelement, RemoveDivName) {

    VacancyModel.RegisterDialog(DialogId, false, false, true);
    VacancyModel.PopUpDialog.dialog({ title: DialogTitle });
    VacancyModel.PopUpDialog.dialog({ width: DialogWidth });
    VacancyModel.PopUpDialog.html(DialogMessage);

    VacancyModel.AddButtons(ConfirmButtonText, function () { $(this).dialog("close"); window[ResultFunction](true, Myelement, RemoveDivName); });
    VacancyModel.AddButtons(CancelButtonText, function () { $(this).dialog("close"); window[ResultFunction](false, Myelement, RemoveDivName); });

    VacancyModel.RefreshDialogButton();

    VacancyModel.PopUpDialog.dialog('open');
}

VacancyModel.NotificationDialog = function (DialogId, DialogTitle, DialogWidth, DialogMessage, ConfirmButtonText) {

    VacancyModel.DestroyDialog();
    VacancyModel.RegisterDialog(DialogId, false, false, true);
    VacancyModel.PopUpDialog.dialog({ title: DialogTitle });
    VacancyModel.PopUpDialog.dialog({ width: DialogWidth });
    VacancyModel.PopUpDialog.html(DialogMessage);

    VacancyModel.AddButtons(ConfirmButtonText, function () { $(this).dialog("close"); });
    VacancyModel.RefreshDialogButton();

    VacancyModel.PopUpDialog.dialog('open');
}
VacancyModel.ConfirmDialog1 = function (DialogId, DialogTitle, DialogWidth, DialogMessage, ConfirmButtonText, CancelButtonText, ResultFunction) {

    VacancyModel.RegisterDialog(DialogId, false, false, true);
    VacancyModel.PopUpDialog.dialog({ title: DialogTitle });
    VacancyModel.PopUpDialog.dialog({ width: DialogWidth });
    VacancyModel.PopUpDialog.html(DialogMessage);

    VacancyModel.AddButtons(ConfirmButtonText, function () { $(this).dialog("close"); window[ResultFunction](true); });
    VacancyModel.AddButtons(CancelButtonText, function () { $(this).dialog("close"); window[ResultFunction](false); });

    VacancyModel.RefreshDialogButton();

    VacancyModel.PopUpDialog.dialog('open');


}

VacancyModel.RefreshDialogButton = function () {
    VacancyModel.PopUpDialog.dialog("option", "buttons", VacancyModel.PopUpButtons);
}
VacancyModel.OpenPostRequestForm = function (obj) {

    VacancyModel.DestroyDialog();
    VacancyModel.RegisterDialog('dialog', false, false, true);
    var allow = true;
    if ($("#slidepanel-login").css("display") == "block") {
        $("#slideit").trigger("click");
        allow = false;
    }
    if ($("#slidepanel-signup").css("display") == "block" && allow) {
        $("#slideit1").trigger("click");
    }
    var linkObj = $(obj);

    var DialogTitle = linkObj.attr('dialog-title') != undefined ? linkObj.attr('dialog-title') : linkObj.prop('title');
    var DialogTitleLink = linkObj.attr('dialog-title-link');
    if (DialogTitleLink != undefined)
        var link = document.createElement('a');
    var span = document.createElement('span');
    var LinkText = document.createTextNode(DialogTitleLink);
    $(link).attr('href', '#').attr('id', 'linkD');
    $(link).append($(span).append($(LinkText)));
    //
    var DialogCompanyName = linkObj.attr('dialog-CompanyName');
    var ViewLink = linkObj.attr('href');
    var separator = ViewLink.indexOf('?') >= 0 ? '&' : '?';
    var DialogWidth = linkObj.attr('dialog-width') != undefined ? parseInt(linkObj.attr('dialog-width').toString()) : 650;
    var DialogWithClose = linkObj.attr('dialog-close-button');
    DialogWithClose = DialogWithClose == 'true' ? true : false;

    $.get(ViewLink + separator + 'content=1', {}, function (data) {

        VacancyModel.PopUpDialog.html(data);
        if (DialogTitleLink == undefined) {
            VacancyModel.OpenDialog(DialogTitle, DialogWidth, DialogWithClose);
        }
        else {
            var Dialogemessage = undefined;
            VacancyModel.OpenDialog(DialogTitle, DialogWidth, DialogWithClose, Dialogemessage, $(link));
        }
    });
    return false;
}
VacancyModel.DisplaySuccessMeesage = function (element, message, viewinmiliseconds) {

    $(element).css("background", "#DFF2BF url('/content/images/success.png') no-repeat 0% 50%");
    $(element).css("color", "#4F8A10");
    VacancyModel.RegisterationSuccess($(element), message, '#a6ca8a');
    //$(element).empty();
    //$(element).append('<p id="success" class="info"><span class="info_inner">' + message + '</span></p>');
    //$(element).hide().fadeIn().delay(viewinmiliseconds).fadeOut('fast');

}
VacancyModel.ProfileSuccessMeesage = function (element, message, viewinmiliseconds) {
    $(element).empty();
    $(element).append('<span style="color:green">' + message + '</span>');
    $(element).hide().fadeIn().delay(viewinmiliseconds).fadeOut('fast');

}
VacancyModel.ProfileErrorMeesage = function (element, message, viewinmiliseconds) {
    $(element).empty();
    $(element).append('<span style="color:red">' + message + '</span>');
    $(element).hide().fadeIn().delay(viewinmiliseconds).fadeOut('fast');

}

VacancyModel.RegisterationSuccess = function (element, message, color) {
    $(element).empty();
    $(element).css("border-top", "3px solid " + color);
    $(element).css("border-bottom", "3px solid " + color);
    $(element).html("<b>" + message + "</b>");
    $(element).delay(100).slideDown(400).delay(2000).slideUp(400);
}
VacancyModel.DisplayProfileNotification = function (data, deletelink) {
    VacancyModel.DisplayProfileCommonNotification(data, $($(deletelink).parent().parent().find(".Save")));
}
VacancyModel.DisplayProfileCommonNotification = function (data, savelink) {
    try {

        if (!data.IsError) {
            savelink.hide();
            VacancyModel.ProfileSuccessMeesage($($(savelink).parent().parent().find(".notification")), data.Message, 3000);
        }
        else {
            VacancyModel.ProfileErrorMeesage($($(savelink).parent().parent().find(".notification")), data.Message, 3000);
        }
    }
    catch (err) {
        VacancyModel.ProfileErrorMeesage($($(savelink).parent().parent().find(".notification")), err.message, 3000);

        // Block of code to handle errors
    }
}

VacancyModel.DisplayErrorMeesage = function (element, message, viewinmiliseconds) {

    $(element).css("background", "#FFBABA url('/content/images/error.png') no-repeat 0% 50%");
    $(element).css("color", "#9F6000");
    VacancyModel.RegisterationSuccess($(element), message, '#f5aca6');

    //$(element).empty();
    //$(element).append('<p id="error" class="info"><span class="info_inner">' + message + '</span></p>');
    //$(element).hide().fadeIn().delay(viewinmiliseconds).fadeOut('fast');

}
VacancyModel.DisplaySuccess = function (form, errors) {
    var errorSummary = getValidationSummaryErrors(form)
            .removeClass('validation-summary-valid')
            .addClass('validation-summary-success');

    var items = $.map(errors, function (error) {
        return '<li>' + error + '</li>';
    }).join('');

    var ul = errorSummary
            .find('ul')
            .empty()
            .append(items);
};
VacancyModel.DisplayErrors = function (form, errors) {
    var errorSummary = getValidationSummaryErrors(form)
            .removeClass('validation-summary-valid')
            .addClass('validation-summary-errors');

    var items = $.map(errors, function (error) {
        return '<li>' + error + '</li>';
    }).join('');

    var ul = errorSummary
            .find('ul')
            .empty()
            .append(items);
};

VacancyModel.DisplayManualErrors = function (elementid, errorMessage) {
    var _myError = '<div class="validation-summary-errors"><span>' + errorMessage + '</span><ul></ul></div>';
    $('#' + elementid).empty();
    $('#' + elementid).append(_myError);
}
var getValidationSummaryErrors = function ($form) {
    // We verify if we created it beforehand
    var errorSummary = $form.find('.validation-summary-errors, .validation-summary-valid');
    if (!errorSummary.length) {
        errorSummary = $('<div class="validation-summary-errors"><span>Please correct the errors and try again </span><ul></ul></div>')
                .prependTo($form);
    }

    return errorSummary;
};

VacancyModel.AjaxPost = function (MyUrl, MyData, MySuccessFunctionName, MyErrorFunctionName) {
    $.ajax({
        type: "POST",
        url: MyUrl,
        cache: false,
        data: MyData,
        success: function (data) {
            data = JSON.parse(data);
            window[MySuccessFunctionName](data.IsError, data.Url, data.Data, data.Message)
        },
        error: function (xhr, text, err) {
            window[MyErrorFunctionName](xhr, text, err);
        }
    });
}

VacancyModel.AutoComplete = function (element) {
    var mydivisionid = $(element).attr("DivisionId");

    var myURL = $(element).attr("AutoComplete-Url");
    var myValue = $(element).val();
    var myValueField = $(element).attr("AutoComplete-valueField");
    var myTextField = $(element).attr("AutoComplete-textField");
    var Myelementattr = $(element).attr('SelectedValue');
    var mySelectedValueFun = $(element).attr('AutoComplete-SelectedValueFun');
    var mydisplayOption = $(element).attr('AutoComplete-DisplayOption');
    $.post(myURL, { "DivisionId": mydivisionid, "term": myValue, "displaytop": mydisplayOption, "columnName": myTextField }, function (data) {
        data = JSON.parse(data);
        var availableResults = VacancyModel.AutoCompleteDataSource(data, myValueField, myTextField);

        VacancyModel.BindAutoComplete(element, availableResults, Myelementattr, mySelectedValueFun, myValueField, myTextField, myURL, mydisplayOption);
    });
}

VacancyModel.AutoCompleteDataSource = function (data, dataid, text) {


    var obj = data;
    Data = null;
    Data = [];
    for (var i = 0; i < obj.length; i++) {
        Data[i] = {
            data: obj[i][dataid],
            value: obj[i][text],
            result: [obj[i][dataid]]
        }
    }
    return Data;
}
VacancyModel.BindAutoComplete = function (element, availableResults, Myelementattr, mySelectedValueFun, valueField, displayField, myURL, mydisplayOption) {
    var elementId = $(element).attr('id');

    BindAutoComplete(element, elementId, mySelectedValueFun, Myelementattr, availableResults, myURL, mydisplayOption, valueField, displayField);
}
function BindAutoComplete(element, id, mySelectedValueFun, Myelementattr, availableResults, myURL, mydisplayOption, valueField, displayField) {

    $("#" + id).autocomplete({
        source: availableResults,
        select: function (event, ui) {
            $(element).attr('SelectedValue', ui.item.data);
            window[mySelectedValueFun]($(element).attr('SelectedValue'));
        },
        close: function (event, ui) {

        },
        change: function (event, ui) {


        },
        search: function (event, ui) {
            $(element).attr('SelectedValue', Myelementattr);
            window[mySelectedValueFun]($(element).attr('SelectedValue'));
        }
    }).keyup(function (e) {

        var txtValue = $("#" + id).val();


        if (txtValue.length >= 1) {
            $.post(myURL, { "DivisionId": $("#DivisionId").val(), "term": txtValue, "displaytop": mydisplayOption, "columnName": displayField }, function (data) {
                data = JSON.parse(data);
                var availableResults1 = VacancyModel.AutoCompleteDataSource(data, valueField, displayField);
                $("#" + id).autocomplete({
                    source: availableResults1,
                    select: function (event, ui) {
                        $(element).attr('SelectedValue', ui.item.data);
                        window[mySelectedValueFun]($(element).attr('SelectedValue'));
                    },
                    close: function (event, ui) {

                    },
                    change: function (event, ui) {


                    },
                    search: function (event, ui) {
                        $(element).attr('SelectedValue', Myelementattr);

                        window[mySelectedValueFun]($(element).attr('SelectedValue'));
                    }
                })
            });
        }

    });
}

VacancyModel.SetLoadingProcess = function () {
    $(document).ajaxStart(function () {
        ShowLoading();
    });
    $(document).ajaxComplete(function () {
        HideLoading();
    });
    $(document).ajaxError(function () {
        HideLoading();
    });
    $(document).ajaxStop(function () {
        HideLoading();
    });
}
//    $('form').on('submit',function () {
//        ShowLoading();
//    });

function ShowLoading() {
    $("#loading").fadeIn();
}
function HideLoading() {
    $("#loading").fadeOut();
}

VacancyModel.ValidateFile = function (filename, required) {
    var extension = filename.split('.').pop();
    var myObject = new Object();
    if (required && filename.length <= 0) {

        myObject.IsError = true;
        myObject.Message = "Please Select File to upload";
        return myObject;
    }
    else if (filename.length > 0) {
        if (extension == "pdf" || extension == "docx" || extension == "doc") {
            myObject.IsError = false;
            myObject.Message = "";
            return myObject;
        }
        else {
            myObject.IsError = true;
            myObject.Message = "Invalid File!! allow .pdf,.docx,.doc";
            return myObject;
        }
    }
    else {
        myObject.IsError = false;
        myObject.Message = "";
        return myObject;
    }
}

VacancyModel.ValidateDocumentType = function (filename, Extensions, required) {

    var extension = filename.split('.').pop();
    var myObject = new Object();
    if (required && filename.length <= 0) {

        myObject.IsError = true;
        myObject.Message = "Please Select File to upload";
        return myObject;
    }
    else if (filename.length > 0) {
        if ((Extensions.toLowerCase().indexOf(extension) > 0)) {
            myObject.IsError = false;
            myObject.Message = "";
            return myObject;
        }
        else {
            myObject.IsError = true;
            myObject.Message = "Invalid File!! Allow " + Extensions;
            return myObject;
        }
    }
    else {
        myObject.IsError = false;
        myObject.Message = "";
        return myObject;
    }



}

VacancyModel.Shorten = function (text, maxLength) {

    var ret = text;

    if (ret.length > maxLength) {
        ret = ret.substr(0, maxLength - 3) + "...";
    }
    return ret;
}
VacancyModel.CopyDataToHeader = function (current, classname, replaceText) {
    if (replaceText.length <= 0) {
        replaceText = '&nbsp';
    }
    var changedata = $(current).closest(".acc-Content");
    changedata.siblings().each(function (index) {
        if ($(this).attr("aria-selected") == 'true') {
            if (replaceText.length > 30) {
                replaceText = replaceText.substring(0, 30) + '...';
            }
            $($(this).find("." + classname)).html(replaceText);
            return true;
        }

    });



}
/* END Common Header Set*/

VacancyModel.commaSeparateNumber = function (e) {
    var val = $(e).val();
    $(e).val(VacancyModel.commaSeparateNumberByValue(val));
}

VacancyModel.commaSeparateNumberByValue = function (value) {
    value = $.trim(value).length == 0 ? "0" : value;
    if (value != undefined) {
        var val = value.toString();
        var check = val.substr(0, 1);
        if (check == "0" && val.length > 1) {
            val = val.substr(1);
        }
        if (val.length > 0) {
            val = val.replace(/[, ]+/g, "").trim();

            var parts = val.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

            if (parts[1] != undefined) {
                parts = parts[0] + "." + parts[1].substring(0, 2);
            }
            return parts;
        }
    }

}

VacancyModel.ProfileHeaderCount = function (element) {
    var total = element.parents('.acc-Content').find('.prfle-tab-head').size();
    var $topheader = element.parents('.acc-Content').prev();
    $topheader.find('.prfle-tab-head > .count').html("(" + total + ")");
}
VacancyModel.IgnoreAccordianHeaderClick = function (accordianClass) {
    $("." + accordianClass + " > .acc-header > .prfle-tab-head > .ansoption").click(function (event) {
        event.stopImmediatePropagation();
        event.preventDefault();
    });
    $("." + accordianClass + " > .acc-header > .prfle-tab-head-brown > .ansoption").click(function (event) {
        event.stopImmediatePropagation();
        event.preventDefault();
    });


}
VacancyModel.IgnoreAccordianClick = function () {
    //Stop accordian click event on delete click
    $(".SubAccordion > .acc-header > .prfle-tab-head > .delete").click(function (event) {
        event.stopImmediatePropagation();
        event.preventDefault();
    });

    //Stop accordian click event on delete click
    $(".SubAccordion > .acc-header > .prfle-tab-head > .Save").click(function (event) {
        event.stopImmediatePropagation();
        event.preventDefault();
    });
    ////Stop accordian click event on delete click
    $("#accordion > .acc-header > .prfle-tab-head > .Save").click(function (event) {
        event.stopImmediatePropagation();
        event.preventDefault();
    });

    $(".SubAccordion > .acc-header > .prfle-tab-head-brown > .Save").click(function (event) {
        event.stopImmediatePropagation();
        event.preventDefault();
    });
}


VacancyModel.DisplayInlineEditNotification = function (data, element) {
    try {

        if (!data.IsError) {

            VacancyModel.ProfileSuccessMeesage($($(element).parent().parent().find(".notification")), data.Message, 3000);
        }
        else {
            VacancyModel.ProfileErrorMeesage($($(element).parent().parent().find(".notification")), data.Message, 3000);
        }
    }
    catch (err) {
        VacancyModel.ProfileErrorMeesage($($(element).parent().parent().find(".notification")), err.message, 3000);

        // Block of code to handle errors
    }


}
VacancyModel.getUrlParameter = function (sURL, sParam) {
    var sPageURL = sURL;
    var sURLqVariables = sPageURL.split('?');
    for (var j = 0; j < sURLqVariables.length ; j++) {
        var sURLVariables = sURLqVariables[j].split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }
}
VacancyModel.CallGetMethod = function (url, successCallback) {
    VacancyModel.CallMethod("GET", url, successCallback);
}
VacancyModel.CallPostMethod = function (url, successCallback) {
    VacancyModel.CallMethod("POST", url, successCallback);
}

var canCache = true;
var ua = window.navigator.userAgent;
var old_ie = ua.indexOf('MSIE ');
var new_ie = ua.indexOf('Trident/');

if ((old_ie > -1) || (new_ie > -1)) {
    canCache = false;
}

VacancyModel.CallMethod = function (method, url, successCallback) {
    debugger;
    $.ajax({
        type: method,
        url: url,
        //timeout: 0,
        cache: canCache,
        //data: JSON.stringify(parameters),
        //contentType: 'application/json;',
        //dataType: 'json',
        success: function (data) {
            var response = JSON.parse(data);
            if (response.SessionTimeOut) {
                VacancyModel.DisplayErrorMeesage("#commonMessage", response.Message, 3000);
                window.location.href = response.Url;
            }
            else if (!response.IsError) {
                if (successCallback != null)
                    successCallback(response.Data, response.Message);
                //window[successCallback](response.Data, custom)
            }
            else {
                VacancyModel.DisplayErrorMeesage("#commonMessage", response.Message, 3000);
            }
        },
        error: function (xhr, text, err) {
            //window[MyErrorFunctionName](xhr, text, err);
            VacancyModel.DisplayErrorMeesage("#commonMessage", text, 3000);
        }
    });
}


VacancyModel.ValidateQuestionAnswer = function (form) {
    var errorclass = 'input-validation-error';
    if ($(form).find('.ATSTextArea').length > 0) {
        var $arealist = $(form).find('.ATSTextArea');
        $arealist.each(function (index) {
            var $area = $(this);
            $area.find('textarea').each(function (index) {
                {
                    if ($(this).val() == "") {
                        $(this).addClass(errorclass);
                    }
                    else {
                        $(this).removeClass(errorclass);
                    }
                }
            })
        });
    }
    if ($(form).find('.ATSTextBox').length > 0) {
        var $txtlist = $(form).find('.ATSTextBox');
        $txtlist.each(function (index) {
            var $txt = $(this);
            $txt.find("input[type='text']").each(function (index) {
                {
                    if ($(this).val() == "") {
                        $(this).addClass(errorclass);
                    }
                    else {
                        $(this).removeClass(errorclass);
                    }
                }
            })
        });
    }
    if ($(form).find('.ATSPickList').length > 0) {
        var $pklist = $(form).find('.ATSPickList');
        $pklist.each(function (index) {
            var $pk = $(this);
            $pk.find('select').each(function (index) {
                {
                    if ($(this).val() == '' || $(this).val() == 0) {
                        $(this).addClass(errorclass);
                    }
                    else {
                        $(this).removeClass(errorclass);
                    }
                }
            })
        });
    }
    if ($(form).find('.ATSYesNo').length > 0) {
        var allow = false;
        var $yesno = $(form).find('.ATSYesNo');

        $yesno.each(function (index) {
            var $yn = $(this);
            $yn.find('select').each(function (index) {

                if ($(this).val() == '' || $(this).val() == 0) {
                    $(this).addClass(errorclass);
                }
                else {
                    $(this).removeClass(errorclass);
                }
            });
        });
    }
    if ($(form).find('.ATSCheckBox').length > 0) {

        var $checkboxlist = $(form).find('.ATSCheckBox');
        $checkboxlist.each(function (index) {
            var allow = false;
            var $chk = $(this);
            $chk.removeClass('input-validation-error');
            $chk.find("input[type='checkbox']").each(function (index) {
                if ($(this).attr('checked') == 'checked') {
                    allow = true;
                }
            });
            if (!allow) {
                $chk.addClass('input-validation-error');
            }
        });
    }
    if ($(form).find('.' + errorclass).length > 0) {
        return true;
    }
    else {
        return false;
    }

}




VacancyModel.LoadQueBasedonCat = function (Quedatalink, vacRndId, quecatid, scheduleintid, Row, htmlcat, ele) {
    Quedatalink = Quedatalink.replace("_VACRNDID_", vacRndId);
    Quedatalink = Quedatalink.replace("_QUECATID", quecatid);
    Quedatalink = Quedatalink.replace("_SCHEDULEINTID_", scheduleintid);
    Quedatalink = Quedatalink.replace("_ROW_", Row);
    $.ajax({
        url: Quedatalink,
        type: "GET",
        success: function (queresponse) {

            queresponse = JSON.parse(queresponse);
            queresponse.Data = '<div>' + queresponse.Data + '</div>';
            if (!queresponse.IsError) {
                $(htmlcat).find('#Questions').html(queresponse.Data);


                if ($(htmlcat).find('.prev-disable').length > 0) {

                    var cathtml = $(htmlcat).find('#Questions').prev().find('#CatPrev').parent().clone();
                    $(cathtml).find('#CatPrev').attr('class', 'prev-active');
                    $(cathtml).find('#CatPrev').text('Prev');
                    if ($(cathtml).find('#CatPrev').length > 0) {
                        _QueRow = ele.find(".vacancy-quest-box").attr('data-querow');
                        var _CatURL = $(cathtml).find('#CatPrev').attr('data-url');
                        _CatURL += "&QueRow=" + -1
                        $(cathtml).find('#CatPrev').attr('data-url', _CatURL);
                    }
                    $(htmlcat).find('.prev-disable').parent().html($(cathtml).html());
                }

                if ($(htmlcat).find('.next-disable').length > 0) {

                    var cathtml = $(htmlcat).find('#Questions').prev().find('#CatNext').parent().clone();
                    $(cathtml).find('#CatNext').attr('class', 'next-active');
                    $(cathtml).find('#CatNext').text('Next');
                    $(htmlcat).find('.next-disable').parent().html($(cathtml).html());
                }
                $(htmlcat).find("#score").slider(VacancyModel.ScaleScore);
                if ($(htmlcat).find("#AnsScore").length > 0) {
                    $(htmlcat).find("#AnsScore").slider(VacancyModel.ScaleScore);
                }
                ele.html(htmlcat);
                //if ($(ele).attr("data-purpose").length > 0) {
                //    if ($(ele).attr("data-purpose") == "conductinterview") {
                //        $("#intComp").remove();
                //    }
                //}
            }

        }
    });
}

VacancyModel.GetBeginInterviews = function (appid) {
    $('#BeginInterview').html('');
    $.post("/MyCandidates/GetBeginInterviews", { ApplicationId: appid }, function (data) {
        data = JSON.parse(data);
        if (data.ScheduleIntId.length == 0) {
            $('#BeginInterview').append('<li class="NoRecords">No Pending Interviews</li>')
        }
        else {
            for (var i = 0; i < data.ScheduleIntId.length; i++) {
                var AppendHtml = '<li><a OnClick="return BeginInterview(this);" data-scheduleintid=' + data.ScheduleIntId[i] + ' data-vacrndid=' + data.VacRndId[i] + '>' + data.RoundDetail[i] + '</a></li>';
                $('#BeginInterview').append(AppendHtml);
            }
        }
    });
}