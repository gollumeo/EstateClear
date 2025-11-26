namespace EstateClear.Domain.Estates;

public sealed class EstateId
{
    private EstateId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EstateId From(Guid value) => new(value);
}
