using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public class FormInputUI : BInput
    {
        public InputType Type { get; set; } = InputType.Text;

        public override string ToHtml()
        {
            if (InputType.Hidden==Type)
            {
                return @$"<input placeholder='type here...' value=""{Value}""  name=""{Label}""  type=""{Type.ToString().ToLower()}"" class=""form-control"" placeholder=""{Placeholder}"" {(IsRequired ? "required" : "")}>";
            }
            return @$"<div class=""mb-3"">
                        <label class=""form-label"">{Label} {(IsRequired ? "<span class=\"label-required\">*</span>" : "")}</label>
                        <input placeholder='type here...' value=""{Value}""  name=""{Label}""  type=""{Type.ToString().ToLower()}"" class=""form-control"" placeholder=""{Placeholder}"" {(IsRequired ? "required" : "")}>
                    </div>";
        }
    }

    public enum InputType
    {
        Text,
        Email,
        Password,
        Number,
        Date,
        Tel,
        Url,
        TextArea,
        Select,
        CheckBox,
        Radio,
        Hidden
    }
}
