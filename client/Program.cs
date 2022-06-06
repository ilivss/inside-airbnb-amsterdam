using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using client;
using client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIBaseAddress")) });

// Services
builder.Services.AddSingleton<IListingService, ListingService>();
builder.Services.AddSingleton<INeighbourhoodService, NeighbourhoodService>();

await builder.Build().RunAsync();
