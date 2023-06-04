using Microsoft.EntityFrameworkCore;

namespace Application.Common.Models;

public sealed class PaginatedList<T> where T : notnull
{
    public IReadOnlyCollection<T> Items { get; }
    public uint PageNumber { get; }
    public uint TotalPages { get; }
    public uint TotalCount { get; }

    public PaginatedList(IReadOnlyCollection<T> items, uint count, uint pageNumber, uint pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (uint)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, uint pageNumber, uint pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToListAsync();

        return new PaginatedList<T>(items, (uint)count, pageNumber, pageSize);
    }

    public static PaginatedList<T> CreateAsync(List<T> source, uint pageNumber, uint pageSize)
    {
        var count = source.Count;
        var items = source.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize).ToList();

        return new PaginatedList<T>(items, (uint)count, pageNumber, pageSize);
    }
}