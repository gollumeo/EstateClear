using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateClear.Application;

public sealed class ProjectParticipantsOfEstateFlow(IEstates estates)
{
    public async Task<IReadOnlyList<DTOs.EstateParticipantProjection>> Execute(ProjectParticipantsOfEstate input)
    {
        var estate = await estates.Load(input.EstateId);

        if (estate is null)
        {
            return Array.Empty<DTOs.EstateParticipantProjection>();
        }

        var projections = estate
            .Participants()
            .Select(participant => new DTOs.EstateParticipantProjection(
                participant.Email(),
                participant.FirstName() ?? string.Empty,
                participant.LastName() ?? string.Empty))
            .ToList()
            .AsReadOnly();

        return projections;
    }
}
