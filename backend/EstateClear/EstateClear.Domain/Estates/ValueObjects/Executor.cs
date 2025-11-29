namespace EstateClear.Domain.Estates.ValueObjects;

public sealed class Executor : IEquatable<Executor>
{
    private readonly Guid _value;

    private Executor(Guid value)
    {
        _value = value;
    }

    public static Executor From(Guid value) => new(value);

    public Guid Value() => _value;

    public bool Equals(Executor? other)
    {
        if (other is null)
        {
            return false;
        }

        return _value.Equals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Executor);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}
