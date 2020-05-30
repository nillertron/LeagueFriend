using System.Collections.Generic;

namespace EFLibrary.Models
{
    public interface ITimeLine
    {
        List<Delta> CsDiffPerMin { get; set; }
        List<Delta> CsPrMin { get; set; }
        int Id { get; set; }
        string Lane { get; set; }
        List<Delta> XpDiffPerMin { get; set; }
        List<Delta> XpPrMin { get; set; }
    }
}