using System;
using Newtonsoft.Json;

namespace GraphQL.Models
{
    public class User
    {
        public string name { get; set; }
        public object bio { get; set; }
        public object company { get; set; }
        public object location { get; set; }
        public Followers followers { get; set; }
    }
}
