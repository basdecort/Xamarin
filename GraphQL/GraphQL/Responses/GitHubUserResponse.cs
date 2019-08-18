using System;
using GraphQL.Models;

namespace GraphQL.Responses
{
    public class UserData<T>
    {
        public T user { get; set; }
    }

    public class QueryResponse<T> : IGraphQueryResponse<T>
    {
        public T data { get; set; }
    }
}
