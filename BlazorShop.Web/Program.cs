using Blazored.LocalStorage;
using BlazorShop.Web;
using BlazorShop.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = "https://localhost:7025";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });


builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IcarrinhoCompraService, CarrinhoCompraService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IGerenciaCarrinhoItensLocalStorageService, GerenciaCarrinhoItemLocalStorageService>();
builder.Services.AddScoped<IGerenciaProdutosLocalStorageService, GerenciaProdutosLocalStorageService>();

await builder.Build().RunAsync();

