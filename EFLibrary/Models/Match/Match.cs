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
        

    }
}
