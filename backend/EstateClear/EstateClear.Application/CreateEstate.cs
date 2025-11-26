namespace EstateClear.Application;

public sealed class CreateEstate
{
    public CreateEstate(Guid executorId, string displayName)
    {
        ExecutorId = executorId;
        DisplayName = displayName;
    }

    public Guid ExecutorId { get; }

    public string DisplayName { get; }
}
