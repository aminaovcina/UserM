using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagement.Domain.Models.Shared;

namespace ApplicationCore.IRepositories
{
    public interface IGlobalRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> criteria);
        T Get(long id);
        T Get(int id);
        T Get(string id);
        T GetFromComposite(int idFirst, string idSecond);
        T GetFromComposite(int idFirst, int idSecond);
        void Insert(T entity);
        T InsertWithReturn(T entity);
        void Insert(List<T> entity);
        List<T> InsertListWithReturn(List<T> entity);
        void Update(T entity);
        void UpdateList(List<T> entity);
        void Delete(T entity);
        void DeleteRange(List<T> entity);
        void SaveChanges();
        T GetWithInclude(Expression<Func<T, bool>> criteria, string[] navigationProperties = null);
        List<T> GetAllWithInclude(Expression<Func<T, bool>> criteria, string[] navigationProperties = null);
        List<T> GetAllWithInclude(string[] navigationProperties = null);
        PaginationDTO<T> GetPaginatedData(int page, int pageSize, string orderBy, Expression<Func<T, bool>> criteria, string[] navigationProperties = null, string order = null);

    }
}
