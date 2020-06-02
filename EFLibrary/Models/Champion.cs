using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFLibrary.Models
{
    public class Champion : IChampion
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [NotMapped]
        public string Image { get => "http://ddragon.leagueoflegends.com/cdn/10.11.1/img/champion/" + Name + ".png"; }


    }
}
