using EmployeeCRUD.Core.Entities.Common;
using EmployeeCRUD.Core.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.Extentions
{
    public static class ChangeTrackerExtention
    {

        public static void TrackAuditing(this ChangeTracker changeTracker,
            IUserDataProvider userDataProvider
            )
        {

            changeTracker.DetectChanges();
            var AuditedEntries = changeTracker
           .Entries().Where(e => e.Entity is ICreationAuditable);
            foreach (var entity in AuditedEntries)
            {
                // If the entity state is Added let's set
                // the CreationDate and CreatedByUserId properties
                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity is ICreationAuditable)
                    {
                        ((ICreationAuditable)entity.Entity).CreatedDate = DateTime.UtcNow;
                        if (userDataProvider != null && userDataProvider.UserId != 0)
                        {
                            ((ICreationAuditable)entity.Entity).CreatedBy =  userDataProvider.UserId;
                        }
                    }
                }
                else if (entity.State == EntityState.Deleted && entity.Entity is ISoftDeleteAuditable)
                {
                    entity.State = EntityState.Modified;
                    ((ISoftDeleteAuditable)entity.Entity).DeletedDate = DateTime.UtcNow ;
                }
            }
        }
    }
}
