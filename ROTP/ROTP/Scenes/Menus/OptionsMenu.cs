using ROTP.Scenes.Menus.Common;
using ROTP.Scenes.Common;
using System;
using Microsoft.Xna.Framework;
using ROTP.Save;

namespace ROTP.Scenes.Menus
{
    class OptionsMenu : Menu
    {
        private Options _options;

        public OptionsMenu(SceneManager manager)
            : base(manager, "Options")
        {
            _options = Game1.Options;

            MenuItem fs = new MenuItem("Plein ecran");
            fs.Selected += OnFullScreenSelected;
            MenuItems.Add(fs);

            MenuItem ok = new MenuItem("Valider");
            ok.Selected += OnOkSelected;
            MenuItems.Add(ok);
        }

        private void OnFullScreenSelected(object sender, EventArgs args)
        {
            Game1.Graphics.ToggleFullScreen();
        }

        private void OnOkSelected(object sender, EventArgs args)
        {
            OnCancel();
        }
    }
}
