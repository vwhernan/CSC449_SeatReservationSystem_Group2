using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CSC449_SeatReservationSystem.Views.Shared.TagHelpers
{
    public class CancelButton : AnchorTagHelper
    {
        public CancelButton(IHtmlGenerator generator) : base(generator)
        {
            Action = "Index";
        }

        public string ButtonText { get; set; } = "Cancel";
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.MergeAttributes(_getAttributes());
            output.Content.Append(ButtonText);
            base.Process(context, output);
        }

        private TagBuilder _getAttributes()
        {
            var builder = new TagBuilder("a");
            builder.AddCssClass("btn");
            builder.AddCssClass("btn-secondary");
            return builder;
        }
    }
}