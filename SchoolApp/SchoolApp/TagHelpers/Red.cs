using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SchoolApp.TagHelpers
{
    [HtmlTargetElement(Attributes = "red")]
    public class Red : TagHelper
    {
        private const string Class = "text-danger";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", Class);
        }
    }
}
