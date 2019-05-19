using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using SCM.DataService.DataContext;

namespace SCM.Models.IdentityModels
{
    public class ScmRole : IdentityRole<int, ScmUserRole>
    {
        public ScmRole()
        {
            
        }
        public ScmRole(string name) : this()
        {
            base.Name = name;
        }
        
        
        [NotMapped] public string UsersEmails => ConcatUsersEmails();

        private string ConcatUsersEmails()
        {
            var ctx = new AppDataContext();
            var users = this.Users.Select(link =>
            {
                var user = ctx.Users.FirstOrDefault(u => u.Id == link.UserId);
                return user;
            }).ToList();

            var emails = string.Join(", " ,users.Select(u => u.Email));
            return emails;
        }
        
    }
}