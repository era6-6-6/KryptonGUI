namespace KryptonGUI;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}

	private void LogIn_Clicked(object sender, EventArgs e)
	{
		AppShell.instance.AddPage(new Krypton_Core.Api(Username.Text , Password.Text),new MainPage(){Title = "test"});
	}
}

