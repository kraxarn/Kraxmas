using System;
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
		public Music Music { get; }

		public Color BackgroundColor = new Color(0x2e, 0xcc, 0x71);

		private Texture2D lineTexture;
		private Color lineColor;
		private Rectangle[] lineDestinations;

		public LevelEditor()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1280,
				PreferredBackBufferHeight = 720
			};
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

		private void LoadLineDestinations()
		{
			const int tileSize = 36;
			var width = graphics.PreferredBackBufferWidth;
			var height = graphics.PreferredBackBufferHeight;

			lineDestinations = new Rectangle[(width / tileSize) * (height / tileSize)];
			var i = 0;

			// Vertical lines
			for (var x = tileSize; x < width; x += tileSize)
				lineDestinations[i++] = new Rectangle(x, 0, 1, height);

			// Horizontal lines
			for (var y = tileSize; y < height; y += tileSize)
				lineDestinations[i++] = new Rectangle(0, y, width, 1);
		}

		protected override void LoadContent()
		{
			base.LoadContent();
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Primitives
			lineTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			lineTexture.SetData(new[]
			{
				Color.White
			});
			lineColor = new Color(0x0, 0x0, 0x0, 0xf);
			LoadLineDestinations();

			Console.WriteLine(Content.RootDirectory);

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

			foreach (var dest in lineDestinations)
			{
				spriteBatch.Draw(lineTexture, dest, lineColor);
			}

			spriteBatch.End();

			Desktop.Render();
		}
	}
}