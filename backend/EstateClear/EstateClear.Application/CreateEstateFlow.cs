using EstateClear.Domain.Estates;

namespace EstateClear.Application;

public sealed class CreateEstateFlow(IEstates estates)
{
    public async Task<EstateCreated> Execute(CreateEstate input)
    {
        if (string.IsNullOrWhiteSpace(input.DisplayName))
        {
            throw new Exception("Display name is required");
        }

        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(input.ExecutorId);

        await estates.Add(estateId, executorId, input.DisplayName);

        return new EstateCreated(estateId);
    }
}
