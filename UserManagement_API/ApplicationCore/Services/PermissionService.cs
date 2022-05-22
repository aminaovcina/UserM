using ApplicationCore.IRepositories;
using ApplicationCore.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagement.Domain.Entities.User;
using UserManagement.Domain.Models.Shared;

namespace ApplicationCore.Services
{
    public class PermissionService : IPermissionService
    {
        public IGlobalRepository<Permission> _permissionRepository;

        public PermissionService(IGlobalRepository<Permission> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public ApiResponse<List<Permission>> GetPermissions()
        {
            var permissions = _permissionRepository.GetAll();
            return ApiResponse<List<Permission>>.Success(permissions == null ? new List<Permission>() : permissions.ToList());
        }
    }
}
