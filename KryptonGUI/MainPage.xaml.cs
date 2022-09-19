namespace KryptonGUI;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}

	private void LogIn_Clicked(object sender, EventArgs e)
	{
		AppShell.instance.AddPage(new MainPage(){Title = "test"});
	}
}

