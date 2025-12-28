using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public interface IInput
    {
        string? Label { get; set; }
        string? Placeholder { get; set; }
        bool IsRequired { get; set; }
        string ToHtml();
    }
}
