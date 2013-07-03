using Microsoft.Xna.Framework;
using ROTP.Scenes.Common;
using ROTP.Scenes.Menus;
using ROTP.Scenes;
using ROTP.Save;
using ROTP.Input;
using ROTP.Options;

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

        private static RotpOptions _RotpOptions = null;

        public static RotpOptions RotpOptions
        {
            get
            {
                if (_RotpOptions == null)
                    _RotpOptions = SaveManager.LoadOptions();
                return Game1._RotpOptions;
            }
            set { Game1._RotpOptions = value; }
        }

        public Game1()
        {
            Window.Title = "Rise of the Pony - Pony Stark vs Etramel";
            
            Achievements.Common.AchievementManager.Load();
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = (int) Game1.RotpOptions.Resolution.Y,
                PreferredBackBufferWidth = (int) Game1.RotpOptions.Resolution.X,
                IsFullScreen = Game1.RotpOptions.IsFullScreen
            };
           
            Content.RootDirectory = "Content";

            SceneManager manager = new SceneManager(this);

            Components.Add(new MenuInput(this));
            Components.Add(manager);

            new BackgroundScene(manager).Add();
            new MainMenu(manager).Add();
        }
    }
}
