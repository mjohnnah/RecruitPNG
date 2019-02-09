using RecruitPNG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecruitPNG.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll(params string[] nav)
        {
            var query = entities.AsNoTracking().AsQueryable();
            foreach (var n in nav)
                query = query.Include(n);
            return query.AsEnumerable();
        }
        public IEnumerable<T> GetMany(Func<T, bool> where, Func<T, object> orderby, bool desc = false, params string[] nav)
        {
            var query = entities.AsNoTracking().AsQueryable();
            foreach (var n in nav)
                query = query.Include(n);
            query = query.Where(where).AsQueryable();
            if (desc == false)
            {
                query = query.OrderBy(orderby).AsQueryable();
            } else
            {
                query = query.OrderByDescending(orderby).AsQueryable();
            }
            return query.AsEnumerable();
        }

        public T Get(string id, params string[] nav)
        {
            var query = entities.AsNoTracking().AsQueryable();
            foreach (var n in nav)
                query = query.Include(n);
            return query.FirstOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.CreateDate = DateTime.Now;
            entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name ?? "username";
            entity.UpdateDate = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name ?? "username";
            entity.IPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.UpdateDate = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name ?? "username";
            entity.IPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            context.Entry(entity).State = EntityState.Modified;
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
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
