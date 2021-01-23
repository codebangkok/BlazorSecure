using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            builder.Services.AddOidcAuthentication(options => {
                options.ProviderOptions.Authority = "https://localhost:5001";
                options.ProviderOptions.ClientId = "interactive";
                options.ProviderOptions.RedirectUri = "https://localhost:5002/authentication/login-callback";
                options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:5002/authentication/logout-callback";
                options.ProviderOptions.ResponseType = "code";
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
