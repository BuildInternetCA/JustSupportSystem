using JustSupportSystem.Models;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JustSupportSystem.DTO
{
    public class UserAccountViewDTO
    {
        public long DbId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }

        public static UserAccountViewDTO GetView(UserAccount user)
        {
            UserAccountViewDTO model = new UserAccountViewDTO();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.DbId = user.Id;
            model.Email = user.Email;
            if (user.GetUserRoleEnum() == UserRoleEnum.Client_Manager)
            {
                model.UserRole = "Company Manager";
            }
            else if (user.GetUserRoleEnum() == UserRoleEnum.Client_User)
            {
                model.UserRole = "Company User";
            }
            else if (user.GetUserRoleEnum() == UserRoleEnum.Agent_Manager)
            {
                model.UserRole = "Agent Manager";
            }
            else if (user.GetUserRoleEnum() == UserRoleEnum.Agent_User)
            {
                model.UserRole = "Agent";
            }
            return model;
        }

        
    }
}
