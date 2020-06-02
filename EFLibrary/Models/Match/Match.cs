using EFLibrary.DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EFLibrary.Models
{
    public class Match : IMatch
    {
        [Key]
        public long GameId { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public bool Win { get; set; }
        [NotMapped]
        public string WinString { get { if (Win) return "Win"; else return "Lose"; } }
        public int Season { get; set; }
        public string PlatformId { get; set; }
        private Champion _Champion;
        [ForeignKey("ChampionId")]
        public Champion Champion { get { return new DbCon().Champion.Where(x => x.Id == ChampionId).FirstOrDefault(); } }
        public int ChampionId { get; set; }
       
        public int Queue { get; set; }
        public string Lane { get; set; }
        public long TimeStamp { get; set; }
        
        public List<Team> Teams { get; set; } = new List<Team>();
        [ForeignKey("ParticipantParticipantId")]
        public List<Participant> Participants { get; set; } = new List<Participant>();
        [NotMapped]
        public int Kills { get; set; } 
        [NotMapped]
        public int Deaths { get; set; } 
        [NotMapped]
        public int Assist { get; set; }
        [NotMapped]
        public string ItemImage1 { get; set; }
        [NotMapped]
        public string ItemImage2 { get; set; }
        [NotMapped]
        public string ItemImage3 { get; set; }
        [NotMapped]
        public string ItemImage4 { get; set; }
        [NotMapped]
        public string ItemImage5{ get; set; }
        [NotMapped]
        public string ItemImage6 { get; set; }
        [NotMapped]
        public string ItemImage0 { get; set; }
        [NotMapped]
        public int GoldEarned { get; set; }
        [NotMapped]
        public int TotalMinionsKilled { get; set; }




    }
}
