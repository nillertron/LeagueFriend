using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Model
{
    public class MatchResponse
    {
        public long GameId { get; set; }
        public string Role { get; set; }
        public int Season { get; set; }
        public string PlatformId { get; set; }
        public int Champion { get; set; }
        public int Queue { get; set; }
        public string Lane { get; set; }
        public long TimeStamp { get; set; }
    }
}
