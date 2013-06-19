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
using GameBase.Terrain;
using GameBase.Entity;

namespace GameBase
{
    public class ZDefGame : Microsoft.Xna.Framework.Game
    {
        // =========== Content / Utilities =============
        // =============================================
        public static SpriteBatch spriteBatch;
        public static GraphicsDeviceManager graphics;
        public static Camera2D camera;
        public static InputHandler input;
        public static Lighting lighting;
        public static SelectionHandle Selection;
        public static Pathfinder pathFinder;

        public static TileMap tileMap;

        public static Texture2D terrainTexture;
        public static Texture2D humanBuildingTexture;

        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        public List<Human> HumanList;
        public static  Texture2D HumanTexture;
        // =============================================

        private RenderTarget2D tileRenderTarget;

        public ZDefGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;

            graphics.PreferMultiSampling = false;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            ScreenWidth = 800;
            ScreenHeight = 800;

            camera = new Camera2D(new Vector2(0, 0), ScreenWidth, ScreenHeight);
            input = new InputHandler();
            tileMap = new TileMap(50, 50);
            lighting = new Lighting(1.0f);
            Selection = new SelectionHandle(input);
            HumanList = new List<Human>();
            pathFinder = new Pathfinder(tileMap);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            terrainTexture = Content.Load<Texture2D>("terrain_spritesheet");
            HumanTexture = Content.Load<Texture2D>("Human");
            humanBuildingTexture = Content.Load<Texture2D>("human_buildings");

            tileRenderTarget = new RenderTarget2D(GraphicsDevice, tileMap.GetWidth() * 64, tileMap.GetHeight() * 64);

            HumanList.Add(new Human(HumanTexture, tileMap.GetEntityTile(21,20)));
        }

        protected override void Update(GameTime gameTime)
        {
            input.UpdateFirstValues();

            if (input.KeyDown(Keys.Escape))
            {
                this.Exit();
               // Environment.Exit(0);
            }

            //if (input.KeyDown(Keys.F1))
            //{
            //    throw new Exception("Lol meet");
            //    // Environment.Exit(0);
            //}

            camera.Input(input);
            tileMap.Update();
            lighting.Update();
            Selection.Update(HumanList, camera, tileMap);

            base.Update(gameTime);
            input.UpdateLastValues();
            camera.Constraint(new Vector2(tileMap.GetWidth() * 64, tileMap.GetHeight() * 64));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(tileRenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            tileMap.Draw();

            for (int i = 0; i < HumanList.Count; i++)
            {
                HumanList[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform());
            spriteBatch.Draw(tileRenderTarget, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
