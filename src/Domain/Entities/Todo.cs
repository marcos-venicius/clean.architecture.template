using Domain.Common;

namespace Domain.Entities;

public sealed class Todo : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool Completed { get; set; }
}