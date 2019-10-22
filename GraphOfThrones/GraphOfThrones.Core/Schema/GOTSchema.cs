using System;
using GraphOfThrones.Core.Schema.Mutations;
using GraphOfThrones.Core.Schema.Queries;
using GraphOfThrones.Core.Schema.Subscriptions;
using GraphQL;

namespace GraphOfThrones.Core.Schema
{
    public class GOTSchema : GraphQL.Types.Schema
    {
        public GOTSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<Query>();
            Mutation = resolver.Resolve<Mutation>();
            Subscription = resolver.Resolve<Subscription>();
        }
    }
}
