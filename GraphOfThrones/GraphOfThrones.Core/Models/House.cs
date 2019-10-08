using System;
using System.Collections.Generic;

namespace GraphOfThrones.Core.Models
{
    public class House
    {
        public string url { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string coatOfArms { get; set; }
        public string words { get; set; }
        public List<string> titles { get; set; }
        public List<string> seats { get; set; }
        public string currentLord { get; set; }
        public string heir { get; set; }
        public string overlord { get; set; }
        public string founded { get; set; }
        public string founder { get; set; }
        public string diedOut { get; set; }
        public List<string> ancestralWeapons { get; set; }
        public List<object> cadetBranches { get; set; }
        public List<object> swornMembers { get; set; }
    }
}
