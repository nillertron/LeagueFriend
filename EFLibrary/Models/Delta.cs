using System;
using System.Collections.Generic;
using System.Text;

namespace EFLibrary.Models
{
    public class Delta : IDelta
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public double Value { get; set; }
    }
}
