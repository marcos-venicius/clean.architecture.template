using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Application.Tests;

public static class MockDbSetExtension
{
    public static Mock<DbSet<T>> GenerateMockDbSet<T>(this IEnumerable<T> entities) where T : class
    {
        var data = entities.AsQueryable().BuildMockDbSet();

        return data;
    }
}