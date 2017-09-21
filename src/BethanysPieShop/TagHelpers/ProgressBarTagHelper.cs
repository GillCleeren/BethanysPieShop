using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BethanysPieShop.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "progress-value")]
    public class ProgressBarTagHelper : TagHelper
    {

        //Not required, will get default value if not specified
        [HtmlAttributeName("progress-value")]
        public int ProgressValue { get; set; }

        [HtmlAttributeName("progress-minimum")]
        public int Minimum { get; set; }

        [HtmlAttributeName("progress-maximum")]
        public int Maximum { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Minimum >= Maximum)
            {
                Minimum = 0;
                Maximum = 100;
            }

            if (ProgressValue > Maximum || ProgressValue < Minimum)
            {
                ProgressValue = 0;
            }

            var percentageComplete = Math.Round((ProgressValue - Minimum) / (decimal)(Maximum - Minimum) * 100, 2);

            string content = $@"<div class='progress-bar progress-bar-success progress-bar-striped' role='progressbar' 
                                aria-valuenow='{ProgressValue}' aria-valuemin='{Minimum}' aria-valuemax='{Maximum}' style='width:{percentageComplete}%'>
                                    {percentageComplete}% Complete </div>";

            output.Content.AppendHtml(content);

            output.Attributes.SetAttribute("class", "progress");

            base.Process(context, output);
        }
    }
}
