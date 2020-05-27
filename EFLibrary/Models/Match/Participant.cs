using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFLibrary.Models
{
    public class Participant : IParticipant
    {
        [Key]
        public int ParticipantId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public string PlayerId { get; set; }

        [ForeignKey("TeamId")]

        public Team Team { get; set; }
        [ForeignKey("ChampionId")]
        public Champion Champion { get; set; }
        public int ChampionId { get; set; }

        [ForeignKey("MatchGameId")]

        public Match Match { get; set; }
        public long MatchGameId { get; set; }
    }
}
