using Api.Model.Match;
using EFLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Model
{
    class Participants
    {
        public int ParticipantId { get; set; }
        public int ChampionId { get; set; }
        public int TeamId { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public Stats Stats { get; set; }
        public TimeLineResponse TimeLine { get; set; }
    }
}
