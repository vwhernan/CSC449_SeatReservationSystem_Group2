using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CSC449_SeatReservationSystem.Views.Shared.TagHelpers;

public class YesButton : TagHelper
{
    public string ButtonText { get; set; } = "Yes";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "input";
        output.MergeAttributes(_getAttributes());
        base.Process(context, output);
    }

    private TagBuilder _getAttributes()
    {
        var builder = new TagBuilder("input");
        builder.Attributes.Add("type", "submit");
        builder.Attributes.Add("value", ButtonText);
        builder.AddCssClass("btn btn-success");
        return builder;
    }
}