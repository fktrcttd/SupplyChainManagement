using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCM.Models.IdentityModels;
using SCM.Models;

namespace SCM.ViewModels
{
    public class CreateModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
        public string Name { get; set; }
        public int? RoleId { get; set; }
    }

    public class UpdateModel
    {
        public int Id { get; set; }

        [Required] public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public int? RoleId { get; set; }
    }

    public class RoleEditModel
    {
        public ScmRole Role { get; set; }
        public IEnumerable<ScmUser> Members { get; set; }
        public IEnumerable<ScmUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required] public string RoleName { get; set; }
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }
}