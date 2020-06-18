using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GraphQLTryOuts.Messaging.Shared;
using GraphQLTryOuts.Users.Data;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GraphQLTryOuts.Users
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(UsersDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("Authority");
                    options.Audience = "GraphQL.Users";
                    options.RequireHttpsMetadata = false;
                });

            services.AddHttpContextAccessor();

            services.AddGraphQL(sp => SchemaBuilder.New()
                    .AddQueryType<Query>()
                    .AddAuthorizeDirectiveType()
                    .AddServices(sp)
                    .Create());

            var messagingSettings = new MessagingSettings();
            Configuration.GetSection("MqttSetup").Bind(messagingSettings);
            services.AddSingleton<MessagingSettings>(messagingSettings);

            services.AddMqttClientProvider(messagingSettings);

            services.AddHostedService<MessagingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseGraphQL();
        }
    }
}
