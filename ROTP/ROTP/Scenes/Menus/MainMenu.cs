using System;
using ROTP.Scenes.Common;
using ROTP.Scenes.Menus.Common;
using Microsoft.Xna.Framework.Input;

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

            MenuItem options = new MenuItem("Options");
            options.Selected += OnOptionsSelected;
            MenuItems.Add(options);

            MenuItem achievements = new MenuItem("Succes");
            achievements.Selected += OnAchievementsSelected;
            MenuItems.Add(achievements);

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

        private void OnOptionsSelected(object sender, EventArgs args)
        {
            new OptionsMenu(SceneManager).Add();
        }

        private void OnAchievementsSelected(object sender, EventArgs args)
        {
            new AchievementsMenu(SceneManager).Add();
        }

        private void OnExitSelected(object sender, EventArgs args)
        {
            OnCancel();
        }
    }
}
