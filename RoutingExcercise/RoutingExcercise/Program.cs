//changes

namespace RoutingExcercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseRouting();

            Dictionary<int, string> countries = new Dictionary<int, string>()
            {
                {1, "United States"},
                {2, "Canada"},
                {3, "United Kingdom"},
                {4, "India"},
                {5, "Japan"}
            };
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/countries", async context =>
                {
                    foreach (KeyValuePair<int, string> country in countries)
                    {
                        await context.Response.WriteAsync($"{country.Key}. {country.Value} \n");
                    }
                });

                endpoints.MapGet("/countries/{id:int:range(1,100)}", async context =>
                {
                    int countryId = Convert.ToInt32(context.Request.RouteValues["id"]);
                    if (countries.ContainsKey(countryId))
                    {
                        await context.Response.WriteAsync($"{countries[countryId]}");
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync("No country");
                    } 
                });

                endpoints.MapGet("/countries/{id:min(101)}", async context =>
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Id should be between 1 and 100");
                });

            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Default Response");
            });
            app.Run();
        }
    }
}
