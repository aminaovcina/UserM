using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Entities.User;
using UserManagement.Domain.Models.Shared;

namespace ApplicationCore.IServices
{
    public interface IPermissionService 
    {
        public ApiResponse<List<Permission>> GetPermissions();
    }
}
