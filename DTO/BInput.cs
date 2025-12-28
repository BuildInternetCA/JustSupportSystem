using System;
using System.Collections.Generic;
using System.Text;

namespace JustSupportSystem.DTO
{
    public abstract class BInput : IInput
    {
        public string? Label { get; set; }
        public string? Placeholder { get; set; }
        public bool IsRequired { get; set; }

        public string? Value { get; set; }
        public abstract string ToHtml();
    }
}
