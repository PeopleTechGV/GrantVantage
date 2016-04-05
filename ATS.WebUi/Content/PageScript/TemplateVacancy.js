function TemplateMyVacancy() { }

TemplateMyVacancy.FadeOut = 3000;

TemplateMyVacancy.GetTemplateUrl = "";

//AccRounddetail
TemplateMyVacancy.AccGetReviewersByVacancyRndURL = "";
TemplateMyVacancy.AccGetQueByVacancyRndURL = "";
TemplateMyVacancy.AccGetTRoundConfigDetailURL = "";
TemplateMyVacancy.AccGetVacQueCatByVacRndURL = "";

//Add Reviewer
TemplateMyVacancy.AddReviewerURL = "";
TemplateMyVacancy.AccGetRevAndQueCountURL = "";

//Add Question
TemplateMyVacancy.AddTQuestionURL = "";
TemplateMyVacancy.AddTVacQueCatURL = "";
TemplateMyVacancy.ChangeVacQueDropDownURL = "";
TemplateMyVacancy.AccGetQueByVacQueCatURL = "";

//Add Reviewer btn
TemplateMyVacancy.AddReviewersBtn = {};

//Delete VacRnd
TemplateMyVacancy.RemoveTVacRnd = "";

// Add Category btn
TemplateMyVacancy.AddCategoryBtn = {};

//AddQuestion Btn
TemplateMyVacancy.AddQuestionBtn = {};

//Delete QueCategory
TemplateMyVacancy.RemoveVacQueCat = "";
TemplateMyVacancy.TVacAccGetVacancyDetailsURL = "";
TemplateMyVacancy.TVacAccGetCompensationInfoURL = "";

TemplateMyVacancy.AccTvac = "";
TemplateMyVacancy.GetRequiredDocumentsURL = "";

$(document).ready(function () {

    //Make Accordion
    $(".TVacAccordian").accordion(VacancyModel.AccFirstAttr);
    $(".AccRound").accordion(VacancyModel.AccSecondAttr);
    $(".innerTVac").accordion(VacancyModel.AccFirstAttr);
    $(".accnewtreviewround").accordion(VacancyModel.AccSecondAttr);

    var link = document.createElement('a');
    link.href = "javascript void();";
    link.className = "btn CaseUpper";
    link.id = "addReviewers"
    link.innerHTML = "Add Reviewer";
    TemplateMyVacancy.AddReviewersBtn = link;

    var Catlink = document.createElement('a');
    Catlink.href = "javascript void();";
    Catlink.className = "btn CaseUpper";
    Catlink.id = "addTVacQueCat"
    Catlink.innerHTML = "Add Category";
    TemplateMyVacancy.AddCategoryBtn = Catlink;

    var Quelink = document.createElement('a');
    Quelink.href = "javascript void();";
    Quelink.className = "btn CaseUpper";
    Quelink.id = "addTQuestion"
    Quelink.innerHTML = "Add Question";
    TemplateMyVacancy.AddQuestionBtn = Quelink;

    TemplateMyVacancy.CreateSelectMenu = function (element) {
        $(element).selectmenu().selectmenu({
            width: 250,
            change: function (event, ui) {
                TemplateMyVacancy.DropVacQueOnChange($(this), ui.item.value, TemplateMyVacancy.ChangeVacQueDropDownURL);
            },
            _renderMenu: function (ul, items) {
                $(ul).addClass('r-right');
            }
        }).selectmenu("menuWidget").addClass("overflow");

    }

    function editCategory(element) {
        var _currHeader = $(element).closest(".acc-header");
        var $acc = $(element).closest(".acc-header").parent();
        var active = $acc.children('.acc-header').index(_currHeader);
        var _Tempid = $(element).parents('.acc-header:first').attr('data-tvacidonly');

        var $that = $(element).closest(".acc-header").next().find(".editsection");
        var _editUrl = TemplateMyVacancy.GetTemplateUrl;
        _editUrl = _editUrl.replace("_TVACID_", _Tempid);
        $.get(_editUrl, function (response) {
            var jsonData = JSON.parse(response);
            if (!jsonData.IsError) {

                $that.html(jsonData.Data);
                $that.show();
                //$acc.accordion("option", "active", parseInt(active));

                //$that.children('.innerTVac').accordion(VacancyModel.AccFirstAttr).accordion("option", "active", parseInt(0))


            }
            else {
                VacancyModel.DisplayErrorMeesage('#commonMessage', jsonData.Message, QuesitonLibrary.FadeOut);
            }

        });
    }

    TemplateMyVacancy.GetPositionBasedOnDivision = function (element) {
        var divisionId = $(element).val();
        $.post("/TemplateVacancy/GetPositionAndLocationByDivisionId", { divisionId: divisionId }, function (data) {
            data = JSON.parse(data);
            ////Job Location
            //var sel1 = $(element).parents('.vacancy-Details').find('#joblocation');
            //var ele1 = sel1.get(0);
            //sel1.empty();
            //var opt = document.createElement('option');
            //opt.innerHTML = '- Select -';
            //opt.value = '';
            //ele1.appendChild(opt);
            //for (var i = 0; i < data.JobLocationId.length; i++) {
            //    var opt = document.createElement('option');
            //    opt.innerHTML = data.LocalName[i];
            //    opt.value = data.JobLocationId[i];
            //    ele1.appendChild(opt);
            //}

            //Position Type
            var sel2 = $(element).parent().next().children('#positiontype');
            var ele2 = sel2.get(0);
            sel2.empty();
            var opt1 = document.createElement('option');
            opt1.innerHTML = '- Select-';
            opt1.value = '';
            ele2.appendChild(opt1);
            for (var i = 0; i < data.PositypeTypeId.length; i++) {
                var opt1 = document.createElement('option');
                opt1.innerHTML = data.LocalNamePosition[i];
                opt1.value = data.PositypeTypeId[i];
                ele2.appendChild(opt1);
            }
        });
    }

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

            VacancyModel.CallPostMethod(TemplateMyVacancy.AccGetRevAndQueCountURL + '?TVacancyRoundId=' + _vacrndId, function (data) {
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


            $('#addTVacRnd').hide();


        }
        else {
            $('#addTVacRnd').show();

        }
        event.stopImmediatePropagation();
    });


    //-------------------------------------------RUTUL PATEL : 20150408-------------------------------------------------

    function AccordionOpenValidation() {
        var _$FormOpen = $('.AccRound');
        var IsFormOpen = false;
        var $Form;
        if (_$FormOpen.find('form').not("#frmTVacRoundConfig,#frmVacQueCat").length > 0) {
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

    TemplateMyVacancy.IgnoreAccordianVacTemplateAccHeaderClick = function (ui) {
        console.log($(ui).find(".prfle-tab-head-brown > .ansoption").length);
        $(ui).find(".prfle-tab-head-brown > .ansoption").each(function () {
            $(this).live('click', function (event) {
                event.stopImmediatePropagation();
                event.preventDefault();
            });


        });
    }

    TemplateMyVacancy.IgnoreAccordianVacTemplateMainAccHeaderClick = function (ui) {
        console.log($(ui).find(".prfle-tab-head > .ansoption").length);
        $(ui).find(".prfle-tab-head > .ansoption").each(function () {
            $(this).live('click', function (event) {
                event.stopImmediatePropagation();
                event.preventDefault();
            });


        });
    }

    TemplateMyVacancy.IgnoreAccordianVacTemplateMainAccHeaderClick($('.TVacAccordian').find('.acc-header'));

    $('#addTVac').live('click', function () {
        var $that = $(this);
        var _editURL = $(this).attr("data-url");
        $.get(_editURL, function (response) {
            var jsonData = JSON.parse(response);
            if (!jsonData.IsError) {

                $that.parent().prev().append(jsonData.Data).accordion('destroy').accordion(VacancyModel.AccFirstAttr).accordion({ active: $(this).find(".acc-header").size() - 1 });
                var newacc = $that.parent().prev().find('.innerTVac');
                var _currHeader = $(newacc).children('.acc-header').attr('data-index', 1);
                var active = $(newacc).children('.acc-header').index(_currHeader);
                $(newacc).accordion(VacancyModel.AccSecondAttr).accordion("option", "active", parseInt(active));

            }

        });

    });

    $('#addTVacRnd').live('click', function () {
        var $that = $(this);
        var _dataURL = $(this).attr("data-url");
        var $formdiv = $that.parent().prev();
        $.get(_dataURL, function (response) {
            var jsonData = JSON.parse(response);
            if (!jsonData.IsError) {
                $formdiv.html(jsonData.Data);
                $formdiv.find('.accNewReviewRound').accordion(VacancyModel.AccSecondAttr).accordion('refresh').accordion({ active: $(this).find(".acc-header").size() - 1 });
            }
            $formdiv.show();
        });
    });

    $('#addReviewers').live('click', function () {
        var $Rev = $(this);
        var vacrndId = $Rev.parents('.acc-Content').prev().attr('data-roundid');
        var TVacId = $Rev.parents(".AccTVacRounds").parent().prev().attr("data-recordid");
        if ($Rev.prev('form').length > 0) {
            return false;
        }
        else {
            var _dataLink = TemplateMyVacancy.AddReviewerURL + '?VacRndId=' + vacrndId + "&TVacId=" + TVacId;
            VacancyModel.CallGetMethod(_dataLink, function (data) {
                $Rev.parent().prev().append(data);
                $Rev.remove();
            });
            return false;
        }
    });

    $('#addTVacQueCat').live('click', function () {
        var $Rev = $(this);
        var _tvacrndId = $Rev.parents('.acc-Content').prev().attr('data-roundid');
        var _tVacId = $Rev.parents(".AccTVacRounds").parent().prev().attr("data-recordid");
        if ($Rev.prev('form').length > 0) {
            return false;
        }
        else {
            VacancyModel.CallGetMethod(TemplateMyVacancy.AddTVacQueCatURL + '?TVacancyRndId=' + _tvacrndId + '&TVacId=' + _tVacId, function (data) {
                var VacQueCatAcc = $Rev.parents('.acc-Content:first').find('.VacQueCat').append(data);

                VacQueCatAcc.accordion(VacancyModel.AccSecondAttr).accordion('refresh').accordion({ active: $(this).find(".acc-header").size() - 1 });
                //$Rev.remove();
                $Rev.hide();
            });
            return false;
        }
    });

    $('#addTQuestion').live('click', function () {
        var $Quethat = $(this);
        var tvacrndId = $Quethat.parents('.acc-Content').prev().attr('data-roundid');
        var tvacQueCatId = $Quethat.parents('.acc-Content').prev().attr('data-pkid');
        var tvacId = $Quethat.parents('.AccTVacDetails').parent().attr('data-catid');
        if ($Quethat.prev('form').length > 0) {
            return false;
        }
        else {
            var _dataLink = TemplateMyVacancy.AddTQuestionURL + '?TVacRndId=' + tvacrndId + '&TVacQueCatId=' + tvacQueCatId + "&TVacId=" + tvacId;
            $.get(_dataLink, function (data) {
                data = JSON.parse(data)
                var $fin = $(data.Data);
                TemplateMyVacancy.CreateSelectMenu($fin.find('#peopleC'));
                $Quethat.before($fin);
                $Quethat.remove();
            });
            return false;
        }
    });

    TemplateMyVacancy.DeleteTVac = function (element) {
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Opportunity Template", 350, "Are you sure you want to delete this  Opportunity Template?", "Yes", "Cancel", "DeleteTVac", element, null);
        return false;
    }

    TemplateMyVacancy.DeleteTVacQueCatConfirm = function (element) {
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Question Category", 350, "Are you sure you want to delete this  Question Category?", "Yes", "Cancel", "DeleteVacQueCat", element, null);
        return false;
    }

    TemplateMyVacancy.DeleteTVacRnd = function (Myelement) {
        var msg = 'Are you sure you want to delete this Review Round?';
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Review Round", 400, msg, "Yes", "Cancel", "DeleteTVacRndConfirm", Myelement, null);
    }

    TemplateMyVacancy.RemoveTVac = function (element) {
        $(element).closest("form").closest('.acc-Content').prev().parents('.acc-Content').prev().remove()
        $(element).closest("form").closest('.acc-Content').prev().parents('.acc-Content').remove();
        $(element).closest("form").closest('.acc-Content').prev().remove();
        $(element).closest("form").closest('.acc-Content').remove();
        return false;
    }

    TemplateMyVacancy.DropVacQueOnChange = function (element, selectedValue, _dataLink) {
        var vacrndid = $(element).parents('.acc-Content:first').prev().attr('data-roundid');
        var _VacQueCatId = $(element).parents('.acc-Content:first').prev().attr('data-pkid');
        var vacqueid = '';
        var queid = '';
        var tvacid = '';
        if ($(element).parents('form').length > 0) {
            queid = $(element).parents('form').closest('.acc-Content').prev().attr('data-queid');

            //            vacqueid = $(element).parents('form').closest('.acc-Content').prev().attr('data-pkid');


            vacqueid = $(element).parents('form').closest('.acc-Content').find('form').find('#TVacQueId').val();

            tvacid = $(element).parents('.AccTVacDetails').parent().prev().attr("data-tvacidonly");

        }
        var $ele = $(element).parents('.Content:first').children('#addQuestion');
        _dataLink = _dataLink.replace("_SELDATA_", queid);
        _dataLink = _dataLink.replace("_VACRNDID_", vacrndid);
        _dataLink = _dataLink.replace("_VACQUEID_", vacqueid);
        _dataLink = _dataLink.replace("_VACQUECATID_", _VacQueCatId);
        _dataLink = _dataLink.replace("_NEWVAL_", selectedValue);
        _dataLink = _dataLink.replace("_TVACID_", tvacid);
        $.ajax({
            url: _dataLink,
            type: 'GET',
            cache: false,
            success: function (data) {
                data = JSON.parse(data)
                var $fin = $(data.Data);
                TemplateMyVacancy.CreateSelectMenu($fin.find('#peopleC'));
                $(element).parents('form').replaceWith($fin);
            },
            error: function (e) {
                //called when there is an error
                //console.log(e.message);
            }
        });
    }

    function AccVacQueCatBA(event, ui) {
        if ($(ui.oldHeader).length > 0) {
            var _TvacQuecatid = $(ui.oldHeader).attr('data-pkid');
        }
        var isFormOpen = AccordionOpenValidation();
        if (!isFormOpen) {
            event.stopImmediatePropagation();
            event.preventDefault();
            return false;
        }
        var _$addbtn = $(ui.newHeader).parents('.VacQueCat').next().find('#addTVacQueCat');
        var _$addQues = $(ui.newPanel).find('.Content').find('#addTQuestion');
        if (_$addQues.length <= 0) {
            $(ui.newPanel).find('.Content').html('');
            $(ui.newPanel).find('.Content').append(TemplateMyVacancy.AddQuestionBtn);
            _$addQues = $(ui.newPanel).find('.Content').find('#addTQuestion');
            console.log(_$addQues.length);
        }
        if (_$addbtn.length <= 0) {
            $(ui.newHeader).parents('.VacQueCat').next().append(TemplateMyVacancy.AddCategoryBtn);
            var _$addbtn = $(ui.newHeader).parents('.VacQueCat').next().find('#addTVacQueCat');
        }
        if (ui.newHeader.length > 0) {
            _$addbtn.hide();
            var _VacQueCatId = $.trim($(ui.newHeader).attr('data-pkid'));

            VacancyModel.CallGetMethod(TemplateMyVacancy.AccGetQueByVacQueCatURL + '?VacQueCatId=' + _VacQueCatId, function (data) {
                $(ui.newPanel).find('.QuestionDetails').html(data);


                //TemplateMyVacancy.MakeFormReadOnly();
            });
            ui.newPanel.find('#DoneRound').text("Done with this Question Category");
        }
        else {
            _$addbtn = $(ui.oldHeader).parents('.VacQueCat').next().find('#addTVacQueCat');
            if (_TvacQuecatid == "00000000-0000-0000-0000-000000000000") {
                _$addbtn.hide();
            }
            else {
                if (_$addbtn.length <= 0) {
                    $(ui.oldHeader).parents('.VacQueCat').next().append(TemplateMyVacancy.AddCategoryBtn);
                    _$addbtn = $(ui.oldHeader).parents('.VacQueCat').next().find('#addTVacQueCataddTVacQueCat');
                }
                _$addbtn.show();
            }
        }
    }

    $(".AccTVacRounds").live("accordionbeforeactivate", function (event, ui) {
        //var _$FormOpen = $(ui.newHeader).closest('.AccTVacRounds');
        //var IsFormOpen = false;
        //var $Form;
        //if (_$FormOpen.siblings('.RoundConfigNewForm:first').length > 0 && _$FormOpen.siblings('.RoundConfigNewForm:first').find('#frmRoundConfig').length > 0) {
        //    return false;
        //}

        //var isFormOpen = AccordionOpenValidation();
        //if (!isFormOpen) {
        //    event.stopImmediatePropagation();
        //    event.preventDefault();
        //    return false;
        //}
        if (ui.newHeader.length > 0) {
            var _vacrndId = $.trim(ui.newHeader.attr('data-vacroundid'));
            VacancyModel.CallPostMethod(TemplateMyVacancy.AccGetRevAndQueCountURL + '?TVacancyRoundId=' + _vacrndId, function (data) {
                $(ui.newPanel).find('.AccRoundDetail').html("");
                $(ui.newPanel).find('.AccRoundDetail').html(data);
                $(ui.newPanel).find('.AccRoundDetail').accordion(VacancyModel.AccSecondAttr).accordion('refresh');
                $(ui.newPanel).find('.AccRoundDetail').on("accordionbeforeactivate", function (event, ui) {
                    AccRounddetailBA(event, ui);
                    event.stopImmediatePropagation();
                });
            });
            var str = ui.newHeader.find('.lblRRName').text();
            var newString = "Done with " + $.trim(str) + " Round";
            ui.newPanel.find('#DoneRound').text(newString);
            $('#addTVacRnd').hide();
        }
        else {
            $('#addTVacRnd').show();
            $('.AccRoundDetail').find('form').remove();
        }
        event.stopImmediatePropagation();
    });

    $(".AccRounddetail").live("accordionbeforeactivate", function (event, ui) {
        AccRounddetailBA(event, ui);
        event.stopImmediatePropagation();
    });

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
                    VacancyModel.CallPostMethod(TemplateMyVacancy.AccGetReviewersByVacancyRndURL + '?VacancyRndId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data);
                    });
                    break;

                case 2:
                    VacancyModel.CallPostMethod(TemplateMyVacancy.AccGetVacQueCatByVacRndURL + '?VacancyRndId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data);
                        if ($(ui.newPanel).find('.VacQueCat').length > 0) {
                            $(ui.newPanel).find('.VacQueCat').accordion(VacancyModel.AccSecondAttr);
                            $(ui.newPanel).find('.VacQueCat').on("accordionbeforeactivate", function (event, ui) {
                                AccVacQueCatBA(event, ui);
                                event.stopImmediatePropagation();
                            });
                        }
                        TemplateMyVacancy.IgnoreAccordianVacTemplateAccHeaderClick($(ui.newPanel).find(".VacQueCat"));
                    });
                    break;

                case 3:
                    VacancyModel.CallPostMethod(TemplateMyVacancy.AccGetTRoundConfigDetailURL + '?TVacancyRndId=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data)
                    });
                    break;

                case 4:
                    VacancyModel.CallGetMethod(TemplateMyVacancy.GetRequiredDocumentsURL + '?TVacRnd=' + _vacrndId, function (data) {
                        $(ui.newPanel).html("");
                        $(ui.newPanel).html(data)
                    });
                    break;
            }
        }
    }

    TemplateMyVacancy.SetActionbar = function (event, ui) {
        var Name = $(ui.newHeader).find(".lblTemplateName").html();
        if (Name != undefined) {
            $(".NavigationBar > .ActionTemplateName").remove();
            var Actionbar = '<div class="NavItems ActionTemplateName"><span class="navTitle">' + Name + '</span><span class="chevron"></span></div>';
            $(".NavigationBar").append(Actionbar);
        }
    }

    $(".TVacAccordian").live("accordionbeforeactivate", function (event, ui) {
        if (ui.newHeader.length > 0) {
            TemplateMyVacancy.SetActionbar(event, ui);
            var _curAcc = $.trim($(ui.newHeader).attr('data-index'));
            var _TvacId = $.trim(ui.newHeader.attr('data-recordid'));
            var _Tempid = $(ui.newHeader).attr('data-tvacidonly');
            if (_Tempid != undefined) {
                var _curAcc = $.trim(ui.newHeader.attr('data-index'));
                var _TvacId = $.trim(ui.newHeader.attr('data-tvacidonly'));
                var datalink = TemplateMyVacancy.AccTvac + '?TVacId=' + _TvacId;
                VacancyModel.CallGetMethod(datalink, function (data, msg) {
                    $(ui.newPanel).find(".AccTVacDetails").html("");
                    $(ui.newPanel).find(".AccTVacDetails").html(data);
                });
            }

            switch (parseInt(_curAcc)) {
                case 1:
                    var _mode = $("div[name ='vacancyDetail']").attr('data-mode');
                    VacancyModel.CallGetMethod(TemplateMyVacancy.TVacAccGetVacancyDetailsURL + '?TVacId=' + _TvacId + '&mode=' + _mode, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                    });
                    break;

                case 2:
                    var _mode = $("div[name ='jobdesc']").attr('data-mode');
                    VacancyModel.CallGetMethod(TemplateMyVacancy.TVacAccGetJobDescriptionURL + '?TVacId=' + _TvacId + '&mode=' + _mode, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                    });
                    break;

                case 3:
                    var _mode = $("div[name ='compinfo']").attr('data-mode');
                    VacancyModel.CallGetMethod(TemplateMyVacancy.TVacAccGetCompensationInfoURL + '?TVacId=' + _TvacId + '&mode=' + _mode, function (data) {
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").html(data);
                    });
                    break;

                case 4:
                    var _mode = $("div[name ='appreviewprocess']").attr('data-mode');
                    var datalink = TemplateMyVacancy.GetTVacRoundByTVacId;
                    datalink = datalink.replace("_TVacID_", _TvacId)
                    VacancyModel.CallGetMethod(datalink, function (data, msg) {
                        $(ui.newPanel).closest(".acc-Content").find(".AccTVacRounds").html("");
                        $(ui.newPanel).closest(".acc-Content").find(".AccTVacRounds").html(data).accordion(VacancyModel.AccSecondAttr).accordion("refresh");
                        TemplateMyVacancy.IgnoreAccordianVacTemplateAccHeaderClick($(ui.newPanel).find(".AccTVacRounds"));
                        var RndNo = 0;
                        $.each($(ui.newPanel).find(".acc-header"), function (index, value) {
                            $(this).find(".CountRound").text(index + 1);
                            RndNo = index + 1;
                        })
                    });
                    break;
            }

            //Get Template Details
            //var _Tempid = $(ui.newHeader).attr('data-tvacidonly');
            //if (_Tempid != undefined) {
            //    var _curAcc = $.trim(ui.newHeader.attr('data-index'));
            //    var _TvacId = $.trim(ui.newHeader.attr('data-tvacidonly'));
            //    var datalink = TemplateMyVacancy.GetTVacRoundByTVacId;
            //    datalink = datalink.replace("_TVacID_", _TvacId)
            //    VacancyModel.CallGetMethod(datalink, function (data, msg) {
            //        $(ui.newPanel).find(".AccRound").html("");
            //        if ($(ui.newHeader).find("span.update").length > 0) {
            //            var $_updateelement = $(ui.newHeader).find("span.update");
            //            editCategory($_updateelement.find("a:first"));
            //        }
            //        else {
            //            var tvacacc = $(ui.newPanel).find(".innerTVac").accordion(VacancyModel.AccFirstAttr)
            //            var currentheader = $(tvacacc).children(".acc-header:first");
            //            var active = $(tvacacc).children('.acc-header').index(currentheader);
            //            $(tvacacc).accordion("option", "active", parseInt(active))

            //        }
            //        $(ui.newPanel).find(".AccRound").html(data).accordion(VacancyModel.AccFirstAttr).accordion("refresh");
            //        var _text = 0;
            //        $.each($(ui.newPanel).find(".AccRound").find(".cntRound"), function (index, value) {
            //            $(this).text(index + 1);
            //            _text = index + 1;
            //        })
            //        $(ui.newHeader).find('#vacrndcnt').text("(Rounds: " + _text + ")");
            //        TemplateMyVacancy.IgnoreAccordianVacTemplateAccHeaderClick($(ui.newPanel).find(".AccRound"));
            //    });
            //}

        }
    });

    $(".doneround").live("click", function () {
        $(this).closest('.acc-Content').prev().trigger('click');
    });
});

