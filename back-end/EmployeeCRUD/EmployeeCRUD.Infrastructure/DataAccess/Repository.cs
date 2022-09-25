
using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.Interfaces.Genaric;
using EmployeeCRUD.Core.Utilities.LinqBuilder.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeCRUD.Infrastructure.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected DbContext dbContext;
        public DbContext DbContext { get; set; }
        protected DbSet<T> ActiveTable { get; set; }
        //------------------------------------------------------------------------------------------
        public Repository(DbContext Db)
        {
            this.DbContext = this.dbContext = Db;
            this.ActiveTable = DbContext.Set<T>();
        }


        //------------------------------------------------------------------------------------------
        public void Add(T Entity)
        {
            dbContext.Set<T>().Add(Entity);
        }
        //------------------------------------------------------------------------------------------
        public void AddRange(List<T> Entity)
        {
            dbContext.Set<T>().AddRange(Entity);
        }
        //------------------------------------------------------------------------------------------
        public T Find(long Id, bool IsAttach = true)
        {
            var Item = dbContext.Set<T>().Find(Id);
            if (Item != null && IsAttach)
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public async Task<T> FindAsync(long Id, bool IsAttach = true)
        {
            var Item = await dbContext.Set<T>().FindAsync(Id);
            if (Item != null && IsAttach)
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> Predecate)
        {
            var Item = await dbContext.Set<T>().FirstOrDefaultAsync(Predecate);
            if (Item != null)
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> Predecate,
            string[] includesString = null)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (includesString != null)
                query = includesString.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var Item = await query.FirstOrDefaultAsync(Predecate);
            if (Item != null)
            {
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            }
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public T FirstOrDefault()
        {
            var Item = dbContext.Set<T>().FirstOrDefault();
            if (Item != null)
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public T FirstOrDefault(Expression<Func<T, bool>> Predecate,
            string[] includesString = null)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (includesString != null)
                query = includesString.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var Item = query.FirstOrDefault(Predecate);
            if (Item != null)
            {
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            }
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public T FirstOrDefault(Expression<Func<T, bool>> Predecate
            , params Expression<Func<T, object>>[] includes)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (includes != null)
                foreach (var includeExpression in includes)
                    query = query.Include(includeExpression);
            var Item = query.FirstOrDefault(Predecate);
            if (Item != null)
            {
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            }
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public T FirstOrDefault(Expression<Func<T, bool>> Predecate)
        {
            var Item = dbContext.Set<T>().FirstOrDefault(Predecate);
            if (Item != null)
                dbContext.Entry<T>(Item).State = EntityState.Detached;
            return Item;
        }
        //------------------------------------------------------------------------------------------
        public ViewModel FirstOrDefault<ViewModel>(Expression<Func<T, ViewModel>> Dto, Expression<Func<T, bool>> Predecate, params Expression<Func<T, object>>[] includes)
        {
            return dbContext.Set<T>().Where(Predecate).Select(Dto).FirstOrDefault();
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }
        //------------------------------------------------------------------------------------------
        public PageingDataResponse<T> GetPaging(Expression<Func<T, bool>> Predicate,
            string OrderColumn, int PageSize, int PageNumber)
        {
            PageingDataResponse<T> pageingData = new PageingDataResponse<T>();
            pageingData.data = dbContext.Set<T>().Where(Predicate)
                .OrderBy(x => x.GetType().GetProperty(OrderColumn))
                .Skip((PageNumber) * PageSize)
                .Take(PageSize)
                .ToList();
            pageingData.total = pageingData.recordsTotal = dbContext.Set<T>().Count(Predicate);
            pageingData.PageIndex = PageNumber;
            pageingData.PageSize = PageSize;
            return pageingData;
        }
        //------------------------------------------------------------------------------------------
        public async Task<PageingDataResponse<TSelect>> GetPaging<TSelect>(System.Linq.Expressions.Expression<Func<T, bool>> Predicate,
            Expression<Func<T, TSelect>> Items, string OrderColumn, int PageSize, int PageNumber, string Direction,
            params Expression<Func<T, object>>[] includes)
        {
            PageingDataResponse<TSelect> pageingData = new PageingDataResponse<TSelect>();
            var query = dbContext.Set<T>().Where(Predicate);
            // include egger loading
            if (includes != null)
                foreach (var includeExpression in includes)
                    query = query.Include(includeExpression);
            pageingData.data = await query.OrderByField<T>(OrderColumn, Direction == "asc")
                .Skip((PageNumber) * PageSize)
                .Take(PageSize)
                .Select(Items)
                .ToListAsync();

            pageingData.total = pageingData.recordsTotal = dbContext.Set<T>().Count(Predicate);
            pageingData.PageIndex = PageNumber;
            pageingData.PageSize = PageSize;
            return pageingData;
        }
        //------------------------------------------------------------------------------------------
        public PageingDataResponse<T> GetPaging(System.Linq.Expressions.Expression<Func<T, bool>> Predicate, string OrderColumn, int PageSize, int PageNumber, string Direction, params Expression<Func<T, object>>[] includes)
        {
            PageingDataResponse<T> pageingData = new PageingDataResponse<T>();
            if (Predicate != null)
            {
                var query = dbContext.Set<T>().Where(Predicate);
                // include egger loading
                if (includes != null)
                    foreach (var includeExpression in includes)
                        query = query.Include(includeExpression);

                pageingData.data = query.OrderByField<T>(OrderColumn, Direction == "asc")
                .Skip((PageSize) * PageNumber)
                .Take(PageSize)
                .AsEnumerable()
                .ToList();
                pageingData.total = pageingData.recordsTotal = dbContext.Set<T>().Count(Predicate);
            }
            else
            {
                // include egger loading
                var query = dbContext.Set<T>().AsQueryable();
                if (includes != null)
                    foreach (var includeExpression in includes)
                        query = query.Include(includeExpression);

                pageingData.data = query
               .OrderByField<T>(OrderColumn, Direction == "asc")
               .Skip((PageSize) * PageNumber)
               .Take(PageSize)
               .ToList();
                pageingData.total = pageingData.recordsTotal = query.Count();
            }
            pageingData.PageIndex = PageNumber;
            pageingData.PageSize = PageSize;

            return pageingData;
        }

        //------------------------------------------------------------------------------------------
        public PageingDataResponse<object> GetPaging(Expression<Func<T, bool>> Predicate, string OrderColumn, int PageSize, int PageNumber, string Direction, Expression<Func<T, object>> Items)
        {
            PageingDataResponse<object> pageingData = new PageingDataResponse<object>();
            if (Predicate != null)
            {
                var query = dbContext.Set<T>().Where(Predicate).Select(Items).AsQueryable();
                pageingData.data = query.OrderByField<object>(OrderColumn, Direction == "asc").ToList()
                .Skip((PageSize) * PageNumber)
                .Take(PageSize)
                .ToList();
                pageingData.total = pageingData.recordsTotal = dbContext.Set<T>().Where(Predicate).Count();
            }
            else
            {
                // include egger loading
                var query = dbContext.Set<T>().Select(Items).AsQueryable();
                pageingData.data = query.OrderByField<object>(OrderColumn, Direction == "asc").ToList()
                .Skip((PageSize) * PageNumber)
                .Take(PageSize)
                .ToList();
                pageingData.total = pageingData.recordsTotal = dbContext.Set<T>().Where(Predicate).Count();
            }
            pageingData.PageIndex = PageNumber;
            pageingData.PageSize = PageSize;
            return pageingData;
        }

        //------------------------------------------------------------------------------------------
        public void Remove(T Entity)
        {
            dbContext.Entry<T>(Entity).State = EntityState.Deleted;
        }
        //------------------------------------------------------------------------------------------
        public void RemoveRang(IEnumerable<T> Entity)
        {
            dbContext.Set<T>().RemoveRange(Entity);
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> Predecate)
        {
            return dbContext.Set<T>().Where(Predecate);
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> Predecate, bool attached)
        {
            var data = dbContext.Set<T>().Where(Predecate).ToList();
            if (data != null)
                foreach (var item in data)
                    dbContext.Entry<T>(item).State = EntityState.Detached;
            return data;
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<ViewModel> Select<ViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> Predecate,
            Expression<Func<T, ViewModel>> Items,
             params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query;
            if (Predecate != null)
                query = dbContext.Set<T>().Where(Predecate).AsQueryable();
            else
                query = dbContext.Set<T>().AsQueryable();
            // include egger loading
            if (includes != null)
                foreach (var includeExpression in includes)
                    query = query.Include(includeExpression);
            return (IEnumerable<ViewModel>)query.Select(Items).AsEnumerable();
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<ViewModel> Select<ViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> Predecate,
            Expression<Func<T, ViewModel>> Items,
             params string[] includes)
        {
            var query = dbContext.Set<T>().Where(Predecate).AsQueryable();
            // include egger loading
            if (includes != null)
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return (IEnumerable<ViewModel>)query.Select(Items).AsEnumerable();
        }

        //------------------------------------------------------------------------------------------
        public IQueryable<ViewModel> Select<ViewModel>(Expression<Func<T, bool>> Predecate, Expression<Func<T, ViewModel>> Items, string OrderColumn = null, string Direction = "asc")
        {

            var query = dbContext.Set<T>().AsQueryable();

            if (Predecate != null)
                query = query.Where(Predecate);
            if (!string.IsNullOrEmpty(OrderColumn))
                query = query.OrderByField<T>(OrderColumn, Direction == "asc");
            return  query.Select(Items);
        }


        //------------------------------------------------------------------------------------------
        public void Update(T Entity)
        {
            dbContext.Entry<T>(Entity).State = EntityState.Modified;
        }


        //------------------------------------------------------------------------------------------
        public float Sum(Expression<Func<T, float>> selector, Expression<Func<T, bool>> Predicate = null)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (Predicate != null)
                query = query.Where(Predicate).AsQueryable();
            return query.Sum(selector);
        }



        //------------------------------------------------------------------------------------------
        public void UpdateRange(List<T> Entity)
        {
            this.dbContext.Set<T>().UpdateRange(Entity);
        }

        //------------------------------------------------------------------------------------------
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (includes != null)
                foreach (var includeExpression in includes)
                    query = query.Include(includeExpression);

            return query.ToList();
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<T> Top(int Count)
        {
            return dbContext.Set<T>().Take(Count).ToList();
        }
        //------------------------------------------------------------------------------------------

        public IEnumerable<T> Top(int Count, Expression<Func<T, bool>> Predecate)
        {
            return dbContext.Set<T>().Where(Predecate).Take(Count).ToList();
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<T> Top(int Count, Expression<Func<T, bool>> Predecate, string ColumnOrderBy, string Direction)
        {
            return dbContext.Set<T>()
                .Where(Predecate).
                OrderByField<T>(ColumnOrderBy, Direction == "asc")
                .Take(Count)
                .ToList();
        }
        //------------------------------------------------------------------------------------------
        public IEnumerable<ViewModel> Top<ViewModel>(int Count, Expression<Func<T, bool>> Predecate, Expression<Func<T, ViewModel>> Items, string ColumnOrderBy, string Direction)
        {
            return dbContext.Set<T>()
                .Where(Predecate).
                OrderByField<T>(ColumnOrderBy, Direction == "asc")
                .Take(Count)
                .Select(Items)
                .ToList();
        }

        //------------------------------------------------------------------------------------------
        public int Count(Expression<Func<T, bool>> Predicate = null)
        {
            if (Predicate != null)
                return dbContext.Set<T>().Where(Predicate).Count();
            else
                return dbContext.Set<T>().Count();
        }

        //------------------------------------------------------------------------------------------
        public decimal Sum(Func<T, decimal> Selector, Expression<Func<T, bool>> Predicate = null)
        {
            if (Predicate != null)
                return dbContext.Set<T>().Where(Predicate).Sum(Selector);
            else
                return dbContext.Set<T>().Sum(Selector);
        }
        //------------------------------------------------------------------------------------------
        public T LastOrDefault(Expression<Func<T, bool>> Predecate,
            string[] includesString = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null)
        {
            T item = null;
            var query = dbContext.Set<T>().AsQueryable();
            if (Predecate != null)
                query = query.Where(Predecate).AsQueryable();

            if (includesString != null && includesString.Length > 0)
                query = includesString.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (OrderBy != null)
            {
                //var orderedLINQ = OrderBy(query);
                item = OrderBy(query).FirstOrDefault();
                //if (item != null)
                //dbContext.Entry<T>(item).State = EntityState.Detached;
            }
            else
            {
                item = query.LastOrDefault();
                //if (item != null)
                //    dbContext.Entry<T>(item).State = EntityState.Detached;
            }
            return item;


        }

    }
}
