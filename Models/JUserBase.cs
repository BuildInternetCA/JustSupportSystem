using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class JUserBase : JDBBase
    {
        public long? CreatedByUser { get; set; }
        [ForeignKey("CreatedByUser")]
        public virtual UserAccount? UserAccount { get; set; }
    }
}
