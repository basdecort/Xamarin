using System;
using System.Collections.Generic;

namespace GraphOfThrones.Core.Models
{
    public class Character
    {
        public string url { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string culture { get; set; }
        public string born { get; set; }
        public string died { get; set; }
        public List<string> titles { get; set; }
        public List<string> aliases { get; set; }
        public string father { get; set; }
        public string mother { get; set; }
        public string spouse { get; set; }
        public List<object> allegiances { get; set; }
        public List<string> books { get; set; }
        public List<object> povBooks { get; set; }
        public List<string> tvSeries { get; set; }
        public List<string> playedBy { get; set; }
    }
}
