using ApplicationCore.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserManagement.Domain.Models;
using UserManagement.Domain.Models.Shared;
namespace UserManagement.Infrastructure.Repositories
{
    public class GlobalRepository<T> : IGlobalRepository<T> where T : class
    {
        private readonly MasterDbContext context;
        private DbSet<T> entities;

        public GlobalRepository(MasterDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> criteria)
        {
            return entities.Where(criteria).AsEnumerable();
        }
        public List<T> GetAllWithInclude(Expression<Func<T, bool>> criteria, string[] navigationProperties = null)
        {
            var query = context.Set<T>().AsQueryable();
            List<T> list;
            if (navigationProperties != null)
            {
                foreach (string navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);
            }

            list = query.Where(criteria).ToList<T>();

            return list;
        }
        public List<T> GetAllWithInclude(string[] navigationProperties = null)
        {
            var query = context.Set<T>().AsQueryable();
            List<T> list;
            if (navigationProperties != null)
            {
                foreach (string navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);
            }

            list = query.ToList<T>();

            return list;
        }
        public T GetWithInclude(Expression<Func<T, bool>> criteria, string[] navigationProperties = null)
        {
            var query = context.Set<T>().AsQueryable();
            T item;
            if (navigationProperties != null)
            {
                foreach (string navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);
            }

            item = query.Where(criteria).FirstOrDefault();

            return item;
        }
        public T Get(long id)
        {
            return entities.Find(id);
        }
        public T Get(int id)
        {
            return entities.Find(id);
        }
        public T Get(string id)
        {
            return entities.Find(id);
        }
        public T GetFromComposite(int idFirst, string idSecond)
        {
            return entities.Find(idFirst, idSecond);
        }
        public T GetFromComposite(int idFirst, int idSecond)
        {
            return entities.Find(idFirst, idSecond);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
        public T InsertWithReturn(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public void Insert(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            context.SaveChanges();
        }
        public List<T> InsertListWithReturn(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void UpdateList(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void DeleteRange(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.RemoveRange(entity);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public PaginationDTO<T> GetPaginatedData(int page, int pageSize, string orderBy, Expression<Func<T, bool>> criteria = null, string[] navigationProperties = null, string order = null)
        {
            var query = context.Set<T>().AsQueryable();
            List<T> list;
            if (navigationProperties != null)
            {
                foreach (string navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);
            }

            var dbContext = query.Where(criteria);
            var TotalItems = dbContext.Count();
            if (order == "asc")
            {
                list = dbContext
               .OrderBy(el => EF.Property<object>(el, orderBy))
               .Skip(page * pageSize)
               .Take(pageSize)
               .ToList();
            }
            else
            {
                list = dbContext
                .OrderByDescending(el => EF.Property<object>(el, orderBy))
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            }

            return new PaginationDTO<T>
            {
                Page = page,
                Pages = TotalItems / pageSize,
                Total = TotalItems,
                Data = list
            }; ;
        }

    }
}
