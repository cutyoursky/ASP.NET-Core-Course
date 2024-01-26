using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace MiddlewareLoginExcercise.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == "POST" && httpContext.Request.Path == "/")
            {
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string body = await reader.ReadToEndAsync();
                Dictionary<string, StringValues> postDict = QueryHelpers.ParseQuery(body);

                string? email = null;
                string? password = null;

                if (postDict.ContainsKey("email") && postDict.ContainsKey("password"))
                {
                    email = postDict["email"][0];
                    password = postDict["password"][0];
                    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                    {
                        if (email == "admin@example.com" && password == "admin1234")
                        {
                            httpContext.Response.WriteAsync("Succesful login");
                        }
                        else
                        {
                            httpContext.Response.StatusCode = 400;
                            httpContext.Response.WriteAsync("Invalid login");
                        }
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    httpContext.Response.WriteAsync("Invalid login");
                }

            }
            else
            {
                await _next(httpContext);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
