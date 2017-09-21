using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "anchor-style")]//
    public class AnchorStyleTagHelper : TagHelper
    {
        public string AnchorStyle { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"btn btn-{AnchorStyle}");
        }
    }
}
