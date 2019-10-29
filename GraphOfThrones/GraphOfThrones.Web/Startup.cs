using GraphOfThrones.Core.Models;
using GraphOfThrones.Core.Schema;
using GraphOfThrones.Core.Schema.Mutations;
using GraphOfThrones.Core.Schema.Subscriptions;
using GraphOfThrones.Core.Schema.Queries;
using GraphOfThrones.Core.Schema.Types;
using GraphOfThrones.Core.Services;
using GraphQL;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphOfThrones
{
    /// <summary>
    /// You can also find an example on: https://github.com/graphql-dotnet/server/blob/f28a93fec4c8fcfe0b6c51c20a3eaace1c2040ca/samples/Samples.Server/Startup.cs
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // If using Kestrel:
            serviceCollection.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // Register DepedencyResolver; this will be used when a GraphQL type needs to resolve a dependency
            serviceCollection.AddSingleton<IDependencyResolver>(c => new FuncDependencyResolver(type => c.GetRequiredService(type)));

            // Query, Mutation and Subscription
            serviceCollection.AddSingleton<Query>();
            serviceCollection.AddSingleton<Mutation>();
            serviceCollection.AddSingleton<Subscription>();

            // Types
            serviceCollection.AddSingleton<CharacterType>();
            serviceCollection.AddSingleton<EpisodeType>();
            serviceCollection.AddSingleton<AddEpisodeRequest>();
            serviceCollection.AddSingleton<KillCharacterRequest>();
            serviceCollection.AddSingleton<CharacterKilledType>();

            // Schema
            serviceCollection.AddSingleton<GOTSchema>();

            // Services
            serviceCollection.AddSingleton<ICharacterService, CharacterService>();
            serviceCollection.AddSingleton<IEpisodeService, EpisodeService>();

            // Register GraphQL services
            serviceCollection.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            }).AddWebSockets();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Allow to display UI
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // This will enable WebSockets in Asp.Net core
            app.UseWebSockets();

            // Enable endpoint for websockets (subscriptions)
            app.UseGraphQLWebSockets<GOTSchema>("/graphql");
            // Enable endpoint for querying
            app.UseGraphQL<GOTSchema>("/graphql");
        }
    }
}
