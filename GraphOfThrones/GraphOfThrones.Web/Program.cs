using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphOfThrones.Core.Schema;
using GraphOfThrones.Core.Schema.Types;
using GraphQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQL.Server.Transports.AspNetCore;

using Microsoft.Extensions.Logging;

namespace GraphOfThrones
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
