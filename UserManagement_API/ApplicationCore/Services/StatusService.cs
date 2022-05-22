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
    public class StatusService : IStatusService
    {
        public IGlobalRepository<Status> _statusRepository;

        public StatusService(IGlobalRepository<Status> statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public ApiResponse<List<Status>> GetStatuses()
        {
            var statuses = _statusRepository.GetAll();
            return ApiResponse<List<Status>>.Success(statuses == null ? new List<Status>() : statuses.ToList());
        }
    }
}
