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

        private MenuItem _fs;

        public OptionsMenu(SceneManager manager)
            : base(manager, "Options")
        {
            _options = Game1.Options;

            _fs = new MenuItem("");
            _fs.Selected += OnFullScreenSelected;
            MenuItems.Add(_fs);

            MenuItem ok = new MenuItem("Valider");
            ok.Selected += OnOkSelected;
            MenuItems.Add(ok);
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

            _fs.Text = "Plein ecran : " + (_options.IsFullScreen? "oui" : "non");
        }

        private void OnFullScreenSelected(object sender, EventArgs args)
        {
            Game1.Graphics.ToggleFullScreen();
            _options.IsFullScreen = Game1.Graphics.IsFullScreen;
        }

        private void OnOkSelected(object sender, EventArgs args)
        {
            OnCancel();
        }
    }
}
