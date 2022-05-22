using ApplicationCore.IRepositories;
using ApplicationCore.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserManagement.Domain.Entities.User;
using UserManagement.Domain.Models.ExceptionHandling;
using UserManagement.Domain.Models.Shared;
using UserManagement.Domain.Models.User;

namespace ApplicationCore.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        public IGlobalRepository<UserPermission> _userPermissionRepository;
        public UserPermissionService(IGlobalRepository<UserPermission> userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }
        public ApiResponse<UserPermissionsDTO> GetUserPermissions(int id)
        {
            Expression<Func<UserPermission, bool>> criteria = e => e.UserId == id;
            var permissions = _userPermissionRepository.GetAllWithInclude(criteria, new string[] { "Permission" }).Select(x => x.Permission).ToList();
            return ApiResponse<UserPermissionsDTO>.Success(new UserPermissionsDTO { UserId = id, Permissions = permissions});
        }
        public List<UserPermission> AssignUserPermissions(UserPermissionsDTO model)
        {
            Expression<Func<UserPermission, bool>> dbSectorsCriteria = e => e.UserId == model.UserId && model.UserPermissionIds.Contains(e.PermissionId);
            var userPermissionsDb = _userPermissionRepository.GetAllWithInclude(dbSectorsCriteria);

            Expression<Func<UserPermission, bool>> dbSectorsCriteria2 = e => e.UserId == model.UserId && !model.UserPermissionIds.Contains(e.PermissionId);
            var userPermissionsToRemove = _userPermissionRepository.GetAllWithInclude(dbSectorsCriteria2);
            _userPermissionRepository.DeleteRange(userPermissionsToRemove);

            var userPermissionsForUpload = model.UserPermissionIds.Where(el => !userPermissionsDb.Any(e => e.PermissionId == el)).ToList();

            userPermissionsForUpload.ForEach(el =>
            {
                userPermissionsDb.Add(new UserPermission
                {
                    UserId = model.UserId,
                    PermissionId = el,
                });
            });
            return userPermissionsDb;
        }
    }
}
