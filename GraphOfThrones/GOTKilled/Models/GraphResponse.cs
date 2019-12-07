using System;
using System.Collections.Generic;
using Shared.Core.Models;

namespace GOTKilled.Models
{
    public class GraphResponse
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public List<Character> characters { get; set; }
        public List<Episode> episodes { get; set; }
    }

}

