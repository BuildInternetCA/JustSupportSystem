using JustSupportSystem.Models;

namespace JustSupportSystem.DTO
{
    public class CompanyFormDTO : FormDTO
    {

        public long DbId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public long DefaultAgentId { get; set; }
        public override void SetForm(JDBContext db)
        {
            FormTitle = "New Company";
            FormDescription = "Please fill out the form below to create a new company.";
            ActionUrl = "/company/add";
            Method = "POST";
            SubmitButtonText = "Create";

            Fields = new List<FormDTOSettings>
            {
                new FormDTOSettings
                {
                    PropertyName = "DbId",
                    Type = InputType.Hidden,
                    IsRequired = true
                },
                new FormDTOSettings
                {
                    PropertyName = "Name",
                    Type = InputType.Text,
                    IsRequired = true
                },
                new FormDTOSettings
                {
                    PropertyName = "Code",
                    Type = InputType.Text,
                    IsRequired = true

                },
                 new FormDTOSettings
                {
                    PropertyName = "Notes",
                    Type = InputType.TextArea,
                    IsRequired = true
                }
            };

            var select2 = new FormDTOSettings
            {
                PropertyName = "DefaultAgentId",
                Type = InputType.Select,
                IsRequired = true,
                Options = db.UserAccounts.Where(p=>!p.IsDeleted).Where(ua => ua.UserRole == 2 || ua.UserRole == 3 ).OrderBy(p => p.FirstName)
                            .Select(ua => new CodeName
                            {
                                Code = ua.Id.ToString(),
                                Name = $"{ua.FirstName} {ua.LastName}"
                            }).ToList()
            };
            Fields.Add(select2);
        }
    }
}
