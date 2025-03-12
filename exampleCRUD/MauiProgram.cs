using exampleCRUD.ViewModels;
using exampleCRUD.Views;
using Microsoft.Extensions.Logging;

namespace exampleCRUD;
using exampleCRUD.DataAccess;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var dbContext = new EmployeeDbContext();
        dbContext.Database.EnsureCreated(); // Creating the db when app loads
        dbContext.Dispose();

        builder.Services.AddDbContext<EmployeeDbContext>();

        builder.Services.AddTransient<EmployeePage>();
        builder.Services.AddTransient<EmployeeViewModel>();
        
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainViewModel>();
        
        Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}