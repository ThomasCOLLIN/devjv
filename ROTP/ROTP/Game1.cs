using Microsoft.Xna.Framework;
using ROTP.Scenes.Common;
using ROTP.Scenes.Menus;
using ROTP.Scenes;
using ROTP.Save;

namespace ROTP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private static GraphicsDeviceManager _graphics;

        public static GraphicsDeviceManager Graphics
        {
            get { return Game1._graphics; }
        }

        private static Options _options = null;

        public static Options Options
        {
            get
            {
                if (_options == null)
                    _options = SaveManager.LoadOptions();
                return Game1._options;
            }
            set { Game1._options = value; }
        }

        public Game1()
        {
            Window.Title = "Rise of the Pony - Pony Stark vs Etramel";

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = (int) Game1.Options.Resolution.Y,
                PreferredBackBufferWidth = (int) Game1.Options.Resolution.X,
                IsFullScreen = Game1.Options.IsFullScreen
            };
           
            Content.RootDirectory = "Content";

            SceneManager manager = new SceneManager(this);

            Components.Add(manager);

            new MainMenu(manager).Add();
        }
    }
}
