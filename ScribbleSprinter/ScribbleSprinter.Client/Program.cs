using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ScribbleSprinter.Client.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();

var types = typeof(ViewModelBase).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ViewModelBase)));

foreach (var type in types)
{
    builder.Services.AddTransient(type);
}

await builder.Build().RunAsync();
