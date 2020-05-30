using EFLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Model
{
    class Teams
    {
        public int DragonKills { get; set; }
        public int BaronKills { get; set; }
        public int TowerKills { get; set; }
        public int RiftHeraldKills { get; set; }
        public int TeamId { get; set; }
        public string Win { get; set; }
    }
}
