using Domain.Common;
using Infra.Persistence;
using MediatR;

namespace Infra.Common;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, EfContext context)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var baseEntities = entities as BaseEntity[] ?? entities.ToArray();
        
        var domainEvents = baseEntities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        baseEntities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
