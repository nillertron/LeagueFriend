using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFLibrary.Models
{
    public class TimeLine : ITimeLine
    {
        public int Id { get; set; }

        public string Lane { get; set; }
        [ForeignKey("DeltaId")]
        public List<Delta> CsDiffPerMin { get; set; } = new List<Delta>();

        public List<Delta> CsPrMin { get; set; } = new List<Delta>();

        public List<Delta> XpDiffPerMin { get; set; } = new List<Delta>();
        public List<Delta> XpPrMin { get; set; } = new List<Delta>();







    }
}
