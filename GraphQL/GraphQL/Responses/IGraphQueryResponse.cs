using System;
namespace GraphQL.Responses
{
    public interface IGraphQueryResponse<T>
    {
        T data { get; set; }
    }
}
