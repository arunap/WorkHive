using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkHive.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void UseSwaggerWithUI(this IApplicationBuilder app) =>

        app
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkHive API v1");
                c.RoutePrefix = string.Empty;
            });
    }
}