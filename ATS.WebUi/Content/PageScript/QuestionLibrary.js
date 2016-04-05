function QuesitonLibrary() { }
QuesitonLibrary.QueAccordianURL = "";
QuesitonLibrary.CatAccordianURL = "";
QuesitonLibrary.EditCategoryURL = "";
QuesitonLibrary.EditQuestion = "";
QuesitonLibrary.EditAnsOptURL = "";
QuesitonLibrary.FadeOut = 3000;


$(document).ready(function () {
    $(".CatAccordian").accordion(VacancyModel.AccFirstAttr);
    $(".QueAccordian").accordion(VacancyModel.AccSecondAttr);
    $(".AnsAccordian").accordion(VacancyModel.AccSecondAttr);

    VacancyModel.IgnoreAccordianHeaderClick("CatAccordian");
    VacancyModel.IgnoreAccordianHeaderClick("QueAccordian");
    VacancyModel.IgnoreAccordianHeaderClick("AnsAccordian");

    $(".QueAccordian").live("accordionbeforeactivate", function (event, ui) {

        if (ui.newHeader.length > 0) {
            var _currentQueId = $(ui.newHeader).attr('data-pkid');
            var _QueDataType = $(ui.newHeader).attr('data-qdatatype');
            var _roundattr = $(ui.newHeader).closest('.acc-Content').prev().attr('data-roundattributeno');
            var _catid = $(ui.newHeader).closest('.acc-Content').prev().attr('data-catidonly');
            var _editURL = QuesitonLibrary.QueAccordianURL;
            //QuesitonLibrary.QueAccordianURL = '@Url.Action("GetAnswerByQueId", "QuestionLibrary", new { @area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, @QueId = "_QUEID_" })'
            _editURL = _editURL.replace("_QUEID_", _currentQueId);
            _editURL = _editURL.replace("_QUEDT_", _QueDataType);
            _editURL = _editURL.replace("_ROUNDATTRIBUTE_", _roundattr);
            _editURL = _editURL.replace("_CATID_", _catid);
            $.get(_editURL, function (response) {
                var jsonData = JSON.parse(response);
                if (!jsonData.IsError) {

                    if (_currentQueId == "00000000-0000-0000-0000-000000000000") {
                        $(ui.newPanel).children().html('');
                        $(ui.newPanel).children().append(jsonData.Data);
                        var _btn = $('#addQuestionQL').length;
                    }
                    else {
                        $(ui.newPanel).find(".AnsAccordian").html('');
                        if ($(ui.newHeader).find("span.update").length > 0) {
                            var $_updateelement = $(ui.newHeader).find("span.update");
                            editQuestion($_updateelement.find("a:first"));
                        }
                        $(ui.newPanel).find(".AnsAccordian").append(jsonData.Data).accordion(VacancyModel.AccSecondAttr).accordion('refresh');
                        $(ui.newPanel).find(".AnsAccordian").on("accordionbeforeactivate", function (event, ui) {
                            
                            var $ansUi = ui;
                            if ($($ansUi.newHeader).find("span.update").length > 0) {
                                var $_updateans = $($ansUi.newHeader).find("span.update");
                                editAnswer($_updateans.find("a:first"));

                            }
                            event.stopImmediatePropagation();
                        });
                        VacancyModel.IgnoreAccordianHeaderClick("AnsAccordian");
                    }

                    var _form = $(ui.newPanel).find("#frmQuestion");
                    var formAECat = $(_form);
                    $.validator.unobtrusive.parse(formAECat);
                }
            });
        }


        else {
        }
        event.stopImmediatePropagation();

    });


    function CatAccordianBA(event, ui) {
        
        if (ui.newHeader.length > 0) {
            var _currentCatId = $(ui.newHeader).attr('data-catidonly');

            var _editURL = QuesitonLibrary.CatAccordianURL;
            //QuesitonLibrary.CatAccordianURL ='@Url.Action("GetQuestionByCategoryId", "QuestionLibrary", new { @area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, @CatId = "_CATID_" })'
            _editURL = _editURL.replace("_CATID_", _currentCatId);
            $.get(_editURL, function (response) {
                var jsonData = JSON.parse(response);
                if (!jsonData.IsError) {
                    if (_currentCatId == "00000000-0000-0000-0000-000000000000") {
                        $(ui.newPanel).children().html('');
                        $(ui.newPanel).children().append(jsonData.Data);
                        var _acc = $(ui.newPanel).find('.AccCategoryInner').children('.acc-header');
                        $(_acc).click();
                    }
                    else {
                        $(ui.newPanel).find(".QueAccordian").html('');
                        if ($(ui.newHeader).find("span.update").length > 0) {
                            var $_updateelement = $(ui.newHeader).find("span.update");
                            editCategory($_updateelement.find("a:first"));
                        }
                        $(ui.newPanel).find(".QueAccordian").append(jsonData.Data).accordion(VacancyModel.AccSecondAttr).accordion('refresh');
                        $(ui.newPanel).find(".QueAccordian").find('.AnsAccordian').accordion(VacancyModel.AccSecondAttr).accordion('refresh');
                        VacancyModel.IgnoreAccordianHeaderClick("QueAccordian");
                    }
                    var _form = $(ui.newPanel).find("#frmCategory");
                    var formAECat = $(_form);
                    $.validator.unobtrusive.parse(formAECat);
                }
            });

        }
        else {
        }
    }
    $(".CatAccordian").live("accordionbeforeactivate", function (event, ui) {
        CatAccordianBA(event, ui);
    });
    $('#addQuestionQL').live('click', function () {
        var $that = $(this);
        //if ($that.parent().prev().find('#frmQuestion').length < 1) {

        var Neworder = $(this).parent().prev().find('.acc-header').length + 1
        var _editURL = $(this).attr("data-url");
        _editURL = _editURL.replace("_QUEORDER_", Neworder);

        $.get(_editURL, function (response) {
            var jsonData = JSON.parse(response);
            if (!jsonData.IsError) {

                $that.parent().prev().append(jsonData.Data).accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion({ active: $(this).find(".acc-header").size() - 1 });
                //$that.parent().prev().accordion({ collapsible: true, active: false }).accordion('refresh');
                //$that.parent().prev().append(jsonData.Data);

            }
            //$('#addQuestionQL').remove();
        });

        //}

    });
});
$('#RemoveAnsOpt,#RemoveCat').live('click', function () {
    if ($(this).closest('form').closest('.editsection').length > 0) {
        //For Edit

        //$(this).closest('form').parents("div[class^='acc-Content:fist'][style^='display: block;']").remove('div');
        //$(this).closest('form').parents("div[class^='acc-header:fist'][aria-selected='true']").remove('div');

        $(this).closest('form').html('').closest('.editsection').hide();
    }
    else {
        //For Insert
        $(this).closest('form').remove();
    }
});
$('#addCategory').live('click', function () {

    var $that = $(this);
    //if ($that.parent().prev().find('#frmCategory').length < 1)
    //{
    var Neworder = $(this).parent().prev().find('.acc-header').length + 1
    var _editURL = $(this).attr("data-url");
    _editURL = _editURL.replace("_CATORDER_", Neworder);

    $.get(_editURL, function (response) {
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {

            //$that.parent().prev().accordion({ collapsible: true, active: false }).accordion('refresh');

            $that.parent().prev().append(jsonData.Data).accordion('destroy').accordion(VacancyModel.AccFirstAttr).accordion({ active: $(this).find(".acc-header").size() - 1 });
            //$that.parent().prev().accordion({ collapsible: true, active: false }).accordion('refresh');
            //$that.parent().prev().append(jsonData.Data);
            var $innerCatAccordian = $('.CatAccordian .acc-header:last').parent();
            $innerCatAccordian.accordion({ active: 0 });
        }javascript: void (0);
    });
    $('#addCategory').hide();
    //}
});

$('#addAnswer').live('click', function () {
    var $that = $(this);
    
    //if ($that.parent().prev().find('#frmAnsOpt').length < 1) {
    var Neworder = $(this).parent().prev().find('.acc-header').length + 1
    var _editURL = $(this).attr("data-url");
    _editURL = _editURL.replace("_ANSORDER_", Neworder);
    $.get(_editURL, function (response) {
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {
            $that.parent().prev().append(jsonData.Data).accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion({ active: $(this).find(".acc-header").size() - 1 });
        }
    });
    //}
});
function editCategory(element) {
    var _currHeader = $(element).closest(".acc-header");
    var $acc = $(element).closest(".acc-header").parent();
    var active = $acc.children('.acc-header').index(_currHeader);


    var $that = $(element).closest(".acc-header").next().find("span:first");
    var _editURL = QuesitonLibrary.EditCategoryURL;
    //'@Url.Action("GetCategory", "QuestionLibrary", new { @area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, @QueCatId = "_QUECATID_" })'
    _editURL = _editURL.replace("_QUECATID_", $(element).attr("data-id"));
    $.get(_editURL, function (response) {
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {
            $(".acc-Content").children().find("#frmCategory").remove();
            $that.html(jsonData.Data);
            $that.show();
            $acc.accordion("option", "active", parseInt(active));

        }
        else {
            VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
        }

    });
};

function editQuestion(element) {
    var _currHeader = $(element).closest(".acc-header");
    var $acc = $(element).closest(".acc-header").parent();
    var active = $acc.children('.acc-header').index(_currHeader);
    var $that = $(element).closest(".acc-header").next().find("span:first");
    var _editURL = QuesitonLibrary.EditQuestion;
    //'@Url.Action("GetQuestion", "QuestionLibrary", new { @area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, @QueId = "_QUEID_" })'
    _editURL = _editURL.replace("_QUEID_", $(element).attr("data-id"));
    _editURL = _editURL.replace("_ROUNDATTRIBUTENO_", $(element).parents(".acc-Content").prev().attr("data-roundattributeno"));
    $.get(_editURL, function (response) {
        $(".acc-Content").children().find("#frmQuestion").remove();
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {
            $that.html(jsonData.Data);
            $that.show();
            $acc.accordion("option", "active", parseInt(active));

        }
        else {
            VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
        }
    });
};

function editAnswer(element) {
    var _currHeader = $(element).closest(".acc-header");
    var _curAnsOptId = $(_currHeader).attr('data-pkid');
    var $acc = $(element).closest(".acc-header").parent();
    var active = $acc.children('.acc-header').index(_currHeader);

    var $that = $(element).closest(".acc-header").next().find("span:first");
    $.get($(element).attr("data-url"), function (response) {
        
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {

            //if (_curAnsOptId == "00000000-0000-0000-0000-000000000000") {
            //    $(ui.newPanel).children().html('');
            //    $(ui.newPanel).children().append(jsonData.Data);
            //    var _acc = $(ui.newPanel).find('.AnsAccordian').children('.acc-header');
            //    $(_acc).click();

            //}

            //else {
            $(".acc-Content").children().find("#frmAnsOpt").remove();
            $that.html(jsonData.Data);
            $that.show();
            $acc.accordion("option", "active", parseInt(active));
            // }
        }
        else {
            VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
        }
    });
}

function SetQueOrder(element) {
    //var neworder = $(element).attr('data-neworder');
    //if (neworder <= 0 || neworder >= $(element).closest(".QueAccordian").find('.acc-header').length + 1) {
    //    VacancyModel.DisplayErrorMeesage('#commonMessage', 'Not able to move last element', QuesitonLibrary.FadeOut);
    //}
    //else {
    $.get($(element).attr("data-url"), function (response) {
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {
            var active = $(element).closest(".QueAccordian").closest('.CatAccordian').accordion("option", "active");
            $(element).closest(".QueAccordian").closest('.CatAccordian').accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion("option", "active", active);

        }
    });
    //}
}

function SetAnsOrder(element, order) {

    //var neworder = $(element).attr('data-neworder');
    //if (neworder <= 0 || neworder >= $(element).closest(".AnsAccordian").find('.acc-header').length + 1) {
    //    VacancyModel.DisplayErrorMeesage('#commonMessage', 'Not able to move last element', QuesitonLibrary.FadeOut);
    //}
    //else {
    $.get($(element).attr("data-url"), function (response) {
        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {
            var active = $(element).closest(".AnsAccordian").closest('.QueAccordian').accordion("option", "active");
            $(element).closest(".AnsAccordian").closest('.QueAccordian').accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion("option", "active", active);

        }
    });
    //}
}

function SetQueCatOrder(element) {

    //var neworder = $(element).attr('data-neworder');
    //if (neworder <= 0 || neworder >= $(element).parents(".CatAccordian").find('.acc-header').length + 1) {
    //    VacancyModel.DisplayErrorMeesage('#commonMessage', 'Not able to move last element', QuesitonLibrary.FadeOut);
    //}
    //else {
    $.get($(element).attr("data-url"), function (response) {

        var jsonData = JSON.parse(response);
        if (!jsonData.IsError) {
            $(element).parents('.CatAccordian').html(jsonData.Data).accordion('destroy').accordion(VacancyModel.AccFirstAttr);
            $(element).parents('.CatAccordian').on("accordionbeforeactivate", function (event, ui) {
                CatAccordianBA(event, ui);
            });
            VacancyModel.IgnoreAccordianHeaderClick("CatAccordian");

        }
    });
    //}
}



$('#SubmitAnsOpt').live('click', function () {

    //    var $that = $(this).parent().prev().find('#AnsWeight');
    var $that = $(this).parents('.vacancy-Details').prev().find('#AnsWeight');

    //var _quedataType = $(this).parent().prev().find('#QueDataType').val();
    //var _quedataType= $(this).parents('.QueAccordian ').find('#QueDataType').val();

    var _quedataType = $(this).parents('.AnsAccordian').parent().prev().attr('data-qdatatype');
    var _ansOptPk = $that.closest(".acc-Content").prev().attr('data-pkid');
    //for yes/no


    if (_quedataType == 5) {
        if ($that.closest(".AnsAccordian").find(".acc-header").length >= 3) {
            VacancyModel.DisplayErrorMeesage('#commonMessage', 'Yes/No has only two answer option yes or no', QuesitonLibrary.FadeOut);
            return false;
        }
        else {
            return true;
        }
    }
    //for Checklist/no
    if (_quedataType == 2) {

        var sum = 0;

        $that.closest(".AnsAccordian").find(".acc-header").each(function (index) {
            if ($(this).attr('data-pkid') != _ansOptPk) {
                sum += parseInt($(this).find('#rndweight').text());

            }
        });
        sum += parseInt($that.val());
        if (sum > 100) {
            VacancyModel.DisplayErrorMeesage('#commonMessage', 'Summation will not be more than 100 for checklist ', QuesitonLibrary.FadeOut);
            return false;
        }
    }
    return true;

});
function DeleteConfirm(element) {

    var _ele = $(element);
    VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Question Category", 400, "Are you sure you want to delete this Question Category ? ", "Yes", "Cancel", "DeleteQueCat", _ele, "test");
}

function DeleteQueCat(Result, element) {
    if (Result) {
        $.get($(element).attr("data-url"), function (response) {

            var jsonData = JSON.parse(response);


            if (!jsonData.IsError) {
                $(element).parents('.CatAccordian').html(jsonData.Data).accordion('destroy').accordion(VacancyModel.AccFirstAttr);
                $(element).parents('.CatAccordian').on("accordionbeforeactivate", function (event, ui) {
                    CatAccordianBA(event, ui);

                });
                var _lstcount = $('.CatAccordian').children('.acc-header').length;
                $("#lblCount").text(_lstcount);
                $('#Count_CSMNU_QUE').text(_lstcount);
                VacancyModel.IgnoreAccordianHeaderClick("CatAccordian");
                VacancyModel.DisplaySuccessMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);


            }
            else {
                VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
            }
        });

    }
}


function DeleteQueConfirm(element) {
    var _ele = $(element);
    VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Question", 300, "Are you sure want to delete this Question? ", "Yes", "Cancel", "DeleteQue", _ele, "test");
}

function DeleteQue(Result, element) {
    var _ele = $(element);
    var _queAcc = $(_ele).closest(".QueAccordian");
    var _catAcc = $(_queAcc).closest('.CatAccordian');
    var _quecnt = "";
    var curr = "";
    var active = "";
    if (Result) {
        //$.get($(element).attr("data-url"), function (response) {
        //    var jsonData = JSON.parse(response);
        //    if (!jsonData.IsError) {
        //         active = $(element).closest(".QueAccordian").closest('.CatAccordian').accordion("option", "active");
        //         $(element).closest(".QueAccordian").closest('.CatAccordian').accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion("option", "active", active);
        //    }
        //    else {
        //        VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
        //    }
        //});
        
        //curr = $('.CatAccordian').find('.acc-header').get(active);
        //_quecnt = $(_queAcc).children('.acc-header').length;
        //alert(_quecnt);
        //$(curr).find("#quecnt").text(_quecnt);

        $.ajax({
            url: $(element).attr("data-url"),
            type: "Get",
            async: false,
            success: function (response) {
                var jsonData = JSON.parse(response);
                if (!jsonData.IsError) {

                    active = $(element).closest(".QueAccordian").closest('.CatAccordian').accordion("option", "active");

                    $(element).closest(".QueAccordian").closest('.CatAccordian').accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion("option", "active", active);
                }
                else {
                    VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
                }
                //for count Update
                curr = $('.CatAccordian').find('.acc-header').get(active);
                _quecnt = $(_queAcc).children('.acc-header').length - 1;

                $(curr).find("#quecnt").text(_quecnt);
            }
        });
    }
}

function DeleteAnsOptConfirm(element) {
    var _ele = $(element);
    VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Answer Option", 300, "Are you sure want to delete this Answer option? ", "Yes", "Cancel", "DeleteAnsOpt", _ele, "test");
}

function DeleteAnsOpt(Result, element) {
    var _ele = $(element);
    var _queAcc = $(_ele).closest(".QueAccordian");
    var _catAcc = $(_queAcc).closest('.CatAccordian');
    var _quecnt = "";
    var curr = "";
    var active = "";
    if (Result) {
        $.ajax({
            url: $(element).attr("data-url"),
            type: "Get",
            async: false,
            success: function (response) {
                var jsonData = JSON.parse(response);
                if (!jsonData.IsError) {
                    active = $(element).closest(".AnsAccordian").closest(".QueAccordian").accordion("option", "active");
                    $(element).closest(".AnsAccordian").closest(".QueAccordian").accordion('destroy').accordion(VacancyModel.AccSecondAttr).accordion("option", "active", active);
                }
                else {
                    VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
                }
            }

        });

    }
}

