using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infra.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence;

public sealed class EFContext : DbContext, IEFContext
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _cofiguration;

    public EFContext(DbContextOptions<EFContext> options, IConfiguration configuration, IMediator mediator)
         : base(options)
    {
        _cofiguration = configuration;
        _mediator = mediator;
    }

    public DbSet<Todo> Todos { get; set; } = default!;

    public void AutoUpdateFields()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: BaseEntity, State: EntityState.Added });

        foreach (var entity in entries)
        {
            ((BaseEntity)entity.Entity).Id = Guid.NewGuid();
            ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
        }

        var entriesToEdit = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: BaseEntity, State: EntityState.Modified or EntityState.Added });

        foreach (var entity in entriesToEdit)
        {
            ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AutoUpdateFields();

        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
