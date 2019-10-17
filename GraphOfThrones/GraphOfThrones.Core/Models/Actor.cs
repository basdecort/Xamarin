using System;
using System.Collections.Generic;

namespace GraphOfThrones.Core.Models
{
    public class Actor
    {
        public string actorName { get; set; }
        public string actorLink { get; set; }
        public List<int> seasonsActive { get; set; }
    }
}
