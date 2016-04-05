using System.Web;
using System.Web.Optimization;

namespace ATS.WebUi.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/VacancyModel").Include(
            //            "~/Content/js/VacancyModel.js"));

            ////bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            ////         "~/Scripts/full/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //           "~/Scripts/*.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Content/js/jquery-ui-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/slidepanel").Include("~/Content/js/jquery.slidepanel.*"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryaccordian").Include("~/Content/js/jquery.prompt.accordian.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jSalaryRange").Include("~/Content/js/SalarySlider/*.js"));

            var division = new ScriptBundle("~/bundles/jDivision");
            division.Include("~/Content/PageScript/Division.js");
            bundles.Add(division);

            var SelectableGrid = new ScriptBundle("~/bundles/jSelectableGrid");
            SelectableGrid.Include("~/Content/js/SelectableGrid.js");
            bundles.Add(SelectableGrid);

            var templateVacancy = new ScriptBundle("~/bundles/jTemplatevacancy");
            templateVacancy.Include("~/Content/PageScript/TemplateVacancy.js");
            bundles.Add(templateVacancy);

            var templateOffer = new ScriptBundle("~/bundles/jTemplateOffer");
            templateOffer.Include("~/Content/PageScript/TemplateOffer.js");
            bundles.Add(templateOffer);

            var MakeOffer = new ScriptBundle("~/bundles/jMakeOffer");
            MakeOffer.Include("~/Content/PageScript/MakeOffer.js");
            bundles.Add(MakeOffer);

            var questionLibrary = new ScriptBundle("~/bundles/jQuestionLibrary");
            questionLibrary.Include("~/Content/PageScript/QuestionLibrary.js");
            bundles.Add(questionLibrary);

            var myVacancy = new ScriptBundle("~/bundles/jMyVacancy");
            myVacancy.Include("~/Content/PageScript/MyVacancy.js");
            bundles.Add(myVacancy);

            var newVacancy = new ScriptBundle("~/bundles/jNewVacancy");
            newVacancy.Include("~/Content/PageScript/NewVacancy.js");
            bundles.Add(newVacancy);

            var bundle = new ScriptBundle("~/bundles/jSalaryRange");
            bundle.Include("~/Scripts/JRangeSlider/jQAllRangeSliders-min.js");
            bundle.Include("~/Scripts/Dropdown/ui.dropdownchecklist.js");
            bundle.Include("~/Content/js/SalarySlider/jshashtable-2.1_src.js");
            bundle.Include("~/Content/js/SalarySlider/jquery.numberformatter-1.2.3.js");
            bundle.Include("~/Content/js/SalarySlider/tmpl.js");
            bundle.Include("~/Content/js/SalarySlider/jquery.dependClass-0.1.js");
            bundle.Include("~/Content/js/SalarySlider/draggable-0.1.js");
            bundle.Include("~/Content/js/SalarySlider/jquery.slider.js");
            bundles.Add(bundle);

            var MultiSelectDropDown = new ScriptBundle("~/bundles/jMultiSelectDropdown");
            MultiSelectDropDown.Include("~/Scripts/Dropdown/ui.dropdownchecklist.js");
            bundles.Add(MultiSelectDropDown);

            var Scriptbundle1 = new ScriptBundle("~/bundles/jquery1");
            Scriptbundle1.Include("~/Scripts/jquery-1.8.3.js");
            Scriptbundle1.Include("~/Content/js/jquery-ui-1.10.4.js");
            Scriptbundle1.Include("~/Content/js/jquery-ui-select.js");
            Scriptbundle1.Include("~/Scripts/jquery.unobtrusive-ajax.min.js");
            Scriptbundle1.Include("~/Scripts/jquery.validate.min.js");
            Scriptbundle1.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            bundles.Add(Scriptbundle1);

            var JQueryVal = new ScriptBundle("~/bundles/jqueryVal");
            JQueryVal.Include("~/Scripts/jquery-1.8.3.min.js");
            JQueryVal.Include("~/Scripts/jquery.unobtrusive-ajax.min.js");
            JQueryVal.Include("~/Scripts/jquery.validate.min.js");
            JQueryVal.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            bundles.Add(JQueryVal);

            var Scriptbundle2 = new ScriptBundle("~/bundles/jquery2");
            Scriptbundle2.Include("~/Content/js/jquery.slidepanel.setup.js");
            Scriptbundle2.Include("~/Content/js/Qaptcha.jquery.js");
            bundles.Add(Scriptbundle2);

            var slidepanel = new ScriptBundle("~/bundles/slidepanel");
            slidepanel.Include("~/Content/js/jquery.slidepanel.setup.js");
            bundles.Add(slidepanel);

            var Scriptbundle3 = new ScriptBundle("~/bundles/jquery3");
            Scriptbundle3.Include("~/Scripts/script.js");
            Scriptbundle3.Include("~/Content/js/VacancyModel.js");
            Scriptbundle3.Include("~/Content/PageScript/ConstantMsg.js");
            bundles.Add(Scriptbundle3);


            bundles.Add(new ScriptBundle("~/bundles/Dopdownlistscript").Include(
                     "~/Content/js/Dropdown/jquery.js",
                     "~/Content/js/Dropdown/ui.core.js",
                     "~/Content/js/Dropdown/ui.dropdownchecklist.js"));

            bundles.Add(new StyleBundle("~/Content/cssSalaryRange").Include("~/Content/css/SalarySlider/*.css",
                "~/Content/css/Dropdown/jquery-ui-1.8.13.custom.css",
                "~/Content/css/JRangeSlider/classic.css"));

            bundles.Add(new StyleBundle("~/Content/cssNumberRange").Include("~/Content/css/SalarySlider/*.css",
                "~/Content/css/JRangeSlider/classic.css"));

            var NumberRange = new ScriptBundle("~/bundles/jNumberRange");
            NumberRange.Include("~/Content/js/SalarySlider/jshashtable-2.1_src.js");
            NumberRange.Include("~/Content/js/SalarySlider/jquery.numberformatter-1.2.3.js");
            NumberRange.Include("~/Content/js/SalarySlider/tmpl.js");
            NumberRange.Include("~/Content/js/SalarySlider/jquery.dependClass-0.1.js");
            NumberRange.Include("~/Content/js/SalarySlider/draggable-0.1.js");
            NumberRange.Include("~/Content/js/SalarySlider/jquery.slider.js");
            bundles.Add(NumberRange);

            var stylebundle = new StyleBundle("~/Content/css1");
            stylebundle.Include("~/Content/css/jquery-ui.css");
            //stylebundle.Include("~/Content/css/careers.css");
            //stylebundle.Include("~/Content/css/dd.css");
            //stylebundle.Include("~/Content/css/flags.css");
            stylebundle.Include("~/Content/css/style.css");
            stylebundle.Include("~/Content/css/Desktop.css");
            stylebundle.Include("~/Content/css/Content.css");
            stylebundle.Include("~/Content/css/Icons.css");
            bundles.Add(stylebundle);

            var StyleResponsive = new StyleBundle("~/Content/cssResponsive");
            StyleResponsive.Include("~/Content/css/responsive.css");
            StyleResponsive.Include("~/Content/css/Tablet.css");
            bundles.Add(StyleResponsive);

            ////For Print profile
            var styleProfile = new StyleBundle("~/Content/cssPrintProfile");
            styleProfile.Include("~/Content/css/jquery-ui.css");
            styleProfile.Include("~/Content/css/careers.css");
            styleProfile.Include("~/Content/css/style.css");
            styleProfile.Include("~/Content/css/Desktop.css");
            bundles.Add(styleProfile);

            //..For CaptchaMvc
            var qaptchaBundle = new StyleBundle("~/Content/qCss");
            qaptchaBundle.Include("~/Content/css/Qaptcha.css");
            bundles.Add(qaptchaBundle);

            //..For Search Candidate
            var Search_cand = new StyleBundle("~/Content/SCCss");
            Search_cand.Include("~/Content/css/search-resumes.css");
            //Search_cand.Include("~/Content/css/SearchCandidate/search-resumes.css");
            bundles.Add(Search_cand);

            var Search_candjs = new ScriptBundle("~/bundles/SCJs");
            Search_candjs.Include("~/Content/js/search-resumes.js");
            bundles.Add(Search_candjs);

            var AjaxVal = new ScriptBundle("~/bundles/AjaxVal");
            //AjaxVal.Include("~/Scripts/jquery.unobtrusive-ajax.min.js");
            AjaxVal.Include("~/Scripts/jquery.validate.min.js");
            AjaxVal.Include("~/Scripts/jquery.validate.unobtrusive.min.js");
            bundles.Add(AjaxVal);

            //..For MVC Grid Candidate used in Employee/MyCandidates
            var GridMvc = new ScriptBundle("~/bundles/GridMvcJS");
            GridMvc.Include("~/Content/js/GridMvc/gridmvc.js");
            bundles.Add(GridMvc);

            var GridMvcMyVac = new ScriptBundle("~/bundles/GridMvcMyVacJS");
            GridMvcMyVac.Include("~/Content/js/GridMvc/gridmvc-myVac.js");
            bundles.Add(GridMvcMyVac);

            var GridMvcCSS = new StyleBundle("~/Content/GridMvcCSS");
            GridMvcCSS.Include("~/Content/css/GridMvc/Gridmvc.css");
            GridMvcCSS.Include("~/Content/css/GridMvc/Bootstrap.css");
            bundles.Add(GridMvcCSS);

            bundles.Add(new StyleBundle("~/Content/Dopdownlistscriptcss").Include("~/Content/css/Dropdown/*.css"));

            bundles.Add(new StyleBundle("~/Content/hintCss").Include("~/Content/css/Hint/*.css"));




            //var InlineEditing = new ScriptBundle("~/bundles/InlineEditing");
            //InlineEditing.Include("~/Content/js/InlineEditing/InlineControls.js");
            //InlineEditing.Include("~/Content/js/InlineEditing/InlineEdit.js");
            //bundles.Add(InlineEditing);
            //var InlineEditingCss = new StyleBundle("~/Content/InlineEditingCss");
            //InlineEditingCss.Include("~/Content/css/InlineEditing/InlineEdit.css");
            //bundles.Add(InlineEditingCss);


            //..For Time Picker
            var TimePickerCSS = new StyleBundle("~/bundles/TimePickerCSS");
            TimePickerCSS.Include("~/Content/TimeControl/css/jquery.ui.timepicker.css");
            bundles.Add(TimePickerCSS);

            var TimePickerJS = new ScriptBundle("~/bundles/TimePickerJS");
            TimePickerJS.Include("~/Content/TimeControl/js/jquery.ui.timepicker.js");
            bundles.Add(TimePickerJS);


            //..For Calendrical Time Picker
            var CalendricalCSS = new StyleBundle("~/bundles/CalendricalCSS");
            CalendricalCSS.Include("~/Content/calendrical/calendrical.css");
            bundles.Add(CalendricalCSS);

            var CalendricalJS = new ScriptBundle("~/bundles/CalendricalJS");
            CalendricalJS.Include("~/Content/calendrical/calendrical.js");
            bundles.Add(CalendricalJS);

            var jAjaxFileUpload = new ScriptBundle("~/bundles/jAjaxFileUpload");
            jAjaxFileUpload.Include("~/Content/js/AjaxFileUpload/AjaxFileUpload.js");
            bundles.Add(jAjaxFileUpload);
        }
    }
}