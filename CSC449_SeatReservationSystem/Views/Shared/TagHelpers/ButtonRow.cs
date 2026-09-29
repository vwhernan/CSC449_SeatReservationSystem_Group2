using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace CSC449_SeatReservationSystem.Views.Shared.TagHelpers
{
    public class ButtonRow : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.MergeAttributes(_getAttributes());
            base.Process(context, output);
        }

        private TagBuilder _getAttributes()
        {
            var builder = new TagBuilder("div");
            builder.AddCssClass("d-flex");
            builder.AddCssClass("gap-2");
            builder.AddCssClass("btn-row");
            return builder;
        }

    }
}
