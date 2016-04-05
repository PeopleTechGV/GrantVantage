// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Linq;
using ATSCommon = ATS.WebUi.Common;
using Res = Resources;
using System.Collections.Generic;
using System.Web.Routing;
using System.Text;
using System.Globalization;

namespace System.Web.Mvc.Html
{
    public static class ValidationExtensions
    {
        private const string HiddenListItem = @"<li style=""display:none""></li>";
        private static string _resourceClassKey;

        public static string ResourceClassKey
        {
            get { return _resourceClassKey ?? String.Empty; }
            set { _resourceClassKey = value; }
        }

        // ValidationSummary

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper)
        {
            return MyValidationSummary(htmlHelper, false /* excludePropertyErrors */);
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            return MyValidationSummary(htmlHelper, excludePropertyErrors, null /* message */);
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, string message)
        {
            return MyValidationSummary(htmlHelper, false /* excludePropertyErrors */, message, (object)null /* htmlAttributes */);
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message)
        {
            return MyValidationSummary(htmlHelper, excludePropertyErrors, message, (object)null /* htmlAttributes */);
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, string message, object htmlAttributes)
        {
            return MyValidationSummary(htmlHelper, false /* excludePropertyErrors */, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, object htmlAttributes)
        {
            return MyValidationSummary(htmlHelper, excludePropertyErrors, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, string message, IDictionary<string, object> htmlAttributes)
        {
            return MyValidationSummary(htmlHelper, false /* excludePropertyErrors */, message, htmlAttributes);
        }

        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, IDictionary<string, object> htmlAttributes)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }
            FormContext formContext = null;
            if (htmlHelper.ViewContext.ClientValidationEnabled)
                formContext = htmlHelper.ViewContext.FormContext;
            
            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                if (formContext == null)
                {
                    // No client side validation
                    return null;
                }
                // TODO: This isn't really about unobtrusive; can we fix up non-unobtrusive to get rid of this, too?
                if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled && excludePropertyErrors)
                {
                    // No client-side updates
                    return null;
                }
            }

            string messageSpan;
            if (!String.IsNullOrEmpty(message))
            {
                TagBuilder spanTag = new TagBuilder("span");
                spanTag.SetInnerText(message);
                messageSpan = spanTag.ToString(TagRenderMode.Normal) + Environment.NewLine;
            }
            else
            {
                messageSpan = null;
            }

            StringBuilder htmlSummary = new StringBuilder();
            TagBuilder unorderedList = new TagBuilder("ul");

            IEnumerable<ModelState> modelStates = GetModelStateList(htmlHelper, excludePropertyErrors);

            foreach (ModelState modelState in modelStates)
            {
                foreach (ModelError modelError in modelState.Errors)
                {
                    string errorText = GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, modelError, null /* modelState */);
                    if (!String.IsNullOrEmpty(errorText))
                    {
                        TagBuilder listItem = new TagBuilder("li");
                        listItem.SetInnerText(errorText);
                        htmlSummary.AppendLine(listItem.ToString(TagRenderMode.Normal));
                    }
                }
            }

            if (htmlSummary.Length == 0)
            {
                htmlSummary.AppendLine(HiddenListItem);
            }

            unorderedList.InnerHtml = htmlSummary.ToString();

            TagBuilder divBuilder = new TagBuilder("div");
            divBuilder.MergeAttributes(htmlAttributes);
            divBuilder.AddCssClass((htmlHelper.ViewData.ModelState.IsValid) ? HtmlHelper.ValidationSummaryValidCssClassName : HtmlHelper.ValidationSummaryCssClassName);
            divBuilder.InnerHtml = messageSpan + unorderedList.ToString(TagRenderMode.Normal);

            if (formContext != null)
            {
                if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    if (!excludePropertyErrors)
                    {
                        // Only put errors in the validation summary if they're supposed to be included there
                        divBuilder.MergeAttribute("data-valmsg-summary", "true");
                    }
                }
                else
                {
                    // client val summaries need an ID
                    divBuilder.GenerateId("validationSummary");
                    formContext.ValidationSummaryId = divBuilder.Attributes["id"];
                    formContext.ReplaceValidationSummary = !excludePropertyErrors;
                }
            }
            return MvcHtmlString.Create(divBuilder.ToString(TagRenderMode.Normal));
            //return divBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }
        private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
        {
            string resourceValue = null;
            if (!String.IsNullOrEmpty(ResourceClassKey) && (httpContext != null))
            {
                // If the user specified a ResourceClassKey try to load the resource they specified.
                // If the class key is invalid, an exception will be thrown.
                // If the class key is valid but the resource is not found, it returns null, in which
                // case it will fall back to the MVC default error message.
                resourceValue = httpContext.GetGlobalResourceObject(ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            }
            return resourceValue ;//?? MvcResources.Common_ValueNotValidForProperty;
        }
        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!String.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState == null)
            {
                return null;
            }

            string attemptedValue = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
            return String.Format(CultureInfo.CurrentCulture, GetInvalidPropertyValueResource(httpContext), attemptedValue);
        }
        // Returns non-null list of model states, which caller will render in order provided.
        private static IEnumerable<ModelState> GetModelStateList(HtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            if (excludePropertyErrors)
            {
                ModelState ms;
                htmlHelper.ViewData.ModelState.TryGetValue(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, out ms);
                if (ms != null)
                {
                    return new ModelState[] { ms };
                }

                return new ModelState[0];
            }
            else
            {
                // Sort modelStates to respect the ordering in the metadata.                 
                // ModelState doesn't refer to ModelMetadata, but we can correlate via the property name.
                Dictionary<string, int> ordering = new Dictionary<string, int>();

                var metadata = htmlHelper.ViewData.ModelMetadata;
                if (metadata != null)
                {
                    foreach (ModelMetadata m in metadata.Properties)
                    {
                        ordering[m.PropertyName] = m.Order;
                    }
                }

                return from kv in htmlHelper.ViewData.ModelState
                       let name = kv.Key
                       orderby ordering.FirstOrDefault()//(name, ModelMetadata.DefaultOrder)
                       select kv.Value;
            }
        }

    }


    #region LanguageLabelExtensions
    public static class LanguageLabelExtensions
    {
        public static MvcHtmlString LanguageLabel(this HtmlHelper html, string name)
        {
            return LanguageLabelHelper(name);
        }
        public static MvcHtmlString LanguageLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LanguageLabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        private static MvcHtmlString LanguageLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            labelText = LanguageLabelHelper(labelText).ToString();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }
            TagBuilder tag = new TagBuilder("label");

            tag.MergeAttributes(htmlAttributes);

            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            tag.SetInnerText(labelText);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));

        }

        //[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString LanguageLabelFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
        {
            return LanguageLabelHelper(ModelMetadata.FromLambdaExpression(expression, html.ViewData));
        }

        private static MvcHtmlString LanguageLabelHelper(ModelMetadata metadata)
        {
            return LanguageLabelHelper(metadata.DisplayName);
        }
        private static MvcHtmlString LanguageLabelHelper(String label)
        {
            try
            {
                String localName = Res.Resources.LanguageDisplay(label);
                return MvcHtmlString.Create(String.IsNullOrEmpty(localName) ? label : localName);
            }
            catch
            {
                return MvcHtmlString.Create(label);
            }
        }
    }
    #endregion

}