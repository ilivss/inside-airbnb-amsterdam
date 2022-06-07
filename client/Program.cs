using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using client;
using client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Inject HttpClient into components
builder.Services.AddHttpClient<IListingService, ListingService>(client => { client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIBaseAddress")); });
builder.Services.AddHttpClient<INeighbourhoodService, NeighbourhoodService>(client => { client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIBaseAddress")); });
builder.Services.AddHttpClient<IStatisticsService, StatisticsService>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIBaseAddress")))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                                               .ConfigureHandler(authorizedUrls: new[] { builder.Configuration.GetValue<string>("APIBaseAddress") })
                                      );

// Services
builder.Services.AddScoped<IMapboxService, MapboxService>();

// Auth0
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration.GetValue<string>("Auth0:Audience"));
});

await builder.Build().RunAsync();
