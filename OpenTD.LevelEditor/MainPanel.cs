using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.ColorPicker;
using Myra.Graphics2D.UI.File;
using OpenTD.Common;

namespace OpenTD.LevelEditor
{
	public class MainPanel : Panel
	{
		private readonly LevelEditor parent;

		public MainPanel(LevelEditor levelEditor)
		{
			parent = levelEditor;

			var menu = new HorizontalMenu();
			menu.Items.Add(FileMenu);
			menu.Items.Add(EditMenu);
			menu.Items.Add(HelpMenu);

			Widgets.Add(menu);
		}

		private MenuItem FileMenu
		{
			get
			{
				var menu = new MenuItem("menuFile", "File");

				var menuFileNew = new MenuItem("menuFileNew", "New")
				{
					Image = GetIconImage(0, 0)
				};
				menu.Items.Add(menuFileNew);

				var open = new FileDialog(FileDialogMode.OpenFile);

				var menuFileOpen = new MenuItem("menuFileOpen", "Open...")
				{
					Image = GetIconImage(0, 1)
				};
				menuFileOpen.Selected += (sender, args) => open.Show(parent.Desktop);
				menu.Items.Add(menuFileOpen);

				var save = new FileDialog(FileDialogMode.SaveFile)
				{
					Filter = ".tdm"
				};

				var menuFileSave = new MenuItem("menuFileSave", "Save as...")
				{
					Image = GetIconImage(0, 2)
				};
				menuFileSave.Selected += (sender, args) => save.Show(parent.Desktop);
				menu.Items.Add(menuFileSave);

				menu.Items.Add(new MenuSeparator());
				var menuFileQuit = new MenuItem("menuFileQuit", "Quit")
				{
					Image = GetIconImage(0, 3)
				};
				menuFileQuit.Selected += (sender, args) => parent.Exit();
				menu.Items.Add(menuFileQuit);

				return menu;
			}
		}

		private MenuItem EditMenu
		{
			get
			{
				var menu = new MenuItem("menuEdit", "Edit");

				var music = new MenuItem("menuEditMusic", "Music...")
				{
					Image = GetIconImage(1, 0)
				};
				music.Selected += (sender, args) => new MusicSelectDialog(parent).ShowModal(parent.Desktop);
				menu.Items.Add(music);

				menu.Items.Add(new MenuItem("menuEditTileset", "Tileset...")
				{
					Image = GetIconImage(1, 1)
				});

				var colorPicker = new ColorPickerDialog
				{
					Color = parent.BackgroundColor
				};
				colorPicker.Closed += (sender, args) => parent.BackgroundColor = colorPicker.Color;

				var background = new MenuItem("menuEditBackground", "Background...")
				{
					Image = GetIconImage(1, 2)
				};
				background.Selected += (sender, args) => colorPicker.Show(parent.Desktop);

				menu.Items.Add(background);

				//Music.Names.ForEach(n => menuEditMusic.Items.Add(new MenuItem($"menuEditMusic{menuEditMusic.Items.Count}", n)));

				return menu;
			}
		}

		private MenuItem HelpMenu
		{
			get
			{
				var menu = new MenuItem("menuHelp", "Help");

				menu.Items.Add(new MenuItem("menuHelpVersion", $"OpenTD LevelEditor {Version.LevelEditor}")
				{
					Image = GetIconImage(2, 0)
				});
				menu.Items.Add(new MenuItem("menuHelpUpdate", "Check for updates")
				{
					Image = GetIconImage(2, 1)
				});

				return menu;
			}
		}

		private IImage GetIconImage(int row, int column) =>
			new TextureRegion(parent.MenuIcons, new Rectangle(16 * column, 16 * row, 16, 16));
	}
}