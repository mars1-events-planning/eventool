namespace Eventool.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    
    public bool IsTransient() => Id == default;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity entity)
            return false;

        if (ReferenceEquals(this, entity))
            return true;

        if (GetType() != entity.GetType())
            return false;

        if (entity.IsTransient() || IsTransient())
            return false;

        return entity.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity left, Entity? right) => 
        left?.Equals(right) ?? Equals(right, null);

    public static bool operator !=(Entity left, Entity right) => !(left == right);
}