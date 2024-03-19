using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;
using System.Text;

namespace ClinicManagementFrontEnd.Classes
{
    public static class EditorForExtension
    {
        public static string GetExpressionText<TModel, TResult>(
             this IHtmlHelper<TModel> htmlHelper,
             Expression<Func<TModel, TResult>> expression)
        {
            var expresionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
                .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

            return expresionProvider.GetExpressionText(expression);
        }

        public static IHtmlContent EditorForMany<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TValue>>> expression, string htmlFieldName = null) where TModel : class
        {
            var items = expression.Compile()(html.ViewData.Model);
            string style = "";
            var sb = new StringBuilder();

            if (String.IsNullOrEmpty(htmlFieldName))
            {
                var prefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

                GetExpressionText(html, expression);


                htmlFieldName = (prefix.Length > 0 ? (prefix + ".") : String.Empty) + GetExpressionText(html, expression);
            }

            var htmlContentBuilder = new HtmlContentBuilder();
            {
                foreach (var item in items)
                {
                    var dummy = new { Item = item };
                    var guid = Guid.NewGuid().ToString();
                    var memberExp = Expression.MakeMemberAccess(Expression.Constant(dummy), dummy.GetType().GetProperty("Item"));
                    var singleItemExp = Expression.Lambda<Func<TModel, TValue>>(memberExp, expression.Parameters);

                    var divTag = new TagBuilder("div");
                    if (htmlFieldName != "EvaluationCategoryList")
                        divTag.AddCssClass("row panel panel-success");
                    else
                    {
                        divTag.AddCssClass("row mb-2 mb-sm-0 py-25");
                    }
                    divTag.Attributes.Add("id", guid);
                    divTag.InnerHtml.AppendHtml(html.EditorFor(singleItemExp, null, $"{htmlFieldName}[{guid}]"));



                    var HiddenInput = new TagBuilder("input");
                    HiddenInput.Attributes.Add("type", "hidden");
                    HiddenInput.Attributes.Add("name", $"{htmlFieldName}.Index");
                    HiddenInput.Attributes.Add("value", guid);
                    HiddenInput.RenderSelfClosingTag();

                    divTag.InnerHtml.AppendLine(HiddenInput);

                    if (htmlFieldName != "EvaluationCategoryList")
                    {

                        var btnDiv = new TagBuilder("div");
                        btnDiv.AddCssClass("form-group col-md-2 display-table");

                        var deleteBtn = new TagBuilder("button");
                        deleteBtn.Attributes.Add("type", "button");
                        deleteBtn.AddCssClass("btn btn-danger display-cell floatright");
                        deleteBtn.Attributes.Add("onclick", $"DeleteLocation('{htmlFieldName}','{guid}')");
                        deleteBtn.InnerHtml.Append("Delete");
                        btnDiv.InnerHtml.AppendLine(deleteBtn);

                        divTag.InnerHtml.AppendLine(btnDiv);
                    }

                    htmlContentBuilder.AppendLine(divTag);


                    //htmlContentBuilder.Append(@$"<div class='form-group col-md-2 display-table'><button type='button' class='btn btn-danger display-cell floatright' onclick=DeleteLocation('{htmlFieldName}','{guid}')>Delete</button></div></div>");

                }
            }
            return htmlContentBuilder;

            //return new HtmlString(sb.ToString());
        }

    }
}
