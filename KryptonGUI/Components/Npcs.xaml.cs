namespace KryptonGUI.Components;

public partial class Npcs : Grid
{
	public Npcs()
	{
		InitializeComponent();
	}

	private void Grid_SizeChanged(object sender, EventArgs e)
	{
        this.NpcPickerMap.WidthRequest = this.Width;
		this.NpcPickerMap.HorizontalTextAlignment = TextAlignment.Center;
        this.NpcPickerMap.VerticalTextAlignment = TextAlignment.Center;
    }
}