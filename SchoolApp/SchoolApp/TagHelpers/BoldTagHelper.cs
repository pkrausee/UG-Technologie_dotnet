using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SchoolApp.TagHelpers
{
    [HtmlTargetElement(Attributes = "bold")]
    public class BoldTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bold");
            output.PreContent.SetHtmlContent("<b>");
            output.PostContent.SetHtmlContent("</b>");
        }
    }
}
