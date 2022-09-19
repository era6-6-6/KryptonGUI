

using KryptonGUI.Components;
using System.Diagnostics;

namespace KryptonGUI;

public partial class SettingsPage : ContentPage
{ 
	public SettingsPage()
	{
		InitializeComponent();
		
        
    }

	private void ContentPage_SizeChanged(object sender, EventArgs e)
	{
        SettngsPicker.WidthRequest = this.Width;
        SettngsPicker.VerticalTextAlignment = TextAlignment.Center;
        SettngsPicker.HorizontalTextAlignment = TextAlignment.Center;
    }

    private void SettngsPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Debug.WriteLine(SettngsPicker.SelectedItem.ToString());
        switch(SettngsPicker.SelectedItem.ToString())
        {
            case "General":
                {
                    ComponentsGrid.Children.Add(new General());
                    break;
                }
        }
    }
}