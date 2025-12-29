using JustSupportSystem.Models;

namespace JustSupportSystem.DTO
{
    public class UserAccountFormDTO : FormDTO
    {
        public long CompanyId { get; set; }
        public long DbId { get; set; }
        public string FirstName { get; set; } = string.Empty;   
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public short Role { get; set; }
        public string? ResetAccount { get; set; }

        public override void SetForm(JDBContext db)
        {
            FormTitle = "New User";
            FormDescription = "Please fill out the form below to create a new user.";
            ActionUrl = $"/company/{CompanyId}/user/add";
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
                    PropertyName = "CompanyId",
                    Type = InputType.Hidden,
                    IsRequired = true
                },
                new FormDTOSettings
                {
                    PropertyName = "FirstName",
                    Type = InputType.Text,
                    IsRequired = true
                },
                new FormDTOSettings
                {
                    PropertyName = "LastName",
                    Type = InputType.Text,
                    IsRequired = true
                },
                new FormDTOSettings
                {
                    PropertyName = "Email",
                    Type = InputType.Email,
                    IsRequired = true

                },
                 new FormDTOSettings
                {
                    PropertyName = "Password",
                    Type = InputType.Password,
                    IsRequired = true
                },
                  new FormDTOSettings
                {
                    PropertyName = "ResetAccount",
                    Type = InputType.CheckBox,
                    IsRequired = false
                },
            };

            var select2 = new FormDTOSettings
            {
                PropertyName = "Role",
                Type = InputType.Select,
                IsRequired = true,
                Options = new List<CodeName>
                {
                    new CodeName { Code = "4", Name = "Manager" },
                    new CodeName { Code = "5", Name = "User" }
                }
            };
            Fields.Add(select2);
        }
    }
}
