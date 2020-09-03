using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
		public Music Music { get; private set; }

		public Color BackgroundColor = new Color(0x2e, 0xcc, 0x71);

		private Texture2D LineTexture;

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
			Music = new Music(this);
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
			
			// Primitives
			LineTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

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
			
			spriteBatch.Begin();
			spriteBatch.Draw(LineTexture, new Rectangle(64, 64, 64, 1), Color.White);
			spriteBatch.End();

			Desktop.Render();
		}
	}
}