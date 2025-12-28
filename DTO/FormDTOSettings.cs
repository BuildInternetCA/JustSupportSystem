namespace JustSupportSystem.DTO
{
    public class FormDTOSettings
    {
        public string? PropertyName { get; set; }
        public InputType Type { get; set; } = InputType.Text;
        public bool IsRequired { get; set; } = true;
        public List<CodeName>? Options { get; set; }
        public string? Value { get; set; }
    }
}
