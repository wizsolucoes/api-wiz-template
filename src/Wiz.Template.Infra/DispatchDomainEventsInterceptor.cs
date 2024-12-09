using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wizco.Common.Base;

namespace Wiz.Template.Infra;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IServiceContext _serviceContext;

    public DispatchDomainEventsInterceptor(IServiceContext serviceContext)
    {
        _serviceContext = serviceContext;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AuditBeforeSaveChanges(eventData.Context);
        return result;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AuditBeforeSaveChanges(eventData.Context);
        return await Task.FromResult(result);
    }

    private void AuditBeforeSaveChanges(DbContext context)
    {
        var emailUserLogged = _serviceContext.UserInfo.Mail;
        var tenantId = _serviceContext.TenantId;

        TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasiliaTimeZone);

        foreach (var entry in context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    SetPropertyIfExists(entry, "DeletedBy", emailUserLogged);
                    break;

                case EntityState.Modified:
                    SetPropertyIfExists(entry, "ChangedBy", emailUserLogged);
                    SetPropertyIfExists(entry, "ChangedAt", now);
                    break;

                case EntityState.Added:
                    SetPropertyIfExists(entry, "TenantId", tenantId);
                    SetPropertyIfExists(entry, "CreatedDate", now);
                    SetPropertyIfExists(entry, "CreatedBy", emailUserLogged);
                    SetPropertyIfExists(entry, "ChangedAt", now);
                    break;
            }
        }
    }

    private void SetPropertyIfExists(EntityEntry entry, string propertyName, object value)
    {
        if (entry.Metadata.FindProperty(propertyName) != null)
        {
            entry.Property(propertyName).CurrentValue = value;
        }
    }
}
