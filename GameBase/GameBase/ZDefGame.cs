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
        public static Texture2D HumanTexture;
        public static Texture2D engieTexture;
        public static Texture2D riflemanTexture;
        // =============================================

        // ======= Draw Depths ==============
        public const float TERRAIN_DRAW_DEPTH = 1.0f;
        public const float BUILDING_DRAW_DEPTH = 0.4f;
        public const float HUMAN_DRAW_DEPTH = 0.5f;

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
            engieTexture = Content.Load<Texture2D>("Engineer");
            riflemanTexture = Content.Load<Texture2D>("Rifleman");

            tileRenderTarget = new RenderTarget2D(GraphicsDevice, tileMap.GetWidth() * 64, tileMap.GetHeight() * 64);

            HumanList.Add(new Human(engieTexture, tileMap.GetEntityTile(41,41)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(41, 40)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(42, 40)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(40, 40)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(41, 39)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(42, 39)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(40, 39)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(41, 38)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(42, 38)));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(40, 38)));
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

            for(int i = 0; i < HumanList.Count;i++)
            {
                HumanList[i].Update(pathFinder, input);
            }

            camera.Input(input);
            lighting.Update();
            Selection.Update(HumanList, camera, tileMap, pathFinder);
            tileMap.Update();
            pathFinder.Update();

            base.Update(gameTime);
            input.UpdateLastValues();
            camera.Constraint(new Vector2(tileMap.GetWidth() * 64, tileMap.GetHeight() * 64));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(tileRenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
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
