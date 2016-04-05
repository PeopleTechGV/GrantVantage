function TemplateOffer() { }

TemplateOffer.GetNewOfferTemplateURL = "";
TemplateOffer.GetOfferTemplateByIdURL = "";
TemplateOffer.DeleteOfferTemplateURL = "";
TemplateOffer.DeleteOfferAttachmentURL = "";

$(document).ready(function () {
    $(".accOfferTemplates").accordion(VacancyModel.AccFirstAttr);

    TemplateOffer.IgnoreAccordianVacTemplateMainAccHeaderClick = function (ui) {
        $(ui).find("p > .ansoption").each(function () {
            $(this).live('click', function (event) {
                event.stopImmediatePropagation();
                event.preventDefault();
            });
        });
    }

    TemplateOffer.IgnoreAccordianVacTemplateMainAccHeaderClick($('.accOfferTemplates').find('.acc-header'));

    TemplateOffer.DeleteOfferTemplateConfirm = function (element) {
        var OfferTemplateId = $(element).parents(".acc-header").attr("data-offertemplateid");
        if (OfferTemplateId == undefined) {
            RemoveNewTemplate(element);
        }
        else {
            VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Award Template", 350, "Are you sure you want to delete this  Award Template?", "Yes", "Cancel", "DeleteOfferTemplate", element, null);
            return false;
        }
    }

    TemplateOffer.DeleteOfferAttachmentConfirm = function (element) {
        VacancyModel.ConfirmDialog("ConfirmDiv", "Delete Attachment", 350, "Are you sure you want to delete this attachments?", "Yes", "Cancel", "DeleteOfferAttachment", element, null);
        return false;
    }

    $('#btnAddTemplate').live("click", function () {
        var element = $(this);
        var dataLink = TemplateOffer.GetNewOfferTemplateURL;
        VacancyModel.CallGetMethod(dataLink, function (data) {
            if ($(".accOfferTemplates").find("form").length > 0) {
                $(".accOfferTemplates").find("form").remove();
            }
            $(element).parent().prev().append(data).accordion('destroy').accordion(VacancyModel.AccFirstAttr).accordion({ active: $(this).find(".acc-header").size() - 1 });
            HideAddButton();
        });
    });

    $(".accOfferTemplates").live("accordionbeforeactivate", function (event, ui) {
        if (ui.newHeader.length > 0) {
            var OfferTemplateId = $(ui.newHeader).attr("data-offertemplateid");
            if (OfferTemplateId != undefined) {
                if ($(ui.newHeader).parents(".accOfferTemplates").find("form").length > 0) {
                    $(ui.newHeader).parents(".accOfferTemplates").find("form").remove();
                }
                var dataLink = TemplateOffer.GetOfferTemplateByIdURL;
                dataLink = dataLink.replace("_OFFERTEMPLATEID_", OfferTemplateId);
                VacancyModel.CallGetMethod(dataLink, function (data) {
                    $(ui.newHeader).next().find(".AccOfferTemplateDetails").html("");
                    $(ui.newHeader).next().find(".AccOfferTemplateDetails").html(data);
                    HideAddButton();
                });
            }
        }
        else {
            ShowAddButton();
        }
    });

    TemplateOffer.AddOfferTemplateSuccess = function (data, OfferTemplateId) {
        data = JSON.parse(data);
        if (data.IsError == false) {
            if (data.Data[0] == "true") {
                var AccHeader = $("div[data-offertemplateid='" + OfferTemplateId + "']");
                var NewName = $(AccHeader).next().find("#OfferTemplateName").val();
                $(AccHeader).find(".lblTemplateName").html(NewName);
            }
            else {
                $(".NewOfferTemplate").attr("data-offertemplateid", data.Data[0]);
                $(".NewOfferTemplate").next().find("#OfferTemplateId").val(data.Data[0]);
                var Count = $(".accOfferTemplates").find(".acc-header").length;
                var NamePrefix = "Award Template " + Count + " - ";
                var TemplateName = $(".NewOfferTemplate").next().find("#OfferTemplateName").val();
                $(".NewOfferTemplate").find(".lblNamePrefix").html(NamePrefix);
                $(".NewOfferTemplate").find(".lblTemplateName").html(TemplateName);
                $(".AdditionalOfferAttachments").html(data.Data[1]);
                TemplateOffer.IgnoreAccordianVacTemplateMainAccHeaderClick($('.accOfferTemplates').find('.acc-header'));
            }
            TemplateOffer.MakeNumberCommaSeparate();
            VacancyModel.DisplaySuccessMeesage('#commonMessage', data.Message, '@ATS.WebUi.Common.Constants.JSCR_MSG_FADEOUT');
        }
        else {
            VacancyModel.DisplayErrorMeesage('#commonMessage', data.Message, '@ATS.WebUi.Common.Constants.JSCR_MSG_FADEOUT');
        }
    }

    TemplateOffer.MakeNumberCommaSeparate = function () {
        $(".ValueColumn").each(function () {
            VacancyModel.commaSeparateNumber(this);
        });

        $(".MaxValue").each(function () {
            VacancyModel.commaSeparateNumber(this);
        });
    }

    TemplateOffer.RemoveNumberCommaSeparate = function () {
        $(".ValueColumn").each(function () {
            var value = $(this).val().replace(/,/g, "");
            $(this).val(value)
        });

        $(".MaxValue").each(function () {
            var value = $(this).val().replace(/,/g, "");
            $(this).val(value)
        });
    }

    TemplateOffer.AddOfferTemplateFailed = function (data) {
        VacancyModel.DisplayErrorMeesage('#commonMessage', data.Message, '@ATS.WebUi.Common.Constants.JSCR_MSG_FADEOUT');
    }

    $(".chkInclude").live("click", function () {
        TemplateOffer.IncludeInOffer(this);
    });

    TemplateOffer.IncludeInOffer = function (element) {
        if ($(element).is(":checked")) {
            $(element).parents(".IncludeInOffer").next(".IncludeOption").show();
        }
        else {
            $(element).parents(".IncludeInOffer").next(".IncludeOption").hide();
        }
    }

    TemplateOffer.EnableCounterOffers = function (element) {
        if ($(element).is(":checked")) {
            $(".EditableByCandidate").show();
        }
        else {
            $(".EditableByCandidate").hide();
            $(".EditableByCandidate").attr("checked", false);
        }
    }

    TemplateOffer.PlacementTypeChange = function (element) {
        $(".Contract").hide();
        $(".Permanent").hide();
        var SelValue = $(element).val();
        if (SelValue != "") {
            if (SelValue == "2") {
                $(".Permanent").show();
            }
            else {
                $(".Contract").show();
            }
            TemplateOffer.ChangePayRate("#SalaryType");
        }
    }

    TemplateOffer.ChangePayRate = function (element) {
        $(".SalaryType").hide();
        $(".HourlyType").hide();
        $(".PieceType").hide();

        var SelValue = $(element).val();
        if (SelValue == "1") {
            $(".SalaryType").show();
        }
        else if (SelValue == "2") {
            $(".HourlyType").show();
        }
        else if (SelValue == "3") {
            $(".PieceType").show();
        }
    }

    $(".accOfferTemplates .acc-header").each(function (index) {
        var i = index + 1;
        NamePrefix = "Award Template " + i + " - ";
        $(this).find(".lblNamePrefix").html(NamePrefix);
    });
});

function DeleteOfferTemplate(Result, element) {
    if (Result) {
        var OfferTemplateId = $(element).parents(".acc-header").attr("data-offertemplateid");
        var dataLink = TemplateOffer.DeleteOfferTemplateURL;
        dataLink = dataLink.replace("_OFFERTEMPLATEID_", OfferTemplateId);
        VacancyModel.CallPostMethod(dataLink, function (data, message) {
            $(element).parents(".acc-header").next(".acc-Content").remove();
            $(element).parents(".acc-header").remove();
            ShowAddButton();
            VacancyModel.DisplaySuccessMeesage('#commonMessage', message, 3000);
            $(".accOfferTemplates .acc-header").each(function (index) {
                var i = index + 1;
                NamePrefix = "Award Template " + i + " - ";
                $(this).find(".lblNamePrefix").html(NamePrefix);
            });
        });
    }
}

function DeleteOfferAttachment(Result, element) {
    if (Result) {
        var AttachmentId = $(element).parents(".RowContent").attr("data-attachmentid");
        var dataLink = TemplateOffer.DeleteOfferAttachmentURL;
        dataLink = dataLink.replace("_OFFERATTACHMENTID_", AttachmentId);
        VacancyModel.CallPostMethod(dataLink, function (data, message) {
            $(element).parents(".RowContent").remove();
            VacancyModel.DisplaySuccessMeesage('#commonMessage', message, 3000);
            RemoveHeader();
        });
    }
}

function RemoveNewTemplate(element) {
    $(element).parents(".acc-header").next(".acc-Content").remove();
    $(element).parents(".acc-header").remove();
    ShowAddButton();
}

function HideAddButton() {
    $("#btnAddTemplate").hide();
}

function ShowAddButton() {
    $("#btnAddTemplate").show();
}