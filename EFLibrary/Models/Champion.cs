using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFLibrary.Models
{
    public class Champion : IChampion
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
