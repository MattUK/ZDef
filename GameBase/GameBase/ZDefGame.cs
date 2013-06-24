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

        public static Player player;

        public static Texture2D terrainTexture;
        public static Texture2D humanBuildingTexture;

        public static Texture2D mainMenuTexture;

        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        public static Utility utilityClass;
        public static List<Human> HumanList; //How dare you change this to static.
        public static List<Zombie> ZombieList; //IT IS STATIC, STATIC
        public static List<Projectile> BulletList;
        public static Texture2D HumanTexture;
        public static Texture2D engieTexture;
        public static Texture2D riflemanTexture;
        public static Texture2D bulletTexture;
        public static Texture2D zombieRiflemanTexture;
        public static Texture2D zombieEngineerTexture;
        public static Texture2D shadowTexture;
        public static Texture2D guiStatus;

        public static SpriteFont spriteFont;
        // =============================================

        // ======= Draw Depths ==============
        public const float TERRAIN_DRAW_DEPTH = 1.0f;
        public const float BUILDING_DRAW_DEPTH = 0.4f;
        public const float HUMAN_DRAW_DEPTH = 0.5f;

        private RenderTarget2D tileRenderTarget;

        private long LastUpdateFPS;
        private int fps, fpsCount;


        //BUTTON TEST
        public static InGame GameGUI;
       // Button button;
        //BUTTON TEST

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
            BulletList = new List<Projectile>();
            pathFinder = new Pathfinder(tileMap);
            zombiePathfinder = new Pathfinder(tileMap, true);
            utilityClass = new Utility();
            lightMap = new LightMap(tileMap.GetWidth(), tileMap.GetHeight(), 0.4f);

            player = new Player();

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
            zombieRiflemanTexture = Content.Load<Texture2D>("RiflemanZombie");
            zombieEngineerTexture = Content.Load<Texture2D>("EngineerZombie");
            shadowTexture = Content.Load<Texture2D>("shadow");
            guiStatus = Content.Load<Texture2D>("GuiStatus");

            spriteFont = Content.Load<SpriteFont>("font");

            tileRenderTarget = new RenderTarget2D(GraphicsDevice, tileMap.GetWidth() * 64, tileMap.GetHeight() * 64);
            GameGUI = new InGame(Content);
            SpawnEngieZombie(20, 20);

            //BUTTONTEST
           // button = new Button(new Vector2(40, ScreenHeight-40), 40, 35, riflemanTexture);
        }

        public static void SpawnEngineer(int i, int j)
        {
            HumanList.Add(new Human(engieTexture, tileMap.GetEntityTile(i, j), 200, bulletTexture));
        }

        public static void SpawnRifleman(int i, int j)
        {
            HumanList.Add(new Human(riflemanTexture, tileMap.GetEntityTile(i, j), 400, bulletTexture));
        }

        public static void SpawnRifleZombie(int i, int j)
        {
            ZombieList.Add(new Zombie(zombieRiflemanTexture, tileMap.GetEntityTile(i, j)));
        }

        public static void SpawnEngieZombie(int i, int j)
        {
            ZombieList.Add(new Zombie(zombieEngineerTexture, tileMap.GetEntityTile(i, j)));
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
                for (int i = 0; i < HumanList.Count; i++)
                {
                    HumanList[i].Update(pathFinder, input, Selection, tileMap, ZombieList);
                }
                for (int i = ZombieList.Count; i-- > 0; )
                {
                    ZombieList[i].Update(HumanList, zombiePathfinder, Selection, tileMap);

                    if (ZombieList[i].Dead == false)
                    {
                        for (int j = BulletList.Count; j-- > 0; )
                        {
                            if (Vector2.Distance(BulletList[j].Position, ZombieList[i].Position) < 17)
                            {
                                BulletList.RemoveAt(j);
                                ZombieList[i].Health -=25;

                                if (ZombieList[i].Health <= 0)
                                {
                                    ZombieList[i].Dead = true;
                                }
                            }
                        }

                        for (int j = HumanList.Count; j-- > 0; )
                        {
                            if (Vector2.Distance(HumanList[j].Position, ZombieList[i].Position) < 30)
                            {
                                SpawnRifleZombie(HumanList[j].CurrentTile.TilePos().X, HumanList[j].CurrentTile.TilePos().Y);
                                HumanList.RemoveAt(j);
                            }
                        }
                    }
                    else
                    {
                        ZombieList.RemoveAt(i);
                    }
                }

                for (int i = 0; i < BulletList.Count; i++)
                {
                    BulletList[i].Update();
                }

                player.Update(gameTime);

                camera.Input(input);
                Selection.Update(HumanList, camera, tileMap, pathFinder);
                lightMap.Update();
                tileMap.Update();
                pathFinder.Update();
                zombiePathfinder.Update();

                camera.Constraint(new Vector2(tileMap.GetWidth() * 64, tileMap.GetHeight() * 64));
                utilityClass.TrapMouse(true, this);
            }
            else
            {
                currentMenu.Update();
            }

            GameGUI.Update(input);

            input.UpdateLastValues();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (playing)
            {
                GraphicsDevice.SetRenderTarget(tileRenderTarget);
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                tileMap.Draw();

                for (int i = 0; i < HumanList.Count; i++)
                {
                    HumanList[i].Draw(spriteBatch);
                }

                for (int i = 0; i < ZombieList.Count; i++)
                {
                    ZombieList[i].Draw(spriteBatch);
                }

                for (int i = 0; i < BulletList.Count; i++)
                {
                    BulletList[i].Draw(spriteBatch);
                }

                spriteBatch.End();

                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform());
                spriteBatch.Draw(tileRenderTarget, new Vector2(0, 0), Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, "FPS = " + fps, new Vector2(400.0f, 10.0f), Color.White);
                spriteBatch.DrawString(spriteFont, "Mouse Pos = " + input.TanslatedMousePos(camera), new Vector2(400, 30.0f), Color.White);
                spriteBatch.DrawString(spriteFont, "Lights = " + lightMap.GetLightCount(), new Vector2(400, 50.0f), Color.White);
                if (Selection.SelectedTile != null)
                {
                    spriteBatch.DrawString(spriteFont, "Small Tile = " + Selection.SelectedTile.TilePos(), new Vector2(400, 70.0f), Color.White);
                }
                
                //BUTTON TEST
                GameGUI.Draw(spriteBatch);
                //BUTTON TEST

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
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();
                currentMenu.Draw();
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
