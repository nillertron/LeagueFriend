using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFLibrary.Models
{
    public class Match : IMatch
    {
        [Key]
        public long GameId { get; set; }
        public string Role { get; set; }
        public int Season { get; set; }
        public string PlatformId { get; set; }
        [ForeignKey("ChampionId")]
        public Champion Champion { get; set; }
        public int ChampionId { get; set; }
        public int Queue { get; set; }
        public string Lane { get; set; }
        public long TimeStamp { get; set; }
        
        public List<Team> Teams { get; set; } = new List<Team>();
        [ForeignKey("ParticipantParticipantId")]
        public List<Participant> Participants { get; set; } = new List<Participant>();
        

    }
}
