using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.User;
using UserManagement.Domain.Models.Shared;
using UserManagement.Domain.Models.User;

namespace ApplicationCore.IServices
{
    public interface IUserService
    {
        ApiResponse<User> CreateUser(UserDTO model);
        ApiResponse<User> EditUser(UserDTO model);
        ApiResponse<bool> DeleteUser(int id);
        ApiResponse<User> GetUserById(int id);
        ApiResponse<List<User>> GetUsers();
        ApiResponse<PaginationDTO<User>> GetUsersPaginated(int page, int pageSize, string search, string sort, string order, int? statusId, List<int> permission = null);
        ApiResponse<UserPermissionsDTO> AssignUserPermissions(UserPermissionsDTO model);
    }
}
