using Myra.Graphics2D.UI;

namespace Kraxmas.LevelEditor
{
	public class MainPanel : Panel
	{
		private readonly HorizontalMenu menu;
		private readonly MenuItem menuFile;

		public MainPanel()
		{
			menuFile = new MenuItem("menuFile")
			{
				Text = "File"
			};
			
			menu = new HorizontalMenu();
			menu.Items.Add(menuFile);
			
			Widgets.Add(menu);
		}
	}
}