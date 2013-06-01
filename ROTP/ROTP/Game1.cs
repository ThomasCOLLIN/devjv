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
            new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";

            SceneManager manager = new SceneManager(this);

            Components.Add(manager);

            new TestScene(manager).Add();
        }
    }
}
