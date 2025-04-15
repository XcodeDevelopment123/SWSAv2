using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace SWSA.MvcPortal.Commons.Extensions;

public static class HtmlHelperExtensions
{
    public static IHtmlContent RenderOptions(this IHtmlHelper htmlHelper, IEnumerable<SelectListItem> items)
    {
        var html = new System.Text.StringBuilder();

        foreach (var option in items)
        {
            var tag = new TagBuilder("option");
            tag.Attributes["value"] = option.Value;
            tag.InnerHtml.Append(option.Text);

            if (option.Selected)
                tag.Attributes["selected"] = "selected";
            if (option.Disabled)
                tag.Attributes["disabled"] = "disabled";

            using var writer = new System.IO.StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);
            html.AppendLine(writer.ToString());
        }

        return new HtmlString(html.ToString());
    }
}
