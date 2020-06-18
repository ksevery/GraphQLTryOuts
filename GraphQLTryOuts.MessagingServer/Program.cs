using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;

namespace GraphQLTryOuts.MessagingServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseKestrel(o =>
                    {
                        o.ListenAnyIP(1883, l => l.UseMqtt()); // MQTT pipeline
                        o.ListenAnyIP(5000); // Default HTTP pipeline
                    })
                    .UseStartup<Startup>()
                    .Build();
    }
}
