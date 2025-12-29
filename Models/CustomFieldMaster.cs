using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class CustomFieldMaster : JDBBase
    {
        public string ? FieldName { get; set; }

        public short? FieldType{ get; set; }
        public bool IsRequired { get; set; }
        public long? ClientCompanyId { get; set; }
        [ForeignKey("ClientCompanyId")]
        public virtual Company? ClientCompany { get; set; }

    }

    public enum CustomFieldType
    {
        Text = 1,
        Date = 2,
        TextArea = 3,
        Number = 4,
        Email = 5
    }
}
