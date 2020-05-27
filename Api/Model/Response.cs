using EFLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Model
{
    class Response
    {
        public int TotalGames { get; set; }
        public List<MatchResponse> Matches { get; set; }
        public Dictionary<string, ChampionResponse> Data { get; set; }

        public List<ParticipantIdentity> participantIdentities { get; set; }
        public List<Teams> Teams { get; set; }
        public List<Participants> Participants { get; set; }
    }
}
