using System;
using System.Collections.Generic;

namespace Shared.Core.Models
{
    public class Character
    {
        public string name { get; set; }
        public string characterName { get; set; }
        public string characterLink { get; set; }
        public string actorName { get; set; }
        public string actorLink { get; set; }
        public bool? royal { get; set; }
        public List<string> parents { get; set; }
        public List<string> siblings { get; set; }
        public List<string> killedBy { get; set; }
        public string characterImageThumb { get; set; }
        public string characterImageFull { get; set; }
        public string nickname { get; set; }
        public List<string> killed { get; set; }
        public List<string> servedBy { get; set; }
        public List<string> parentOf { get; set; }
        public List<string> marriedEngaged { get; set; }
        public List<string> serves { get; set; }
        public bool? kingsguard { get; set; }
        public List<string> guardedBy { get; set; }
        public List<Actor> actors { get; set; }
        public List<string> guardianOf { get; set; }
        public List<string> allies { get; set; }
        public List<string> abductedBy { get; set; }
        public List<string> abducted { get; set; }
        public List<string> sibling { get; set; }
    }

}
