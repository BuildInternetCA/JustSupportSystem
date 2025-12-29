using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public class UIButton
    {
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? CssClass { get; set; } = "btn-primary";

        public UIButtonType Type { get; set; } = UIButtonType.Primary;
        public string ToHtml()
        {
            if (Type == UIButtonType.Primary)
            {
                return $"<a href=\"{Url}\" class=\"btn btn-1 mr-1 {CssClass}\">{Icon} {Title}</a>";
            }
            if (Type == UIButtonType.Secondary)
            {
                return $"<a href=\"{Url}\" class=\"btn btn-1 mr-1 {CssClass}\">{Icon} {Title} </a>";
            }
            return $"<a href=\"{Url}\" class=\"btn btn-1 mr-1  {CssClass}\">{Icon} {Title}</a>";
        }
        public string ToHtml(Dictionary<string, string> data)
        {
            var generatedHTML = ToHtml();
            if (!string.IsNullOrEmpty(Url) && Url.Contains("[~"))
            {
                foreach (var d in data)
                {
                    generatedHTML = generatedHTML.Replace($"[~{d.Key}]", $"{d.Value}");
                }
            }
            return generatedHTML;
        }
    }
    public enum UIButtonType
    {
        Primary,
        Secondary,
        Danger
    }
}
