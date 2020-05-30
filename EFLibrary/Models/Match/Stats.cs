using System;
using System.Collections.Generic;
using System.Text;

namespace EFLibrary.Models
{
    public class Stats : IStats
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int LargestMultiKill { get; set; }
        public int GoldEarned { get; set; }
        public int TotalPlayerScore { get; set; }
        public int ChampLevel { get; set; }
        public int TotalMinionsKilled { get; set; }
        public int Deaths { get; set; }
        public long TotalDamageDealt { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public long VisionScore { get; set; }
        public int Item0 { get; set; }
        public int Item1 { get; set; }

        public int Item2 { get; set; }
        public int Item3 { get; set; }
        public int Item4 { get; set; }
        public int Item5 { get; set; }
        public int Item6 { get; set; }
    }
}
