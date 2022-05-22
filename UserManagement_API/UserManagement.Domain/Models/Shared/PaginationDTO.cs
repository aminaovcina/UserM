using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Domain.Models.Shared
{
    public class PaginationDTO<T> where T : class
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
    }
}
