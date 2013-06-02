using Microsoft.Xna.Framework;
using ROTP.Scenes.Common;
using ROTP.Scenes;

namespace ROTP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public Game1()
        {
            Window.Title = "Rise of the Pony - Pony Stark vs Etramel";

            new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 500,
                PreferredBackBufferWidth = 500,
                IsFullScreen = false
            };
           
            Content.RootDirectory = "Content";

            SceneManager manager = new SceneManager(this);

            Components.Add(manager);

            new GameScene(manager).Add();
        }
    }
}
