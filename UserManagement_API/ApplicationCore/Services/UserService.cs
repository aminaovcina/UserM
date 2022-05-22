using ApplicationCore.IRepositories;
using ApplicationCore.IServices;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.User;
using UserManagement.Domain.Models.ExceptionHandling;
using UserManagement.Domain.Models.Shared;
using UserManagement.Domain.Models.User;
using BC = BCrypt.Net.BCrypt;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        public IGlobalRepository<User> _userRepository;
        public IUserPermissionService _userPermissionService;

        public UserService(IGlobalRepository<User> userRepository, IUserPermissionService userPermissionService)
        {
            _userRepository = userRepository;
            _userPermissionService = userPermissionService;
        }
        public ApiResponse<User> CreateUser(UserDTO model)
        {
            var user = new User
            {
                Username = model.Username,
                Password = BC.HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CreatedOn = DateTime.Now,
                StatusId = model.StatusId,
                IsDeleted = false
                
        };
            Expression<Func<User, bool>> criteria = e => e.Username == user.Username || e.Email == user.Email;
            if (_userRepository.GetWithInclude(criteria) != null)
                throw new AppException("User with same Username already exists.");

            return ApiResponse<User>.Success(_userRepository.InsertWithReturn(user));
        }
        public ApiResponse<User> EditUser(UserDTO model)
        {
            var userDbObj = _userRepository.Get(model.UserId);
            if(userDbObj == null)
                throw new KeyNotFoundException("User not found.");
            if (userDbObj.IsDeleted)
                throw new AppException("User deleted.");
            userDbObj.FirstName = model.FirstName;
            userDbObj.LastName = model.LastName;
            userDbObj.Email = model.Email;
            userDbObj.StatusId = model.StatusId;
            userDbObj.EditedOn = DateTime.Now;
            _userRepository.Update(userDbObj);
            return ApiResponse<User>.Success(userDbObj);
        }
        public ApiResponse<bool> DeleteUser(int id)
        {
            var userDbObj = _userRepository.Get(id);
            if (userDbObj == null)
                throw new KeyNotFoundException("User not found.");
            userDbObj.IsDeleted = true;
            userDbObj.EditedOn = DateTime.Now;
            _userRepository.Update(userDbObj);
            return ApiResponse<bool>.Success(true);
        }
        public ApiResponse<User> GetUserById(int id)
        {
            Expression<Func<User, bool>> criteria = e =>
            !e.IsDeleted && e.UserId == id;

            var userDbObj = _userRepository.GetWithInclude(criteria, new string[] { "Status"});
            if (userDbObj == null)
                throw new KeyNotFoundException("User not found.");
            if (userDbObj.IsDeleted)
                throw new AppException("User deleted.");
            return ApiResponse<User>.Success(userDbObj);
        }
        public ApiResponse<List<User>> GetUsers()
        {
            var users = _userRepository.GetAll();
            return ApiResponse<List<User>>.Success(users == null ? new List<User>() : users.ToList());
        }
        public ApiResponse<PaginationDTO<User>> GetUsersPaginated(int page, int pageSize, string search, string sort, string order, int? statusId, List<int> permissions = null)
        {
            if (search == null) search = "";

            Expression<Func<User, bool>> criteria = e =>

            (permissions == null || permissions.Count == 0 ||
            e.Permissions.Any(el => permissions.Contains(el.PermissionId))) &&

            (statusId == null ||
            e.StatusId == statusId) &&

            !e.IsDeleted  &&

            ((search == "" || e.FirstName.ToLower().Contains(search.ToLower()) || e.FirstName.ToLower().Contains(search.ToLower())) ||

            (search == "" || e.LastName.ToLower().Contains(search.ToLower()) || e.LastName.ToLower().Contains(search.ToLower())) ||

            (search == "" || e.Email.ToLower().Contains(search.ToLower()) || e.Email.ToLower().Contains(search.ToLower())) ||

            (search == "" || e.Username.ToLower().Contains(search.ToLower()) || e.Username.ToLower().Contains(search.ToLower())))
            ;

            return ApiResponse<PaginationDTO<User>>.Success(_userRepository.GetPaginatedData(page, pageSize, (sort == "" ? "UserId" : sort), criteria, new string[] { "Status", "Permissions" }, order));
        }

        public ApiResponse<UserPermissionsDTO> AssignUserPermissions(UserPermissionsDTO model)
        {
            var userDbObj = _userRepository.Get(model.UserId);
            if (userDbObj == null)
                throw new KeyNotFoundException("User not found.");
            if (userDbObj.IsDeleted)
                throw new AppException("User deleted.");

            userDbObj.Permissions = _userPermissionService.AssignUserPermissions(model);
            userDbObj.EditedOn = DateTime.Now;
            _userRepository.Update(userDbObj);
            return _userPermissionService.GetUserPermissions(model.UserId);
        }
    }
}
