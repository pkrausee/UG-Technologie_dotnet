namespace SchoolApp.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class BigString : TagHelper
    {
        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h1";
            output.Attributes.SetAttribute("class", "bigString");
            output.Content.SetContent(Value);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
