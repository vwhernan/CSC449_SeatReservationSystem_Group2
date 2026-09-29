
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CSC449_SeatReservationSystem.Views.Shared.TagHelpers
{
    public class RowTagHelper : TagHelper
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
            builder.AddCssClass("row");
            return builder;
        }
    }
    public class RowColTagHelper : TagHelper
    {
        public int? ColWidth { get; set; }
        public string? Breakpoint { get; set; } = "lg";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.MergeAttributes(_getAttributes());
            base.Process(context, output);
        }

        private TagBuilder _getAttributes()
        {
            var builder = new TagBuilder("div");

            var colBuilder = new StringBuilder();
            colBuilder.Append("col");

            if (Breakpoint != null)
            {
                colBuilder.Append($"-{Breakpoint}");
            }

            if (ColWidth.HasValue)
            {
                colBuilder.Append($"-{(ColWidth == 0 ? "auto" : ColWidth)}");
            }

            builder.AddCssClass(colBuilder.ToString());
            return builder;
        }
    }
}
