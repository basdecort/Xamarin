using System;
using System.Collections.Generic;

namespace GraphOfThrones.Core.Models
{
    public class Episode
    {
        public int seasonNum { get; set; }
        public int episodeNum { get; set; }
        public string episodeTitle { get; set; }
        public string episodeLink { get; set; }
        public string episodeAirDate { get; set; }
        public string episodeDescription { get; set; }
        public List<string> openingSequenceLocations { get; set; }
        public List<Scene> scenes { get; set; }
    }
}
