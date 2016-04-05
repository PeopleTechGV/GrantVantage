function MyVacancy() { }

//VacancyAcc
MyVacancy.FadeOut = 3000;
MyVacancy.VacAccGetVacancyDetailsURL = "";
MyVacancy.VacAccGetJobDescriptionURL = "";
MyVacancy.VacAccGetCompensationInfoURL = "";
MyVacancy.VacAccGetApplicationProcessURL = "";
//AccRounddetail
MyVacancy.AccGetReviewersByVacancyRndURL = "";
MyVacancy.AccGetQueByVacancyRndURL = "";
MyVacancy.AccGetRoundConfigDetailURL = "";

//Apply Template URL
MyVacancy.ApplyTemplateURL = "";

//AccRound
MyVacancy.AccGetRevAndQueCountURL = "";

//AddQuestion
MyVacancy.AddQuestionURL = "";

//Add Reviewer
MyVacancy.AddReviewerURL = "";

//Get All Applications by vacancy

MyVacancy.GetAllApplicationURL = "";
//Is VacancyApplication Exists
MyVacancy.IsCandidateApplied = 'false';


//Add Vacnacy Question Category
MyVacancy.AddVacQueCatURL = "";
//Get Que by VacQueCat 
MyVacancy.AccGetQueByVacQueCatURL = "";
MyVacancy.ChangeVacQueDropDownURL = "";

//Delete VacRnd
MyVacancy.RemoveVacRnd = "";
//Delete VacQueCat

MyVacancy.RemoveVacQueCat = "";

//Add Reviewer btn
MyVacancy.AddReviewersBtn = {};


// Add Category btn
MyVacancy.AddCategoryBtn = {};

//AddQuestion Btn
MyVacancy.AddQuestionBtn = {};
MyVacancy.RemoveVacQue = "";

MyVacancy.ChkSpan = {};
MyVacancy.ChkTemplate = {};
MyVacancy.GetVacancyHistoryURL = "";
MyVacancy.GetDeclinedAutoEmailURL = "";
MyVacancy.GetMyVacancyGearActionURL = "";
MyVacancy.GetRequiredDocumentsURL = "";

$(document).ready(function () {

    var link = document.createElement('a');
    link.href = "javascript void();";
    link.className = "btn CaseUpper";
    link.id = "addReviewers"
    link.innerHTML = "Add Reviewer";
    MyVacancy.AddReviewersBtn = link;


    var Catlink = document.createElement('a');
    Catlink.href = "javascript void();";
    Catlink.className = "btn CaseUpper";
    Catlink.id = "addVacQueCat"
    Catlink.innerHTML = "Add Category";
    MyVacancy.AddCategoryBtn = Catlink;

    var Quelink = document.createElement('a');
    Quelink.href = "javascript void();";
    Quelink.className = "btn CaseUpper";
    Quelink.id = "addQuestion"
    Quelink.innerHTML = "Add Question";
    MyVacancy.AddQuestionBtn = Quelink;

    var chkspan = document.createElement('span')
    chkspan.className = "ttldbrown CaseUpper";
    chkspan.innerHTML = "Apply Template";
    MyVacancy.ChkSpan = chkspan;

    var chktemplate = document.createElement('input')
    chktemplate.type = "checkbox";
    chktemplate.id = "chkApplyTemplate"
    chktemplate.value = "Apply Template";
    chktemplate.style = "margin-right: 7px;";
    chktemplate.checked = false;
    MyVacancy.ChkTemplate = chktemplate;

    $(".doneround").live("click", function () {

        $(this).closest('.acc-Content').prev().trigger('click');
    });

    MyVacancy.MakeFormReadOnly = function () {

        if (MyVacancy.IsCandidateApplied == 'true') {
            $('#VacancyAcc').find('form').each(function (index) {
                var form = $(this);
                VacancyModel.MakeVacancyFormReadOnly($(this).attr('id'));
                //$('#' + $(this).attr('id') + 'button').hide();
                //$('#' + $(this).attr('id') + 'a').hide();
            });
            $('.ARPContent').find('a').hide();
            $('#VacancyAcc').find('.downloadresume').show();
            $('#VacancyAcc').find('.downloadcover').show();
            $('.EnableButton').show();
            $(".seting-icon a#btn-remove-vacancy").hide();
            $(".seting-icon a#removeVacancy").hide();

            if ($('#VacancyAcc').find('.note').length > 0) {
                $('#VacancyAcc').find('.note').css('display', 'none');
            }
            $('.AccRoundDetail').find('button').remove();
            $('.AccRoundDetail').find('#ReviewRound').attr('disabled', 'disabled');
            $('#divMainTemplate').css('display', 'none');
        }
    }
    MyVacancy.MakeFormReadOnly();

    $("#chkApplyTemplate").live('change', function () {
        if ($("#chkApplyTemplate").is(':checked')) {
            var dataLink = MyVacancy.ApplyTemplateURL;
            VacancyModel.CallGetMethod(dataLink, function (data) {
                $("#divTemplateForm").html('');
                $("#divTemplateForm").append(data);
                MyVacancy.MakeFormReadOnly();
            });
        }
        else {
            $("#divTemplateForm").html('');
        }

    });

    MyVacancy.CreateSelectMenu = function (element) {

        $(element).selectmenu().selectmenu({
            width: 250,

            change: function (event, ui) {

                MyVacancy.DropVacQueOnChange($(this), ui.item.value, MyVacancy.ChangeVacQueDropDownURL);
            },
            _renderMenu: function (ul, items) {
                $(ul).addClass('r-right');
            }
        }).selectmenu("menuWidget").addClass("overflow");

    }

    MyVacancy.DeleteVacRnd = function (Myelement) {
        var msg = 'Are you sure you want to delete this Review Round?';
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Opportunity Round", 400, msg, "Yes", "Cancel", "DeleteVacRndConfirm", Myelement, null);
    }

    MyVacancy.DeleteVacQueCatConfirm = function (element) {

        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Question Category", 300, "Are you sure you want to delete this  Question Category?", "Yes", "Cancel", "DeleteVacQueCat", element, null);
        return false;
    }

    MyVacancy.IgnoreAccordianVacancyAccHeaderClick = function (ui) {
        console.log($(ui).find(".prfle-tab-head-brown > .ansoption").length);

        $(ui).find(".prfle-tab-head-brown > .ansoption").each(function () {
            $(this).live('click', function (event) {
                event.stopImmediatePropagation();
                event.preventDefault();
            });
        });
    }

    MyVacancy.SetConformation = function (eleContent) {
        $(eleContent).prev().find('.ttllbrownConf').remove();
        var CountConf = $('.ttllbrownConf').length;
        if (CountConf == "0") {
            //$("#VacancyAcc").accordion('destroy').accordion(VacancyModel.AccFirstAttr).accordion("option", "active", accIndex);
            var VacancyId = $(eleContent).prev().attr('data-recordid');


            var dataLink = MyVacancy.GetMyVacancyGearActionURL;
            dataLink = dataLink.replace("_VACID_", VacancyId);
            VacancyModel.CallGetMethod(dataLink, function (data) {
                $('.ActionBar').replaceWith(data);
            });
        }
        else {
            var accIndex = $(eleContent).prev().size();
            if (accIndex == 1) {
                $("#ddlVacancyStatus").attr("disabled", true);
                $("#ddlStatusReason").attr("disabled", true);
            }
            $(eleContent).find(".prfle-button-save").remove();
        }
    }

    MyVacancy.DropVacQueOnChange = function (element, selectedValue, _dataLink) {
        console.log('Onchange Call');
        var vacrndid = $(element).parents('.acc-Content:first').prev().attr('data-roundid');
        var _VacQueCatId = $(element).parents('.acc-Content:first').prev().attr('data-pkid');
        var vacqueid = '';
        var queid = '';
        if ($(element).parents('form').length > 0) {
            queid = $(element).parents('form').parent().attr('data-queid');
            vacqueid = $(element).parents('form').parent().attr('data-pkid');
        }
        var $ele = $(element).parents('.Content:first').children('#addQuestion');
        _dataLink = _dataLink.replace("_SELDATA_", queid);
        _dataLink = _dataLink.replace("_VACRNDID_", vacrndid);
        _dataLink = _dataLink.replace("_VACQUEID_", vacqueid);
        _dataLink = _dataLink.replace("_VACQUECATID_", _VacQueCatId);
        _dataLink = _dataLink.replace("_NEWVAL_", selectedValue);
        $.ajax({
            url: _dataLink,
            type: 'GET',
            cache: false,
            success: function (data) {
                data = JSON.parse(data)
                var $fin = $(data.Data);
                MyVacancy.CreateSelectMenu($fin.find('#peopleC'));
                $(element).parents('form').replaceWith($fin);
            },
            error: function (e) {
                //called when there is an error
                //console.log(e.message);
            }
        });

        //$.get(_dataLink, function (data) {
        //    data = JSON.parse(data);
        //    var $fin = $(data.Data);

        //    //MyVacancy.CreateSelectMenu($fin.find('form').find('#peopleC'));
        //    //$fin.find('form').find('#peopleC').selectmenu().selectmenu({
        //    //    width: 250,
        //    //    change: function (event, ui) {
        //    //        MyVacancy.DropVacQueOnChange($(this), ui.item.value,_dataLink);
        //    //    },
        //    //    _renderMenu: function (ul, items) {
        //    //        $(ul).addClass('r-right');
        //    //    }
        //    //}).selectmenu("menuWidget").addClass("overflow");

        //    $(element).parents('form').replaceWith($fin);
        //    //$(element).parents('form').find('#peopleC').selectmenu().select


        //});
    }

    //Initialize Main Accordian





    $("#VacancyAcc").accordion({
        icons: VacancyModel.AccFirstIcon,
        heightStyle: "content",
        collapsible: true,
        active: 0,
        animate: true
    });
    $("#VacancyAcc > div.headerVacHistory").click(function (event, ui) {
        if ($(this).hasClass('ui-state-active')) {
            $('#aPrintHistory').show();
        }
        else {
            $('#aPrintHistory').hide();
        }
    });



    $("#VacancyAcc").live("accordionbeforeactivate", function (event, ui) {
        var isFormOpen = AccordionOpenValidation();
        if (!isFormOpen) {
            event.stopImmediatePropagation();
            event.preventDefault();
            return false;
        }
        if (ui.newHeader.length > 0) {
            $('#aPrintHistory').hide();
            //Get oppened accordian index
            var _curAcc = $.trim(ui.newHeader.attr('data-index'));
            //Get current Vacancy Id
            var _vacId = $.trim(ui.newHeader.attr('data-recordid'));
            switch (parseInt(_curAcc)) {
                case 0:
                    var _mode = $("div[name ='vacancyDetail']").attr('data-mode');
                    VacancyModel.CallPostMethod(MyVacancy.VacAccGetVacancyDetailsURL + '?VacancyId=' + _vacId + '&mode=' + _mode, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                        MyVacancy.MakeFormReadOnly();
                    });

                    break;
                case 1:
                    var _mode = $("div[name ='jobdesc']").attr('data-mode');
                    VacancyModel.CallPostMethod(MyVacancy.VacAccGetJobDescriptionURL + '?VacancyId=' + _vacId + '&mode=' + _mode, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                        MyVacancy.MakeFormReadOnly();
                    })

                    break;
                case 2:
                    var _mode = $("div[name ='cominfo']").attr('data-mode');
                    VacancyModel.CallPostMethod(MyVacancy.VacAccGetCompensationInfoURL + '?VacancyId=' + _vacId + '&mode=' + _mode, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                        //$(ui.newPanel).closest(".acc-Content").find(".VacQueCat").html(data);
                        //$(ui.newPanel).closest(".acc-Content").find(".VacQueCat").accordion(VacancyModel.AccSecondAttr);
                        MyVacancy.MakeFormReadOnly();
                    });

                    break;
                case 3:

                    var _mode = $(ui.newPanel).attr('data-mode');
                    VacancyModel.CallPostMethod(MyVacancy.VacAccGetApplicationProcessURL + '?VacancyId=' + _vacId + '&mode=' + _mode + '&FillScore=false', function (data) {
                        $(ui.newPanel).find(".AccRound").html("");
                        $(ui.newPanel).find(".AccRound").append(data).accordion('option', "active", false).accordion('refresh');
                        MyVacancy.IgnoreAccordianVacancyAccHeaderClick($(ui.newPanel).find(".AccRound"));
                        //Start Apply Round Number 
                        var _text = 0;
                        $.each($(ui.newPanel).find(".AccRound").find(".cntRound"), function (index, value) {
                            $(this).text(index + 1);
                            _text = index + 1;
                        })
                        if (_text != 0) {
                            $('#divTemplateChk').html('');
                            $('#divTemplateForm').html('');
                        }

                        _text = $(ui.newPanel).find(".AccRound").find(".acc-header").length;
                        $(ui.newHeader).find('#vacrndcnt').text("(Rounds: " + _text + ")");
                        MyVacancy.MakeFormReadOnly();
                        //End Apply Round Number
                        $("#addRoundConfig").show();
                    });
                    break;

                case 4:
                    var AppStatus = $("#SelectedStatus").html();
                    var _mode = $(ui.newPanel).attr('data-mode');
                    VacancyModel.CallPostMethod(MyVacancy.GetAllApplicationURL + '?VacancyId=' + _vacId + '&AppStatus=' + AppStatus, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                    });
                    break;

                case 5:
                    var AppStatus = $("#SelectedStatus").html();
                    var _mode = $(ui.newPanel).attr('data-mode');
                    VacancyModel.CallGetMethod(MyVacancy.GetVacancyHistoryURL + '?VacancyId=' + _vacId, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                    });
                    break;

                case 6:
                    VacancyModel.CallGetMethod(MyVacancy.GetDeclinedAutoEmailURL + '?VacancyId=' + _vacId, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                    });
                    break;
            }
        }
        else {
        }

    });
    //Initialize Main Accordian END
    function AccordionOpenValidation() {

        var _$FormOpen = $('.AccRound');

        var IsFormOpen = false;
        var $Form;
        console.log(_$FormOpen.find('form').not("#frmRoundConfig").length);
        if (_$FormOpen.find('form').not("#frmRoundConfig,#frmVacQueCat,#frmVacRev").length > 0) {
            //frmRoundConfig
            IsFormOpen = true;
            $Form = $(_$FormOpen.find('form').get(0));
        }
        else {
            return true;
        }
        if (IsFormOpen) {
            $Form.effect("shake", "slow");
            $Form.find('div:first').effect("highlight", "slow");

            return false;
        }
    }






    //Initialize AccVacQueCat Accordian
    $(".VacQueCat").accordion(VacancyModel.AccSecondAttr);

    MyVacancy.AccVacQueCatBA = function (event, ui) {
        if ($(ui.oldHeader).length > 0) {
            var _vacQuecatid = $(ui.oldHeader).attr('data-pkid');
        }
        var isFormOpen = AccordionOpenValidation();
        if (!isFormOpen) {
            event.stopImmediatePropagation();
            event.preventDefault();
            return false;
        }

        var _$addbtn = $(ui.newHeader).parents('.VacQueCat').next().find('#addVacQueCat');
        var _$addQues = $(ui.newPanel).find('.Content').find('#addQuestion');
        if (_$addQues.length <= 0) {
            $(ui.newPanel).find('.Content').append(MyVacancy.AddQuestionBtn);
            _$addQues = $(ui.newPanel).find('.Content').find('#addQuestion');
            console.log(_$addQues.length);
        }
        if (_$addbtn.length <= 0) {
            $(ui.newHeader).parents('.VacQueCat').next().append(MyVacancy.AddCategoryBtn);
            var _$addbtn = $(ui.newHeader).parents('.VacQueCat').next().find('#addVacQueCat');
        }

        if (ui.newHeader.length > 0) {
            _$addbtn.hide();
            if ($(ui.newPanel).find('#frmVacQue').length > 0) {
                $(ui.newPanel).find('#frmVacQue').remove();
            }
            var _VacQueCatId = $.trim($(ui.newHeader).attr('data-pkid'));

            VacancyModel.CallGetMethod(MyVacancy.AccGetQueByVacQueCatURL + '?VacQueCatId=' + _VacQueCatId, function (data) {
                $(ui.newPanel).find('.QuestionDetails').html(data);
                MyVacancy.MakeFormReadOnly();
            });
            ui.newPanel.find('#DoneRound').text("Done with this Question Category");
            $(ui.newPanel).find(".CRUInterviewQuestion").show();
            $(ui.newPanel).find(".CRUVacQueCat").hide();
        }
        else {
            _$addbtn = $(ui.oldHeader).parents('.VacQueCat').next().find('#addVacQueCat');
            if (_vacQuecatid == "00000000-0000-0000-0000-000000000000") {
                _$addbtn.hide();
            }
            else {
                if (_$addbtn.length <= 0) {
                    $(ui.oldHeader).parents('.VacQueCat').next().append(MyVacancy.AddCategoryBtn);
                    _$addbtn = $(ui.oldHeader).parents('.VacQueCat').next().find('#addVacQueCat');
                }
                _$addbtn.show();
            }
        }

    };

    //$(".VacQueCat").live("accordionbeforeactivate", function (event, ui) {
    //    AccVacQueCatBA(event, ui);
    //    event.stopImmediatePropagation();
    //});

    //Initialize AccVacQueCat Accordian END

    //Initialize AccRounddetail Accordian
    $(".AccRounddetail").accordion(VacancyModel.AccSecondAttr);
    function AccRounddetailBA(event, ui) {
        var isFormOpen = AccordionOpenValidation();
        if (!isFormOpen) {
            event.stopImmediatePropagation();
            event.preventDefault();
            return false;
        }

        if (ui.newHeader.length > 0) {
            var _curAcc = $.trim($(ui.newHeader).attr('data-index'));
            var _newpan = $(ui.newPanel);
            var _vacrndId = $.trim($(ui.newHeader).attr('data-roundid'));
            switch (parseInt(_curAcc)) {
                case 1:
                    VacancyModel.CallPostMethod(MyVacancy.AccGetReviewersByVacancyRndURL + '?VacancyRndId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data);
                        MyVacancy.MakeFormReadOnly();
                    });
                    break;

                case 2:
                    VacancyModel.CallPostMethod(MyVacancy.AccGetVacQueCatByVacRndURL + '?VacancyRndId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data);
                        if ($(ui.newPanel).find('.VacQueCat').length > 0) {
                            $(ui.newPanel).find('.VacQueCat').accordion(VacancyModel.AccSecondAttr);

                            $(ui.newPanel).find('.VacQueCat').on("accordionbeforeactivate", function (event, ui) {
                                MyVacancy.AccVacQueCatBA(event, ui);
                                event.stopImmediatePropagation();
                                MyVacancy.MakeFormReadOnly();
                            });
                        }
                        MyVacancy.IgnoreAccordianVacancyAccHeaderClick($(ui.newPanel).find('.VacQueCat'));
                        MyVacancy.MakeFormReadOnly();
                    });
                    break;

                case 3:
                    VacancyModel.CallPostMethod(MyVacancy.AccGetRoundConfigDetailURL + '?VacancyRoundId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data)
                        MyVacancy.MakeFormReadOnly();
                    });
                    break;

                case 4:
                    VacancyModel.CallGetMethod(MyVacancy.GetRequiredDocumentsURL + '?VacRndId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data)
                    });
                    break;
            }
        }
        MyVacancy.MakeFormReadOnly();
    }
    $(".AccRounddetail").live("accordionbeforeactivate", function (event, ui) {

        AccRounddetailBA(event, ui);
        event.stopImmediatePropagation();
        MyVacancy.MakeFormReadOnly();
    });
    //Initialize AccRounddetail Accordian END

    $(".AccRound").accordion(VacancyModel.AccSecondAttr);
    $(".AccRound").live("accordionbeforeactivate", function (event, ui) {
        var _$FormOpen = $(ui.newHeader).closest('.AccRound');

        //Don't allow open other accordion if add Round accordion is open
        var IsFormOpen = false;
        var $Form;
        if (_$FormOpen.siblings('.RoundConfigNewForm:first').length > 0 && _$FormOpen.siblings('.RoundConfigNewForm:first').find('#frmRoundConfig').length > 0) {

            return false;
        }
        var isFormOpen = AccordionOpenValidation();
        if (!isFormOpen) {
            event.stopImmediatePropagation();
            event.preventDefault();
            return false;
        }


        if (ui.newHeader.length > 0) {
            var _vacrndId = $.trim(ui.newHeader.attr('data-vacroundid'));
            VacancyModel.CallPostMethod(MyVacancy.AccGetRevAndQueCountURL + '?VacancyRoundId=' + _vacrndId, function (data) {
                $(ui.newPanel).siblings().find('.AccRoundDetail').html("");
                $(ui.newPanel).find('.AccRoundDetail').html("");
                $(ui.newPanel).find('.AccRoundDetail').html(data);
                $(ui.newPanel).find('.AccRoundDetail').accordion(VacancyModel.AccSecondAttr).accordion('refresh');
                $(ui.newPanel).find('.AccRoundDetail').on("accordionbeforeactivate", function (event, ui) {
                    AccRounddetailBA(event, ui);

                    event.stopImmediatePropagation();
                });
            });


            var str = ui.newHeader.find('#rndtype').text();
            var newString = "Done with " + $.trim(str) + " Round";
            ui.newPanel.find('#DoneRound').text(newString);


            $('#addRoundConfig').hide();
            $('.note').hide();
        }
        else {
            $('#addRoundConfig').show();
            $('.note').show();

        }
        event.stopImmediatePropagation();
    });
    //Initialize AccRound Accordian END
    $('#addQuestion').live('click', function () {


        var $Quethat = $(this);
        var vacrndId = $Quethat.parents('.acc-Content').prev().attr('data-roundid');
        var vacQueCatId = $Quethat.parents('.acc-Content').prev().attr('data-pkid');

        if ($Quethat.prev('form').length > 0) {
            return false;
        }
        else {

            var _dataLink = MyVacancy.AddQuestionURL + '?VacRndId=' + vacrndId + '&VacQueCatId=' + vacQueCatId;
            $.get(_dataLink, function (data) {
                data = JSON.parse(data)

                var $fin = $(data.Data);

                MyVacancy.CreateSelectMenu($fin.find('#peopleC'));

                //$fin.find('#peopleC').selectmenu().selectmenu({
                //    width: 250,
                //    change: function (event, ui) {
                //        dropeVacancyQue($(this), ui.item.value);
                //    },
                //    _renderMenu: function (ul, items) {
                //        $(ul).addClass('r-right');
                //    }
                //}).selectmenu("menuWidget").addClass("overflow");

                $Quethat.before($fin);
                $Quethat.remove();
            });


            //VacancyModel.CallGetMethod(MyVacancy.AddQuestionURL + '?VacRndId=' + vacrndId + '&VacQueCatId=' + vacQueCatId, function (data) {
            //    $Quethat.before(data);

            //});
            //to hide Add Round btn
            $('#addRoundConfig').hide()
            return false;
        }
    });

    $('#addReviewers').live('click', function () {

        var $Rev = $(this);
        var vacrndId = $Rev.parents('.acc-Content').prev().attr('data-roundid');
        if ($Rev.prev('form').length > 0) {
            return false;
        }
        else {
            var _dataLink = MyVacancy.AddReviewerURL + '?VacRndId=' + vacrndId;
            VacancyModel.CallGetMethod(_dataLink, function (data) {
                $Rev.parent().prev().append(data);
                $Rev.remove();
            });
            return false;
        }
    });

    $('#addVacQueCat').live('click', function () {

        var $Rev = $(this);
        var _vacrndId = $Rev.parents('.acc-Content').prev().attr('data-roundid');
        if ($Rev.prev('form').length > 0) {
            return false;
        }
        else {

            VacancyModel.CallGetMethod(MyVacancy.AddVacQueCatURL + '?VacancyRndId=' + _vacrndId, function (data) {

                var VacQueCatAcc = $Rev.parents('.acc-Content:first').find('.VacQueCat').append(data);


                //Anand:Change the size for Active Accordion
                VacQueCatAcc.accordion(VacancyModel.AccSecondAttr).accordion('refresh').accordion({ active: -1 });
                //$Rev.remove();-- Depreciated
                //Anand to hide the button
                $Rev.hide();

            });

            return false;
        }
    });


});