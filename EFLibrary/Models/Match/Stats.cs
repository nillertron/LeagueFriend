using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public string Item0Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/"+Item0+".png"; }
        public int Item1 { get; set; }
        [NotMapped]
        public string Item1Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/" + Item1 + ".png"; }

        public int Item2 { get; set; }
        [NotMapped]
        public string Item2Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/" + Item2 + ".png"; }

        public int Item3 { get; set; }
        [NotMapped]
        public string Item3Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/" + Item3 + ".png"; }

        public int Item4 { get; set; }
        [NotMapped]
        public string Item4Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/" + Item4 + ".png"; }

        public int Item5 { get; set; }
        [NotMapped]
        public string Item5Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/" + Item5 + ".png"; }

        public int Item6 { get; set; }
        [NotMapped]
        public string Item6Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/item/" + Item6 + ".png"; }

    }
}
