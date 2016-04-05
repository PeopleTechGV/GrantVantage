function SelectableGrid() { }

SelectableGrid.DeleteUrl = "";

$(document).ready(function () {
    $("#selectable").selectable({
        cancel: 'ul.RowHeader',
        create:
        function () {
            $(".ShowOnMulti").hide();
        },

        stop: function () {
            var SelectedLength = $("#selectable > .ui-selected").length;
            if (SelectedLength == 0) {
                $(".ShowOnMulti").hide();
            }
            else {
                $(".ShowOnMulti").show();
            }
        },

        selecting: function (e, ui) {
            var curr = $(ui.selecting.tagName, e.target).index(ui.selecting);
            if (e.shiftKey && prev > -1) {
                $(ui.selecting.tagName, e.target).slice(Math.min(prev, curr), 1 + Math.max(prev, curr)).addClass('ui-selected');
            } else {
                prev = curr;
            }
        },
    });
});


function ConfirmDeleteRecords() {
    VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Records", 320, "Are you sure want to delete selected records?", "Yes", "Cancel", "DeleteRecords", this, "test");
    return false;
}

function ConfirmDeleteRecordsRoleWise() {
    var RecordNames = "";
    $("#selectable > .ui-selected").each(function () {
        var auth = $(this).attr("data-auth");
        if (auth.indexOf("Delete") == -1) {
            $(this).addClass("Error-UnAuthorized").removeClass("ui-selected")
            RecordNames = RecordNames + "  - " + $(this).children("li:first-child").text() + "<br /> ";
        }
    });
    if (RecordNames != "") {
        RecordNames = "<b>You are not authorized to delete below records.</b><br />" + RecordNames + "<b>Do you want to continue?</b>";
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Records", 320, RecordNames, "Yes", "Cancel", "DeleteRecords", this, "test");
    }
    else {
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Records", 320, "Are you sure want to delete selected records?", "Yes", "Cancel", "DeleteRecords", this, "test");
    }
    return false;
}

function DeleteRecords(Result, element) {
    if (Result == true) {
        var dataId = [];
        $("#selectable > .ui-selected").each(function () {
            dataId.push($(this).attr("data-id"));
        });
        if (dataId == '')
            return false;

        var deletelink = SelectableGrid.DeleteUrl;
        deletelink = deletelink.replace("_DATAID_", dataId);
        $.ajax({
            url: SelectableGrid.DeleteUrl,
            data: { 'id': dataId.toString() },
            type: "post",
            cache: false,
            success: function (response) {
                var data = JSON.parse(response)
                if (data.IsError) {
                    if (data.IsDefaultMessage) {
                        VacancyModel.DisplayErrorMeesage('#commonMessage', data.Message, 3000);
                    }
                    else {
                        VacancyModel.DisplayErrorMeesage('#commonMessage', 'Not able to delete records', 3000);
                    }
                }
                else {
                    $(".NavigationBar > #NavCompanySetupMenu").replaceWith(data.Data);
                    var DeleteRecords = $("#selectable > .ui-selected").length;
                    var Count = $("#btnBack").find("#lblCount").html();
                    Count = parseInt(Count) - parseInt(DeleteRecords);
                    $("#btnBack").find("#lblCount").html(Count);
                    $("#selectable > .ui-selected").remove();
                    $(".ActionBar #btnDelete").hide();
                    VacancyModel.DisplaySuccessMeesage('#commonMessage', 'Record Deleted Successfully', 3000);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                VacancyModel.DisplayErrorMeesage('#commonMessage', 'Not able to delete records', 3000);

            }
        });
    }
    $(".Error-UnAuthorized").removeClass("ui-selected").removeClass("Error-UnAuthorized");
}
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}