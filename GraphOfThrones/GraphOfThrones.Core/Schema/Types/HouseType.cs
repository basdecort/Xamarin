using System;
using GraphOfThrones.Core.Models;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Types
{
    public class HouseType : ObjectGraphType<House>
    {
        public HouseType()
        {
            Field(h => h.name);
            Field(h => h.currentLord);
        }
    }
}
