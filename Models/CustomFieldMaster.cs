using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class CustomFieldMaster : JDBBase
    {
        public string ? FieldName { get; set; }

        public long? ClientCompanyId { get; set; }
        [ForeignKey("ClientCompanyId")]
        public virtual Company? ClientCompany { get; set; }

    }
}
