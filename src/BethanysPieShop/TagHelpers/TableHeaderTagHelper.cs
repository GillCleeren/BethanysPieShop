using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    [HtmlTargetElement("table", Attributes = "header")]
    public class TableHeaderTagHelper : TagHelper
    {
        public string HeaderContent { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            TagBuilder header = new TagBuilder("h2");
            header.InnerHtml.Append(HeaderContent);
            output.PreElement.SetHtmlContent(header);
        }
    }
}
