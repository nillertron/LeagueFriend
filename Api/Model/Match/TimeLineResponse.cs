using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Model.Match
{
    class TimeLineResponse
    {
        public Dictionary<string, double> CsDiffPerMinDeltas { get; set; }
        public Dictionary<string, double> CreepsPerMinDeltas { get; set; }
        public Dictionary<string, double> XpPerMinDeltas { get; set; }
        public Dictionary<string, double> XpDiffPerMinDeltas { get; set; }
        public string Lane { get; set; }
    }
}
