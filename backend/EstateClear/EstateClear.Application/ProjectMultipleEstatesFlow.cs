using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateClear.Application.DTOs;

namespace EstateClear.Application;

public sealed class ProjectMultipleEstatesFlow(IEstates estates)
{
    public async Task<IReadOnlyList<EstateSummaryProjection>> Execute(ProjectMultipleEstates input)
    {
        var estatesByExecutor = await estates.ByExecutor(input.ExecutorId);

        var projections = estatesByExecutor
            .Select(estate => new EstateSummaryProjection(
                estate.Id.Value(),
                estate.DisplayName().Value(),
                estate.Status.ToString()))
            .ToList()
            .AsReadOnly();

        return projections;
    }
}
