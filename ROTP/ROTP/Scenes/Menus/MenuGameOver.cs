using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Scenes.Menus.Common;
using ROTP.Scenes.Common;

namespace ROTP.Scenes.Menus
{
    class MenuGameOver : Menu
    {
         private Scene _parent;

         public MenuGameOver(SceneManager manager, Scene parent)
            : base(manager, "GameOver")
        {
            TransitionOnTime = TimeSpan.FromSeconds(1);
            IsPopup = true;

            this.

            _parent = parent;

            //MenuItem cont = new MenuItem("Reprendre");
            //cont.Selected += OnContinueSelected;
            //MenuItems.Add(cont);

            MenuItem quit = new MenuItem("Quitter");
            quit.Selected += OnquitSelected;
            MenuItems.Add(quit);
        }

        //private void OnContinueSelected(object sender, EventArgs args)
        //{
        //    TransitionOffTime = TimeSpan.FromSeconds(1);
        //    Remove();
        //}

        private void OnquitSelected(object sender, EventArgs args)
        {
            TransitionOffTime = TimeSpan.FromSeconds(0);
            Remove();
            _parent.Remove();
        }
    }
}
