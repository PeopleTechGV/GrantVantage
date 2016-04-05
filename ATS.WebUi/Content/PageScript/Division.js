function Division() { };
Division.GetAllPositionType = "";
Division.GetPositionTypeModel = "";
Division.GetAllJobLocation = "";
Division.GetJobLocationModel = "";

Division.AddPositionTypeBtn = {};
Division.AddJoblocationBtn = {};

$(document).ready(function () {

    $("#DivisionAcc").accordion({
        icons: VacancyModel.AccFirstIcon,
        heightStyle: "content",
        collapsible: true,
        active: 5,
        animate: true
    });

    var plink = document.createElement('a');
    plink.href = "javascript:void(0);";
    plink.className = "btn CaseUpper";
    plink.id = "addPositionType"
    plink.innerHTML = "Add Positiontype";
    Division.AddPositionTypeBtn = plink;

    var jlink = document.createElement('a');
    jlink.href = "javascript:void(0);";
    jlink.className = "btn CaseUpper";
    jlink.id = "addJobLocation";
    jlink.innerHTML = "Add joblocation";
    Division.AddJoblocationBtn = jlink;

    $("#DivisionAcc").live("accordionbeforeactivate", function (event, ui) {
        //var isFormOpen = AccordionOpenValidation();
        //if (!isFormOpen) {
        //    event.stopImmediatePropagation();
        //    event.preventDefault();
        //    return false;
        //}
        if (ui.newHeader.length > 0) {
            var _curAcc = $.trim(ui.newHeader.attr('data-index'));
            var _DivId = $.trim(ui.newHeader.attr('data-pkid'));
            switch (parseInt(_curAcc)) {
                case 0:
                    VacancyModel.CallPostMethod(Division.GetAllPositionType + '?DivisionId=' + _DivId, function (data) {
                        var _selectedpositiontype = $("#SelectPositionType").val();
                        _selectedpositiontype = _selectedpositiontype.split(";");
                        $(ui.newPanel).closest(".acc-Content").html("");
                        $(ui.newPanel).closest(".acc-Content").append(data);
                        var inputTags = $('.styled_checkbox');
                        var selectedTags = [];
                        if (_selectedpositiontype.length > 0) {
                            for (var i = 0; i < _selectedpositiontype.length ; i++) {
                                var _curr = $('.styled_checkbox:input[value=' + _selectedpositiontype[i] + ']')
                                $(_curr).attr('checked', 'checked')
                                selectedTags.push($(_curr));
                            }
                        }
                        $(selectedTags).each(function (index, value) {
                            var that = $(this);
                            $(inputTags).each(function (index, value) {
                                if ($(this).val() == $(that).val()) {
                                    inputTags.splice(index, 1);
                                }
                            });
                        });
                        //$(inputTags).each(function (index, value) {
                        //    $(this).prop("checked", false);
                        //});
                    });
                    break;
                case 1:
                    VacancyModel.CallPostMethod(Division.GetAllJobLocation + '?DivisionId=' + _DivId, function (data) {
                        var _selectedjoblocation = $("#SelectJobLocation").val();
                        _selectedjoblocation = _selectedjoblocation.split(";");
                        $(ui.newPanel).closest(".acc-Content").children(".acc-inner").html("");
                        $(ui.newPanel).closest(".acc-Content").children(".acc-inner").append(data);



                        var inputTags = $('.styled_checkbox');
                        var selectedTags = [];
                        if (_selectedjoblocation.length > 0) {
                            for (var i = 0; i < _selectedjoblocation.length ; i++) {
                                var _curr = $('.chkboxJobLocation:input[value=' + _selectedjoblocation[i] + ']')
                                $(_curr).attr('checked', 'checked')
                                selectedTags.push($(_curr));
                            }
                        }
                        $(selectedTags).each(function (index, value) {
                            var that = $(this);
                            $(inputTags).each(function (index, value) {
                                if ($(this).val() == $(that).val()) {
                                    inputTags.splice(index, 1);
                                }
                            });
                        });
                        $(inputTags).each(function (index, value) {
                            $(this).prop("checked", false);
                        });
                    });
                    break;
            }
        }
        else {
        }

    });

    $('#addPositionType').live('click', function () {
        var _ele = $(this);
        var _DivId = $(_ele).parents(".acc-Content").prev().attr('data-pkid');

        VacancyModel.CallPostMethod(Division.GetPositionTypeModel + '?DivisionId=' + _DivId, function (data) {

            $(_ele).parent().prev().html("");
            $(_ele).parent().prev().html(data);
            $(_ele).parent().children('a').remove();
        });

    });
    $('#RemovePositionType').live('click', function () {
        var _ele = $(this).parents("#EditSection").html("");
        var _addbtn = Division.AddPositionTypeBtn;
        //$(_ele).hide();
        $(_ele).next().html(_addbtn);

    });




    $('#addJobLocation').live('click', function () {
        var _ele = $(this);
        var _DivId = $(_ele).parents(".acc-Content").prev().attr('data-pkid');

        VacancyModel.CallPostMethod(Division.GetJobLocationModel + '?DivisionId=' + _DivId, function (data) {

            $(_ele).parent().prev().html("");
            $(_ele).parent().prev().html(data);
            $(_ele).parent().children('a').remove();
        });

    });
    $('#RemoveJoblocation').live('click', function () {
        var _ele = $(this).parents("#EditSection").html("");
        var _addbtn = Division.AddJoblocationBtn;
        //$(_ele).hide();
        $(_ele).next().html(_addbtn);

    });

});