using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Entities.User;

namespace UserManagement.Domain.Models.User
{
    public class UserPermissionsDTO
    {
        public int UserId { get; set; }
        public List<int> UserPermissionIds { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
