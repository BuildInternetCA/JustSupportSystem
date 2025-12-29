using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Type = System.Type;

namespace JustSupportSystem.DTO
{
    public class FormUI
    {
        public FormUI()
        {
            SkipProperties = new List<string>
            {
                "FormTitle",
                "FormDescription",
                "ActionUrl",
                "Method",
                "SubmitButtonText",
                "SuccessMessage",
                "ErrorMessage"
            };

        }
        private List<string> SkipProperties;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ActionUrl { get; set; }
        public string? Method { get; set; }
        public string? SubmitButtonText { get; set; }
        public int Rows { get; set; } = 2;
        public List<BInput> Inputs { get; set; } = new List<BInput>();
        public string ToHtml()
        {
            StringBuilder inputsHtml = new StringBuilder();
            foreach (var input in Inputs)
            {
                inputsHtml.AppendLine(input.ToHtml());
            }
            return @$"<form class=""card"" action=""{ActionUrl}"" method=""{Method}"">
                         <div class=""card-header"">
                            <h3 class=""card-title"">{Title}<br/><small class='text-secondary' style='font-weight:normal !Important;'>{Description}</small></h3>
                          
                        </div>
                        <div class='card-body'>
                        {inputsHtml.ToString()}
                        </div>
                        <div class=""card-footer text-end"">
                            <button type=""submit"" class=""btn btn-primary"">{SubmitButtonText}</button>
                          </div>
                    </form>";
        }

        public FormUI GetFormFromDTO<T>(T data)
        {
            Type classType = typeof(T);
            PropertyInfo[] properties = classType.GetProperties();
            var formSettings = data as FormDTO;
            if(formSettings != null)
            {
                Title = formSettings.FormTitle;
                Description = formSettings.FormDescription;
                ActionUrl = formSettings.ActionUrl;
                Method = formSettings.Method;
                SubmitButtonText = formSettings.SubmitButtonText;
            }
            foreach (PropertyInfo property in properties.Where(p=> !SkipProperties.Any(x => x.Equals(p.Name))))
            {
                var prop = classType.GetProperty(property.Name);
                string value = string.Empty;
                if (prop != null)
                {
                    var valueInternal = prop.GetValue(data);
                    
                    if(valueInternal != null)
                    {
                        value = valueInternal.ToString() ?? string.Empty;
                    }
                }
                var propertyType = property.PropertyType.Name.ToLower();
               
                var setting = formSettings != null? formSettings.Fields.Find(f => f.PropertyName == property.Name) : null;

                if (setting != null)
                {
                    if (string.IsNullOrEmpty(setting.Value))
                    {
                        setting.Value = value;
                    }
                    if (setting.Type == InputType.Select)
                    {
                        FormSelectUI input = new FormSelectUI();
                        input.Label = property.Name;
                        input.IsRequired = setting.IsRequired;
                        input.Options = setting.Options;
                        input.Value = setting.Value;
                        Inputs.Add(input);

                    }
                    else if (setting.Type == InputType.Radio)
                    {
                        FormRadioUI input = new FormRadioUI();
                        input.Label = property.Name;
                        input.IsRequired = setting.IsRequired;
                        input.Options = setting.Options;
                        input.Value = setting.Value;
                        Inputs.Add(input);
                    }
                    else if (setting.Type == InputType.CheckBox)
                    {
                        FormCheckBoxUI input = new FormCheckBoxUI();
                        input.Label = property.Name;
                        input.IsRequired = setting.IsRequired;
                        input.Value = setting.Value;
                        Inputs.Add(input);
                    }
                    else if (setting.Type == InputType.TextArea)
                    {
                        FormTextAreaUI input = new FormTextAreaUI();
                        input.Label = property.Name;
                        input.IsRequired = setting.IsRequired;
                        input.Value = setting.Value;
                        Inputs.Add(input);
                    }
                    else
                    {
                        FormInputUI input = new FormInputUI();
                        input.Type = setting.Type;
                        input.Label = property.Name;
                        input.IsRequired = setting.IsRequired;
                        input.Value = setting.Value;
                        Inputs.Add(input);
                    }
                }
                else
                {
                    if (propertyType.Equals("string"))
                    {
                        FormInputUI input = new FormInputUI();
                        input.Type = InputType.Text;
                        input.Label = property.Name;
                        input.Value = value;
                        Inputs.Add(input);
                    }
                    if (propertyType.Equals("long") || propertyType.Equals("int64") || propertyType.Equals("int32"))
                    {
                        FormInputUI input = new FormInputUI();
                        input.Type = InputType.Number;
                        input.Label = property.Name;
                        input.Value = value;
                        Inputs.Add(input);
                    }
                    if (propertyType.Equals("datetime"))
                    {
                        FormInputUI input = new FormInputUI();
                        input.Type = InputType.Date;
                        input.Label = property.Name;
                        input.Value = value;
                        Inputs.Add(input);
                    }
                }
            }
            return this;
        }
        
    }
}
