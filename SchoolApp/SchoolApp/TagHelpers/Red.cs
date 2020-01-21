namespace SchoolApp.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

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
