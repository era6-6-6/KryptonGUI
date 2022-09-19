

namespace KryptonGUI;

public partial class AppShell : Shell
{
    public static AppShell instance;
	public AppShell()
	{
		InitializeComponent();
        instance = this;
	}
    public void AddPage(ContentPage page, string icon = "krypton_logo.png")
    {
        
        AppShell.instance.FlyoutBehavior = FlyoutBehavior.Flyout;
        var item = new FlyoutItem()
        {

            Title = "Account name",
            Route = "botWindow",

            Icon = icon,
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
            Items =
                {
                    new ShellContent
                    {
                        Title = "test",
                        Content = new UserWindow()
                    },
                    new ShellContent
                    {
                        Title = "Settings",
                        Content = new SettingsPage()
                    }
                }




        };
        Shell.Current.Items.Add(item);
        CurrentItem = Items[Items.Count - 1];

        if (Items[0].Title == "Log In")
        {
            Items.RemoveAt(0);
        }
        return;
    }

    public async void AddUser(string username, string icon = "krypton_logo.png")
    {



        var item = new FlyoutItem()
        {

            Title = "test",

            Route = "botWindow",


            FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
            Items =
                {
                   new ShellContent()
                   {

                   },
                   new ShellContent()
                   {

                   }


            }
        };

        Shell.Current.Items.Add(item);
        CurrentItem = item;
        Shell.Current.IsVisible = true;
        return;

    }

}
