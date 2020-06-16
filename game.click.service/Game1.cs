using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game.click.service
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SceneObject shuttle;
        private SceneObject sh2;
        private Container stage;
        private Container scene;

        public Game1()
        {
            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            shuttle = new SceneObject(Content.Load<Texture2D>("shuttle"), new Vector2(100,200), SpriteEffects.FlipHorizontally, 1, (float) -3.14/2,new Vector2(0,0),"shuttle");
            sh2 = new SceneObject(Content.Load<Texture2D>("shuttle"), new Vector2(100, 200), SpriteEffects.FlipVertically,1,0f,new Vector2(0,0), "sh2");
            stage = new Container(new Vector2(0, 0), new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height), true, GraphicsDevice, "stage");
            scene = new Container(new Vector2(65, 180), new Vector2(GraphicsDevice.Viewport.Width/4, GraphicsDevice.Viewport.Height/2), true, GraphicsDevice, "scene");
            
            stage.AddClickableChild(shuttle);
            stage.AddDrawableChild(shuttle);

            scene.AddClickableChild(sh2);
            scene.AddDrawableChild(sh2);
            stage.AddDrawableChild(scene);
            stage.AddClickableChild(scene);
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (stage.hasCursor(mouseState)) stage.onClick(mouseState);
            }
            //shuttle.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            stage.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
