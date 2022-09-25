
using EmployeeCRUD.Core.Services.Contracts.Genaric;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        public UnitOfWork(DbContext _Context)
        {
            Context = _Context;
        }
        public int Commit() => Context.SaveChanges();
        public void Dispose() => Context.Dispose();
        public async Task<int> CommitAsync() => await Context.SaveChangesAsync();
    }
}
