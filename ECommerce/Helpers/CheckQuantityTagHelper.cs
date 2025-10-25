using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ECommerce.Helpers
{
    //[HtmlTargetElement("check-quantity")]
    //<check-quantity></check-quantity>
    [HtmlTargetElement("span", Attributes = "quantity,")]
    public class CheckQuantityTagHelper:TagHelper
    {

        [HtmlAttributeName("quantity")]
        public int Quantity { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Quantity > 0)
            {
                output.Content.SetContent($"{Quantity} Avilable In Stock");
                output.Attributes.SetAttribute("class", "badge bg-success rounded-pill");
            }
            else
            {
                output.Content.SetContent("Out of Stock");
                output.Attributes.SetAttribute("class", "badge bg-danger rounded-pill");
            }
        }

    }
}
