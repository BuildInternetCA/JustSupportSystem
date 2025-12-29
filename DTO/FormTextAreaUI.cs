using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public class FormTextAreaUI :BInput
    {
        public int Rows { get; set; } = 4;
        public override string ToHtml()
        {
            return @$"<div class=""mb-3"">
                        <label class=""form-label"">{Label} {(IsRequired ? "<span class=\"label-required\">*</span>" : "")}</label>
                        <textarea placeholder='type here...' name=""{Label}""  class=""form-control"" rows=""{Rows}"" placeholder=""{Placeholder}"" {(IsRequired ? "required" : "")}>{Value}</textarea>
                    </div>";
        }
    }
}
