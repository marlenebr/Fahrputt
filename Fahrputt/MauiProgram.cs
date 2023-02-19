using CommunityToolkit.Maui.Markup;
using Fahrputt.Logic;
using Fahrputt.Services;
using Fahrputt.ViewModels;

namespace Fahrputt;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{

        FahrputtAppManager appManager = FahrputtAppManager.GetInstance;


        Console.WriteLine("-------0");

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        Console.WriteLine("-------1");



        //builder.Services.AddSingleton<StationDataService>();
        //builder.Services.AddSingleton<StationItemsViewModel>();
        builder.Services.AddSingleton<FavoritesPage>();
        builder.Services.AddSingleton<StationsMainPage>();
  

        //StationsMainPage stationMainPage = new StationsMainPage();  



        return builder.Build();
	}
}
