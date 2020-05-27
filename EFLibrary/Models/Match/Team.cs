using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFLibrary.Models
{
    public class Team : ITeam
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Win { get; set; }
    }
}
