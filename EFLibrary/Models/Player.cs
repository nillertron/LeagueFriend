using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFLibrary.Models
{
    public class Player : IPlayer
    {
        [Key]
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string PuuId { get; set; }
        public string Name { get; set; }
        public int ProfileIconId { get; set; }
        public int SummonerLevel { get; set; }
        public bool SaveSearch { get; set; }

    }
}
