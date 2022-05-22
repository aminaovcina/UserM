using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Domain.Models.User
{
    public class UserSearchFilterDto
    {
        public int page = 0;
        public int pageSize = 10;
        public string search = "";
        public string sort = "";
        public string order = "";
        public int? statusId = null;
        public List<int> permissions = null;
    }
}
