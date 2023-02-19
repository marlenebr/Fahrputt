using Fahrputt.Logic;

namespace Fahrputt;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    //protected override Window CreateWindow(IActivationState activationState)
    //{
    //    Window window =  base.CreateWindow(activationState);

    //    window.Destroying += (sender, e) =>
    //    {
    //        FahrputtAppManager.GetInstance.OnApplicationStopped();

    //    };

    //    return window;
    //}
}
