using JustSupportSystem.Models;

namespace JustSupportSystem.DTO
{
    public abstract class FormDTO
    {
        public string? FormTitle { get; set; }
        public string? FormDescription { get; set; }
        public string? ActionUrl { get; set; }
        public string? Method { get; set; }
        public string? SubmitButtonText { get; set; }
        public bool IsPartial { get; set; }
        public string SuccessMessage { get; set; } = "Your form has been submitted successfully.";
        public string ErrorMessage { get; set; } = "There was an error submitting your form. Please try again.";
        public List<FormDTOSettings> Fields { get; set; }

        public void AddField(FormDTOSettings field)
        {
            if (Fields == null)
            {
                Fields = new List<FormDTOSettings>();
            }
            Fields.Add(field);
        }

        public void RemoveField(FormDTOSettings field)
        {
            Fields?.Remove(field);
        }

        public void ClearFields()
        {
            Fields?.Clear();
        }

        public abstract void SetForm(JDBContext db);
    }
}
