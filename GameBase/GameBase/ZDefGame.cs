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
using GameBase.GUI;

namespace GameBase
{
    public class ZDefGame : Microsoft.Xna.Framework.Game
    {
        // =========== Game / State Stuff ==============
        public static Menu currentMenu;
        public static bool playing;
        // =============================================

        // =========== Content / Utilities =============
        // =============================================
        public static SpriteBatch spriteBatch;
        public static GraphicsDeviceManager graphics;
        public static Camera2D camera;
        public static InputHandler input;
        public static SelectionHandle Selection;
        public static Pathfinder pathFinder;
        public static Pathfinder zombiePathfinder;

        public static TileMap tileMap;
        public static LightMap lightMap;

        public static Texture2D terrainTexture;
        public static Texture2D humanBuildingTexture;

        public static Texture2D mainMenuTexture;

        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        public static Utility utilityClass;
        public static List<Human> HumanList; //How dare you change this to static.
        public static List<Zombie> ZombieList; //IT IS STATIC, STATIC
        public static Texture2D HumanTexture;
        public static Texture2D engieTexture;
        public static Texture2D riflemanTexture;
        public static Texture2D bulletTexture;
        public static Texture2D zombieTexture;
        public static Texture2D lightTexture;

        public static SpriteFont spriteFont;
        // =============================================

        // ======= Draw Depths ==============
        public const float TERRAIN_DRAW_DEPTH = 1.0f;
        public const float BUILDING_DRAW_DEPTH = 0.4f;
        public const float HUMAN_DRAW_DEPTH = 0.5f;

        private RenderTarget2D tileRenderTarget;

        private long LastUpdateFPS;
        private int fps, fpsCount;

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

            camera = new Camera2D(new Vector2(0, 0), ScreenWidth, ScreenHeight);
            input = new InputHandler();
            tileMap = new TileMap(50, 50);
            Selection = new SelectionHandle(input);
            HumanList = new List<Human>();
            ZombieList = new List<Zombie>();
            pathFinder = new Pathfinder(tileMap);
            zombiePathfinder = new Pathfinder(tileMap, true);
            utilityClass = new Utility();
            lightMap = new LightMap(tileMap.GetWidth(), tileMap.GetHeight(), 1.0f);

            currentMenu = new MainMenu(ScreenWidth, ScreenHeight);
            playing = false;

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
            mainMenuTexture = Content.Load<Texture2D>("main_menu_background");
            bulletTexture = Content.Load<Texture2D>("BulletTexture");
            zombieTexture = Content.Load<Texture2D>("Zombee");
            lightTexture = Content.Load<Texture2D>("light");

            spriteFont = Content.Load<SpriteFont>("font");

            tileRenderTarget = new RenderTarget2D(GraphicsDevice, tileMap.GetWidth() * 64, tileMap.GetHeight() * 64);

            ZombieList.Add(new Zombie(zombieTexture, tileMap.GetEntityTile(20, 12)));

            HumanList.Add(new Human(engieTexture, tileMap.GetEntityTile(41, 40), 200, bulletTexture));
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(41, 40), 400, bulletTexture));
        }

        protected override void Update(GameTime gameTime)
        {
            input.UpdateFirstValues();

            if (input.KeyDown(Keys.Escape))
            {
                this.Exit();
               // Environment.Exit(0);
            }

            if (playing)
            {
               // Console.WriteLine("Playing");

                for (int i = 0; i < HumanList.Count; i++)
                {
                    HumanList[i].Update(pathFinder, input, Selection, tileMap, ZombieList);
                }
                for (int i = 0; i < ZombieList.Count; i++)
                {
                    ZombieList[i].Update(HumanList, zombiePathfinder, Selection, tileMap);
                }

                if (input.KeyClicked(Keys.Z))
                {
                    ZombieList.Add(new Zombie(zombieTexture, Selection.GetSelectedTile(tileMap, input.TanslatedMousePos(camera))));
                }

                camera.Input(input);
                Selection.Update(HumanList, camera, tileMap, pathFinder);
               // lightMap.Update();
                tileMap.Update();
                pathFinder.Update();

                camera.Constraint(new Vector2(tileMap.GetWidth() * 64, tileMap.GetHeight() * 64));
                utilityClass.TrapMouse(true, this);
            }
            else
            {
                currentMenu.Update();
            }

            input.UpdateLastValues();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (playing)
            {
                GraphicsDevice.SetRenderTarget(tileRenderTarget);
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                tileMap.Draw();

                for (int i = 0; i < HumanList.Count; i++)
                {
                    HumanList[i].Draw(spriteBatch);

                    HumanList[i].weapon.DrawBullets(spriteBatch);
                }

                for (int i = 0; i < ZombieList.Count; i++)
                {
                    ZombieList[i].Draw(spriteBatch);

                }

                spriteBatch.End();

                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform());
                spriteBatch.Draw(tileRenderTarget, new Vector2(0, 0), Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, "FPS = " + fps, new Vector2(10.0f, 10.0f), Color.White);
                spriteBatch.DrawString(spriteFont, "Mouse Pos = " + input.TanslatedMousePos(camera), new Vector2(10.0f, 30.0f), Color.White);
                spriteBatch.DrawString(spriteFont, "Lights = " + lightMap.GetLightCount(), new Vector2(10.0f, 50.0f), Color.White);
                spriteBatch.End();

                if (LastUpdateFPS + 1000 < Environment.TickCount)
                {
                    fps = fpsCount;
                    fpsCount = 0;
                    LastUpdateFPS = Environment.TickCount;
                }

                fpsCount++;
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                currentMenu.Draw();
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
