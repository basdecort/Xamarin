using System;
using GraphOfThrones.Core.Models;
using GraphQL.Types;

namespace GraphOfThrones.Core.Schema.Types
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(b => b.isbn);
            Field(b => b.name);
        }
    }
}
