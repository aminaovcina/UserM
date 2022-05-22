using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Entities.User;
using UserManagement.Domain.Models.Shared;
using UserManagement.Domain.Models.User;

namespace ApplicationCore.IServices
{
    public interface IUserPermissionService
    {
        ApiResponse<UserPermissionsDTO> GetUserPermissions(int id);
        List<UserPermission> AssignUserPermissions(UserPermissionsDTO model);
    }
}
