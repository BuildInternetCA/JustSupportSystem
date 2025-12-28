
using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public class FormSelectUI : BInput
    {
        public List<CodeName> Options { get; set; } = new List<CodeName>();
        public override string ToHtml()
        {
            StringBuilder optionsHtml = new StringBuilder();
            optionsHtml.Append("<option value=''>-select-</option>");
            foreach (var option in Options)
            {
                if(!string.IsNullOrEmpty(Value)) 
                {
                    if(option.Code == Value)
                    {
                        optionsHtml.AppendLine($@"<option value=""{option.Code}"" selected>{option.Name}</option>");
                        continue;
                    }
                }
                optionsHtml.AppendLine($@"<option value=""{option.Code}"">{option.Name}</option>");
            }
            return @$"<div class=""form-group"">
                        <label class=""form-label"">{Label} {(IsRequired ? "<span class=\"label-required\">*</span>" : "")}</label>
                        <select name=""{Label}"" class=""form-select"" {(IsRequired ? "required" : "")}>
                            {optionsHtml.ToString()}
                        </select>
                    </div>";
        }
    }
}
