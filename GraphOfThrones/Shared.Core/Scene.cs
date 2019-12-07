using System;
using System.Collections.Generic;

namespace Shared.Core.Models
{
    public class Scene
    {
        public string sceneStart { get; set; }
        public string sceneEnd { get; set; }
        public string location { get; set; }
        public string subLocation { get; set; }
        public List<Character> characters { get; set; }
        public bool? greensight { get; set; }
        public string altLocation { get; set; }
        public bool? warg { get; set; }
        public bool? flashback { get; set; }
    }
}
