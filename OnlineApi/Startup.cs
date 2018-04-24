// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // задаём политику CORS, чтобы наше клиентское приложение могло отправить запрос на сервер API
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:5593",
                        "http://localhost:5592",
                        "http://localhost:5594")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvcCore()
                .AddAuthorization(options =>
                // политики позволяют не работать с Roles magic strings, содержащими перечисления ролей через запятую
                options.AddPolicy("AdminsOnly", policyUser =>
                {
                    policyUser.RequireClaim("role", "admin");
                })
                )
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5590";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "api1";
                    options.RoleClaimType = "role";
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            /*
            app.UseCors(policy =>
            {
                policy.WithOrigins(
                    "http://localhost:5593",
                    "http://localhost:5592");

                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });
            */
            app.UseCors("default");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}