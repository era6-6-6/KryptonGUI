using KryptonGUI.Render;
using System.Diagnostics;

namespace KryptonGUI;

public partial class UserWindow : ContentPage
{
	public UserWindow()
	{
		InitializeComponent();
		Minimap.Background = Color.FromRgb(0, 0, 0);
		Thread temp = new Thread(RenderThread);
        temp.Start();

    }
	private void RenderThread()
	{
		while (true)
		{
			try
			{
				Thread.Sleep(1000);

				this.Dispatcher.Dispatch(() =>
					{
						RenderMap rd = new RenderMap();
						Minimap.Drawable = rd;
					});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
	}
}