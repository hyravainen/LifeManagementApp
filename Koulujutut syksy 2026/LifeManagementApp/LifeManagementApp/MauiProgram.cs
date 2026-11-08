using LifeManagementApp.Data;
using LifeManagementApp.Services;
using LifeManagementApp.ViewModels;
using LifeManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeManagementApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        SQLitePCL.Batteries_V2.Init();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Tietokanta
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "todos.db");
        builder.Services.AddDbContextFactory<AppDbContext>(o => o.UseSqlite($"Data Source={dbPath}"));

        // Palvelut: Singleton, koska niillä ei ole omaa tilaa ja ne voidaan jakaa
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
        builder.Services.AddSingleton<IWeatherService, OpenMeteoWeatherService>();

        // ViewModelit ja sivut: Transient, uusi instanssi joka kerta
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<TodoListViewModel>();
        builder.Services.AddTransient<AddTodoViewModel>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<TodoListPage>();
        builder.Services.AddTransient<AddTodoPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
