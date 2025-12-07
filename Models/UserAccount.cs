using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class UserAccount : JDBBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsSSOUser { get; set; }
        public short UserRole { get; set; }
        public bool PasswordResetRequired { get; set; }
        public DateTime LastLoginDate { get; set; }

        public UserRoleEnum GetUserRoleEnum()
        {
            return (UserRoleEnum)UserRole;
        }

        [ForeignKey("Company")]
        public long? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ICollection<Company>? Companies { get; set; }
    }
}
