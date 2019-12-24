using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleOrNotPeopleDemo.Models
{
    public class Result
    {
        public bool IsPeople { get; set; }
        public float Confidence { get; set; }
        public byte[] PhotoBytes { get; set; }

    }
}
