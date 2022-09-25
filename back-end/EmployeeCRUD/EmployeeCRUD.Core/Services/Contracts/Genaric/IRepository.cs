using EmployeeCRUD.Core.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace  EmployeeCRUD.Core.Interfaces.Genaric
{
    public interface IRepository<T> where T : class
    {
        public DbContext DbContext { get; set; }
        T Find(long Id, bool IsAttach = true);
        Task<T> FindAsync(long Id, bool IsAttach = true);
        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> Predecate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> Predecate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> Predecate,
            string[] includesString = null);
        T LastOrDefault(Expression<Func<T, bool>> Predecate,
             string[] includesString = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null);
        T FirstOrDefault(Expression<Func<T, bool>> Predecate,
            string[] includesString = null);
        T FirstOrDefault(Expression<Func<T, bool>> Predecate,
          params Expression<Func<T, object>>[] includes);
        ViewModel FirstOrDefault<ViewModel>(Expression<Func<T, ViewModel>> Dto, Expression<Func<T, bool>> Predecate,
            params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAll();
        IEnumerable<T> Top(int Count);
        IEnumerable<T> Top(int Count, Expression<Func<T, bool>> Predecate);
        IEnumerable<T> Top(int Count, Expression<Func<T, bool>> Predecate, string ColumnOrderBy, string Direction);
        IEnumerable<ViewModel> Top<ViewModel>(int Count, Expression<Func<T, bool>> Predecate, Expression<Func<T, ViewModel>> Items, string ColumnOrderBy, string Direction);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Select(Expression<Func<T, bool>> Predecate);
        IEnumerable<T> Select(Expression<Func<T, bool>> Predecate, bool attached);
        IQueryable<ViewModel> Select<ViewModel>(Expression<Func<T, bool>> Predecate, Expression<Func<T, ViewModel>> Items, string OrderColumn = null, string Direction = "asc");
        IEnumerable<ViewModel> Select<ViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> Predecate,
           Expression<Func<T, ViewModel>> Items,
            params Expression<Func<T, object>>[] includes);
        IEnumerable<ViewModel> Select<ViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> Predecate,
        Expression<Func<T, ViewModel>> Items,
         params string[] includes);
        PageingDataResponse<T> GetPaging(Expression<Func<T, bool>> Predicate, string OrderColumn, int PageSize, int PageNumber);
       Task<PageingDataResponse<TSelect> > GetPaging<TSelect>(System.Linq.Expressions.Expression<Func<T, bool>> Predicate,
            Expression<Func<T, TSelect>> Items, string OrderColumn, int PageSize, int PageNumber, string Direction, params Expression<Func<T, object>>[] includes);
        PageingDataResponse<T> GetPaging(Expression<Func<T, bool>> Predicate, string OrderColumn, int PageSize, int PageNumber, string Direction, params Expression<Func<T, object>>[] includes);




        void Add(T Entity);
        void AddRange(List<T> Entity);
        void UpdateRange(List<T> Entity);
        void Update(T Entity);
        void Remove(T Entity);

        void RemoveRang(IEnumerable<T> Entity);
        float Sum(Expression<Func<T, float>> selector, Expression<Func<T, bool>> Predicate = null);
        int Count(Expression<Func<T, bool>> Predicate = null);
        decimal Sum(Func<T, decimal> Selector, Expression<Func<T, bool>> Predicate = null);
    }
}
