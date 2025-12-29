
using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public class FormRadioUI : BInput
    {
        public List<CodeName> Options { get; set; } = new List<CodeName>();
        public override string ToHtml()
        {
            StringBuilder optionsHtml = new StringBuilder();
            foreach (var option in Options)
            {
                optionsHtml.AppendLine($@"<label class=""form-radio-label"">
                                             <input type=""radio"" name=""{Label}"" class=""form-check-input"" value=""{option.Code}"" {(!string.IsNullOrEmpty(Value) && option.Code == Value ? "checked" : "")} {(IsRequired ? "required" : "")}>
                                             {option.Name}
                                         </label>");
            }
            return @$"<div class=""form-group"">
                        <span class=""form-check-label"">{Label} {(IsRequired ? "<span class=\"label-required\">*</span>" : "")}</span>
                        <div class=""form-radio-group"">
                            {optionsHtml.ToString()}
                        </div>
                    </div>";
        }
    }
}
