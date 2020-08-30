using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;

namespace OpenTD.LevelEditor
{
	public class LevelEditor : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private MainPanel mainPanel;

		public Desktop Desktop { get; private set; }
		public Texture2D MenuIcons { get; private set; }

		public Color BackgroundColor = new Color(0x4c, 0xaf, 0x50);

		public LevelEditor()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1280,
				PreferredBackBufferHeight = 720
			};
			Window.AllowUserResizing = true;
			Window.Title = "OpenTD Level Editor";
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			MyraEnvironment.Game = this;
		}

		protected override void Initialize()
		{
			// TODO: Remove after update to 3.8.1
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Images
			MenuIcons = Content.Load<Texture2D>("Image/MenuIcons");

			// UI
			mainPanel = new MainPanel(this);
			Desktop = new Desktop
			{
				HasExternalTextInput = true,
				Root = mainPanel
			};
		}

		protected override void UnloadContent()
		{
			Content.Unload();
			base.UnloadContent();
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			GraphicsDevice.Clear(BackgroundColor);

			Desktop.Render();
		}
	}
}