using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.DataAccess.DataSeed
{
    internal static class EntitySeeder
    {
        public static void Seed<TEntity>(this EntityTypeBuilder<TEntity> builder, IEntitySeed<TEntity> entitySeed) where TEntity : class
        {
            builder.HasData(entitySeed.GetEntitiesToSeed());
        }
    }
    internal interface IEntitySeed<out TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetEntitiesToSeed();
    }
}
