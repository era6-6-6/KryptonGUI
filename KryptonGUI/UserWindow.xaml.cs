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

	private void Minimap_StartInteraction(object sender, TouchEventArgs e)
	{
		double X = Minimap.Width;
		double Y = Minimap.Height;
		int MapID = Api._user.userData.MapID;

        double convertX;
        double convertY;

        convertX = (double)X / 20500;
        convertY = (double)Y / 13000;

        if (MapID == 29 || MapID == 16 || MapID == 91 || MapID == 93)
        {
            convertX = (double)X / 41000;
            convertY = (double)Y / 26000;
        }



        PointF firstPoint = e.Touches.FirstOrDefault();
		if(Api.logic == null)
		{
			Api.logic = new Krypton_Core.Logic.LogicMethods(Api , Api.Tweener);
		}
		Api.logic.FlyToCorndinates((int)(firstPoint.X / convertX), (int)(firstPoint.Y / convertY));

        


        Debug.WriteLine($"ship sent to X:{(int)(firstPoint.X / convertX)} Y:{(int)(firstPoint.Y / convertY)}");

    }
}