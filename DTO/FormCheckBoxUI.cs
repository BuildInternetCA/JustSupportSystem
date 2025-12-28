using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public class FormCheckBoxUI : BInput
    {
        public override string ToHtml()
        {
            if (!string.IsNullOrEmpty(Value) && (Value.ToLower().Equals("true") || Value.Equals("1") || Value.ToLower().Equals("yes")))
            {
                return @$"<div class=""form-group"">
                        <label class=""form-label"">
                            <input name=""{Label}"" type=""checkbox"" class=""form-checkbox"" checked {(IsRequired ? "required" : "")}>
                            {Label} {(IsRequired ? "<span class=\"label-required\">*</span>" : "")}
                        </label>
                    </div>";
            }
            return @$"<div class=""form-group"">
                        <label class=""form-label"">
                            <input value=""1"" name=""{Label}"" type=""checkbox"" class=""form-checkbox"" {(IsRequired ? "required" : "")}>
                            {Label} {(IsRequired ? "<span class=\"label-required\">*</span>" : "")}
                        </label>
                    </div>";
        }
    }
}
