using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameBase
{
    public class ZDefGame : Microsoft.Xna.Framework.Game
    {
        // =========== Content / Utilities =============
        // =============================================
        public static SpriteBatch spriteBatch;

        public static Texture2D terrainTexture;
        // =============================================

        GraphicsDeviceManager graphics;

        Camera2D Camera;
        InputHandler Input;

        int ScreenWidth = 800;
        int ScreenHeight = 600;
        Vector2 MapSize;

        public ZDefGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;

            graphics.PreferMultiSampling = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            ScreenWidth = 800;
            ScreenHeight = 800;

            Camera = new Camera2D(new Vector2(0, 0), ScreenWidth, ScreenHeight);
            Input = new InputHandler();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateFirstValues();

            Camera.Input(Input);

            base.Update(gameTime);
            Input.UpdateLastValues();
            Camera.Constraint(MapSize);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
