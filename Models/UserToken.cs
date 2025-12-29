using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class UserToken : JDBBase
    {
        public string? Token { get; set; }
        public bool IsExpired { get; set; }
        public bool IsEmailToken { get; set; }
        public string? UserAgent { get; set; }
        public string? IpAddress { get; set; }
        public bool IsTOPTVerified { get; set; }

        [ForeignKey("UserAccount")]
        public long UserID { get; set; }

        public UserAccount? UserAccount { get; set; }
    }
}
