using Krypton_Core.Collections.Game.Maps;

namespace KryptonGUI.Components;

public partial class General : Grid
{
	public General()
	{
		InitializeComponent();
        foreach(var map in MapCollection.Maps)
		{
			MapCombo.Items.Add(map.Value);
		}
	}
}