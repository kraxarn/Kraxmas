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
		private Desktop desktop;
		private MainPanel mainPanel;

		public LevelEditor()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1280,
				PreferredBackBufferHeight = 720
			};
			Window.AllowUserResizing = true;
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

			mainPanel = new MainPanel();

			desktop = new Desktop
			{
				HasExternalTextInput = true,
				Root = mainPanel
			};
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			GraphicsDevice.Clear(Color.Black);

			desktop.Render();
		}
	}
}