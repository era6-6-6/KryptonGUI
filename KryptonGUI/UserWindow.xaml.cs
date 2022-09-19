using Krypton_Core;
using KryptonGUI.Render;
using System.Diagnostics;

namespace KryptonGUI;

public partial class UserWindow : ContentPage
{
    Api Api { get; init; }
	public UserWindow(Api api)
	{
        Api = api;
        InitializeComponent();
		Minimap.Background = Color.FromRgb(0, 0, 0);
		Task.Run(() => Api.StartSession());
		Task.Run(async () => await RenderThread());


    }
    private async Task RenderThread()
	{
		while (true)
		{
			try
			{
				await Task.Delay(10);
				this.Dispatcher.Dispatch(() =>
					{
						RenderMap rd = new RenderMap(Api , Minimap.Width , Minimap.Height , Api._user.userData.MapID , Api.RunTime);
						Minimap.Drawable = rd;
					});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
               
            }
        }
	}
}