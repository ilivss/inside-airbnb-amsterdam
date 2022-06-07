using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using client;
using client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIBaseAddress")) });

builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIBaseAddress")))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                .ConfigureHandler(
                    authorizedUrls: new[] { builder.Configuration.GetValue<string>("APIBaseAddress") }
                ));

builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("ServerAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration.GetValue<string>("Auth0:Audience"));
});

// Services
builder.Services.AddSingleton<IListingService, ListingService>();
builder.Services.AddSingleton<INeighbourhoodService, NeighbourhoodService>();
builder.Services.AddSingleton<IStatisticsService, StatisticsService>();
builder.Services.AddSingleton<IMapboxService, MapboxService>();

await builder.Build().RunAsync();
