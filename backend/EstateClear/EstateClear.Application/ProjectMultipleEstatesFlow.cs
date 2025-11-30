using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateClear.Application;

public sealed class ProjectMultipleEstatesFlow(IEstates estates)
{
    public async Task<IReadOnlyList<DTOs.EstateSummaryProjection>> Execute(ProjectMultipleEstates input)
    {
        var estatesByExecutor = await estates.ByExecutor(input.ExecutorId);

        var projections = estatesByExecutor
            .Select(estate => new DTOs.EstateSummaryProjection(
                estate.Id.Value(),
                estate.DisplayName().Value(),
                estate.Status.ToString()))
            .ToList()
            .AsReadOnly();

        return projections;
    }
}
