using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBase.GUI
{
    class MainMenu : Menu
    {

        private Vector2 normalPlayButton;
        private Vector2 highlightPlayButton;
        private Rectangle playButtonRectangle;

        private bool buttonHighlighted;

        public MainMenu(int gameWidth, int gameHeight)
        {
            normalPlayButton = new Vector2(26.0f, 612.0f);
            highlightPlayButton = new Vector2(282.0f, 612.0f);
            playButtonRectangle = new Rectangle((gameWidth / 2) - (254 / 2), (gameHeight / 2) - (84 / 2), 254, 84);
        }

        public override void Update()
        {
            if (playButtonRectangle.Contains((int)ZDefGame.input.MousePosition().X, (int)ZDefGame.input.MousePosition().Y) && ZDefGame.input.LeftClick())
            {
                ZDefGame.playing = true;
            }
        }

        public override void Draw()
        {
            ZDefGame.spriteBatch.Draw(ZDefGame.mainMenuTexture, new Vector2(0.0f, 0.0f), new Rectangle(0, 0, 800, 600), Color.White);

            if (buttonHighlighted)
            {
                ZDefGame.spriteBatch.Draw(ZDefGame.mainMenuTexture, playButtonRectangle, new Rectangle(282, 612, 254, 84), Color.White);
            }
            else
            {
                ZDefGame.spriteBatch.Draw(ZDefGame.mainMenuTexture, playButtonRectangle, new Rectangle(26, 612, 254, 84), Color.White);
            }
        }

    }
}
