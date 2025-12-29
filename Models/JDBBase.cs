using System.ComponentModel.DataAnnotations;

namespace JustSupportSystem.Models
{
    public abstract class JDBBase
    {
        public JDBBase() 
        {
            DateAdded = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
            IsDeleted = false; 
        }
        [Key]
        public long Id { get; set; }
        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
