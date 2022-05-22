using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Entities.User;

namespace UserManagement.Domain.Models.User
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public List<Permission> UserPermissions { get; set; }
        public List<int> UserPermissionsIds { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? EditedOn { get; set; }
    }
}
