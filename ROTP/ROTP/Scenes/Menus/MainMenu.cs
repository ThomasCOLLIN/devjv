using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Scenes.Common;
using ROTP.Scenes.Menus.Common;

namespace ROTP.Scenes.Menus
{
    class MainMenu : Menu
    {
        public MainMenu(SceneManager manager)
            : base(manager, "Menu principal")
        {
            MenuItem launch = new MenuItem("Lancer le jeu");
            launch.Selected += OnLaunchSelected;
            MenuItems.Add(launch);

            MenuItem exit = new MenuItem("Quitter");
            exit.Selected += OnExitSelected;
            MenuItems.Add(exit);
        }

        public override void OnCancel()
        {
            SceneManager.Game.Exit();
        }

        private void OnLaunchSelected(object sender, EventArgs args)
        {
            new GameScene(SceneManager).Add();
        }

        private void OnExitSelected(object sender, EventArgs args)
        {
            OnCancel();
        }
    }
}
