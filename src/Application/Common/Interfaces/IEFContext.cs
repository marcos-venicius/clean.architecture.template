using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IEfContext
{
    public DbSet<Todo> Todos { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}