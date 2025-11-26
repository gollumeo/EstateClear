using Rituals.Contracts;
using Rituals.Runeforge;

namespace EstateClear.Api;

public class Runeforge : DormantRuneforge
{
    protected override IEnumerable<IRune> Frostmourne()
    {
        yield break;
    }
}