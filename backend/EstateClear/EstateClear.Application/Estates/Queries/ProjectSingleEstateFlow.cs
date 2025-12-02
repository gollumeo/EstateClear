using EstateClear.Application.Estates.DTOs;
using EstateClear.Application.Estates.Ports;

namespace EstateClear.Application.Estates.Queries;

public sealed class ProjectSingleEstateFlow(IEstates estates)
{
    public async Task<SingleEstateProjection?> Execute(ProjectSingleEstate input)
    {
        var estate = await estates.Load(input.EstateId);

        if (estate is null)
        {
            return null;
        }

        return new SingleEstateProjection(
            estate.Id.Value(),
            estate.ExecutorId.Value(),
            estate.DisplayName().Value(),
            estate.Status.ToString());
    }
}
