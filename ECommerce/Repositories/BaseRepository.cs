using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repositories
{
    public class BaseRepository <TEntity> where TEntity : class
    {
        private DbSet<TEntity> Table;
        private EcommerceContext context;

        public BaseRepository(EcommerceContext _context)
        {
            context = _context;
            Table = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }
        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> expression = null,
            int PageSize = 10, int PageNumer = 1)
        {
            IQueryable<TEntity>  Data = expression== null 
                ? Table.AsQueryable() :
                Table.Where(expression);

            if (PageSize <= 0)
                PageSize = 10;
            if (PageNumer <= 1) 
                PageNumer = 1;
            //20 pro
            //pagenumber = 2
            //pagesize = 10
            //10*1

            int toskip = PageSize * (PageNumer - 1);

            if(toskip >Data.Count())
                PageNumer = 1;

           return Data.Skip(toskip).Take(PageSize);
         

        }

        public void Add(TEntity entity) { 
            Table.Add(entity);
            
        }
        public void AddRange(List<TEntity> entities)
        {
            Table.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Table.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }
        public void DeleteRange(List<TEntity> entities)
        {
            Table.RemoveRange(entities);
        }

        public void UnitofWork()
        {
            context.SaveChanges();
        }


    }
}
