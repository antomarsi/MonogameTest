using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicPlatform.Desktop
{
    public class Game1 : Game
    {
        //Display
        const int SCREEN_WIDTH = 320;
        const int SCREEN_HEIGHT = 180;
        const bool FULLSCREEN = false;

        GraphicsDeviceManager graphics;
        PresentationParameters pp;
        SpriteBatch spriteBatch;

        static public int screenW, screenH;
        static public Vector2 screen_center;
        Rectangle screenRect, desktopRect;
        float speed = 50;

        // Textures
        Texture2D far_background, mid_background;
        Texture2D tiles_images;

        //POSITIONS
        static public Vector2 background_pos;

        //RenderTargets
        RenderTarget2D MainTarget;

        // INPUT
        Input input;

        public Game1()
        {
            int initial_screen_width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 10;
            int initial_screen_height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 10;
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = initial_screen_width,
                PreferredBackBufferHeight = initial_screen_height,
                IsFullScreen = FULLSCREEN,
                PreferredDepthStencilFormat = DepthFormat.Depth16,
            };
            Window.IsBorderless = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            MainTarget = new RenderTarget2D(GraphicsDevice, SCREEN_WIDTH, SCREEN_HEIGHT);
            pp = GraphicsDevice.PresentationParameters;
            SurfaceFormat format = pp.BackBufferFormat;
            screenW = MainTarget.Width;
            screenH = MainTarget.Height;
            desktopRect = new Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight);
            screenRect = new Rectangle(0, 0, screenW, screenH);
            screen_center = new Vector2(screenW / 2f, screenH / 2f) - new Vector2(32f, 32f);

            input = new Input();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            far_background = Content.Load<Texture2D>("background_stars");
            mid_background = Content.Load<Texture2D>("mid_background");
            tiles_images = Content.Load<Texture2D>("tileset");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            input.Update();
            if (input.Keypress(Keys.Escape)) Exit();
            if (input.Keydown(Keys.Left)) background_pos.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (input.Keydown(Keys.Right)) background_pos.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (input.Keydown(Keys.Up)) background_pos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (input.Keydown(Keys.Down)) background_pos.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(MainTarget);

            // DRAW STUFF

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointWrap);
            spriteBatch.Draw(far_background, screenRect, new Rectangle((int)(-background_pos.X * 0.5f), 0, far_background.Width, far_background.Height), Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointWrap);
            spriteBatch.Draw(mid_background, screenRect, new Rectangle((int)(-background_pos.X), (int)(-background_pos.Y), far_background.Width, far_background.Height), Color.White);
            spriteBatch.End();

            // DRAW MAINTARGET TO BACKBUFFER
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(MainTarget, desktopRect, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
