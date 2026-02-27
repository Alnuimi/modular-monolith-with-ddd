namespace Blocks.Domain.Entities;

public interface IEntity
{
    int Id { get; }
}

public interface IEntity<TPrimartKey>
    where TPrimartKey : struct
{
    TPrimartKey Id { get; }
}

public abstract class Entity : IEntity
{
    public virtual int Id { get; init; }
}

public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
where TPrimaryKey : struct
{
    public virtual TPrimaryKey Id { get; init; }
}