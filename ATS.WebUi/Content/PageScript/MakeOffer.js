function MakeOffer() { }

MakeOffer.GetEmailContentURL = "";
MakeOffer.GetOfferLetterContentURL = "";

$(document).ready(function () {
    $("#ddlNoteToCandidate").live("change", function () {
        var EmailTemplateId = $(this).val();
        debugger;
        if (EmailTemplateId != "") {
            var dataLink = MakeOffer.GetEmailContentURL;
            dataLink = dataLink.replace("_EMAILTEMPLATEID_", EmailTemplateId);
            VacancyModel.CallGetMethod(dataLink, function (data) {
                debugger;
                $("#EmailSubject").val(data.Subject);
                $("#EmailDescription").css("display", "block");
                $("#EmailDescription").val(data.EmailDescription);
                $("#EmailDescription").css("display", "none");
                $("#EditEmailModel").find(".cleditorMain iframe").contents().find('body').html(data.EmailDescription);
            });
        }
    });

    $("#ddlOfferLetter").live("change", function () {
        var OfferLetterId = $(this).val();
        if (OfferLetterId != "") {
            var dataLink = MakeOffer.GetOfferLetterContentURL;
            dataLink = dataLink.replace("_OFFERLETTERID_", OfferLetterId);
            VacancyModel.CallGetMethod(dataLink, function (data) {
                $("#OfferLetter").css("display", "block");
                $("#OfferLetter").val(data.Description);
                $("#OfferLetter").css("display", "none");
                $("#EditOfferLetterModel").find(".cleditorMain iframe").contents().find('body').html(data.Description);
            });
        }
    });


    MakeOffer.CancelOffer = function (element) {
        
        var _ele = $(element);
        var vacOfferid = $(_ele).attr('data-offerid');
        if (vacOfferid === "00000000-0000-0000-0000-000000000000") {
            $(_ele).parents('#frmMakeOffer').parent().prev().remove();
            $(_ele).parents('#frmMakeOffer').parent().remove();
            $('.AccRoundDetail').find('#btnAddOffer').show();
        }
        else {
            var curr = $(_ele).closest('.acc-Content').prev();
            $(curr).trigger("click");
        }
    }

    MakeOffer.PlacementTypeChange = function (element) {
        
        if ($(element).val() != "") {
            if ($(element).val() == "2") {
                $(".Contract").hide();
                $(".Permanent").show();
            }
            else {
                $(".Contract").show();
                $(".Permanent").hide();
            }
        }
        else {
            $(".Contract").hide();
            $(".Permanent").hide();
        }
        MakeOffer.ChangePayRate("#SalaryType");
    }

    MakeOffer.ChangePayRate = function (element) {
        if ($(element).val() != "" && $("#ddlPlacementType").val() == "2") {
            $(".SalaryType").hide();
            $(".HourlyType").hide();
            $(".PieceType").hide();
            if ($(element).val() == "1") {
                $(".SalaryType").show();
            }
            else if ($(element).val() == "2") {
                $(".HourlyType").show();
            }
            else if ($(element).val() == "3") {
                $(".PieceType").show();
            }
        }
        else {
            $(".SalaryType").hide();
            $(".HourlyType").hide();
            $(".PieceType").hide();
        }
    }

    MakeOffer.CloseAllPopup = function () {
        CloseVacOfferModel();
        CloseCandidateAcceptModel();
        CloseCandidateDeclineModel();
        CloseCandidateCounterModel();
        CloseCandidateRetractAcceptanceModel();
    }
});

//MODEL POPUP
function OpenEditEmailModel(event) {
    var EmailTemplateId = $(event).prev("#ddlNoteToCandidate").val();
    if (EmailTemplateId != "") {
        $(event).prev("#ddlNoteToCandidate").removeClass('input-validation-error');
        $("#EditEmailModel").css("display", "block");
        $(".cover").css("display", "block");
    }
    else
    {
        $(event).prev("#ddlNoteToCandidate").addClass('input-validation-error');
    }
}

function CloseEditEmailModel() {
    $("#EditEmailModel").css("display", "none");
    $(".cover").css("display", "none");
}

function OpenEditOfferLetterModel(event) {
    var EmailTemplateId = $(event).prev("#ddlOfferLetter").val();
    if (EmailTemplateId != "") {
        $(event).prev("#ddlOfferLetter").removeClass('input-validation-error');
        $("#EditOfferLetterModel").css("display", "block");
        $(".cover").css("display", "block");
    }
    else {
        $(event).prev("#ddlOfferLetter").addClass('input-validation-error');
    }    
}

function CloseEditOfferLetterModel() {
    $("#EditOfferLetterModel").css("display", "none");
    $(".cover").css("display", "none");
}