namespace EstateClear.Domain.Estates;

public sealed class ExecutorId
{
    private ExecutorId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ExecutorId From(Guid value) => new(value);
}
