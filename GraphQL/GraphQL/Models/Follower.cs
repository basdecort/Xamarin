using System;
using System.Collections.Generic;

namespace GraphQL.Models
{
    public class Followers
    {
        public List<Follower> nodes { get; set; }
    }

    public class Follower
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
