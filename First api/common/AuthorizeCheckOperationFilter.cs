using First_api.common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace First_api.common
{

    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if the endpoint method or its controller has the [AllowAnonymous] attribute
            var hasAnonymous = context.ApiDescription.CustomAttributes().Any(attr => attr is AllowAnonymousAttribute);

            if (hasAnonymous)
            {
                // If it is a public endpoint, don't show the padlock icon next to it
                return;
            }

            // Initialize security field if empty
            operation.Security ??= new List<OpenApiSecurityRequirement>();

            // Apply the padlock only to endpoints that actually require authorization
            var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        };

            operation.Security.Add(securityRequirement);
        }
    }

}
