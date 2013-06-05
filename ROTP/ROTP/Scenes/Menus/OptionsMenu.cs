using ROTP.Scenes.Menus.Common;
using ROTP.Scenes.Common;
using System;
using Microsoft.Xna.Framework;
using ROTP.Save;
using System.Diagnostics;

namespace ROTP.Scenes.Menus
{
    class OptionsMenu : Menu
    {
        private Options _options;

        private MenuItem _fs;
        private MenuItem _bgm;
        private MenuItem _sound;

        public OptionsMenu(SceneManager manager)
            : base(manager, "Options")
        {
            _options = Game1.Options;

            _bgm = new MenuItem("");
            _bgm.Selected += OnBgmSelected;
            MenuItems.Add(_bgm);

            _sound = new MenuItem("");
            _sound.Selected += OnSoundSelected;
            MenuItems.Add(_sound);

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

            _bgm.Text = "BGM : " + (_options.IsBgmOn ? "marche" : "arret");
            _sound.Text = "sons : " + (_options.IsSoundOn ? "marche" : "arret");
            _fs.Text = "Plein ecran : " + (_options.IsFullScreen ? "oui" : "non");
        }

        private void OnFullScreenSelected(object sender, EventArgs args)
        {
            Game1.Graphics.ToggleFullScreen();
            _options.IsFullScreen = Game1.Graphics.IsFullScreen;
        }

        private void OnBgmSelected(object sender, EventArgs args)
        {
            _options.IsBgmOn = !_options.IsBgmOn;
        }

        private void OnSoundSelected(object sender, EventArgs args)
        {
            _options.IsSoundOn = !_options.IsSoundOn;
        }

        private void OnOkSelected(object sender, EventArgs args)
        {
            bool res = SaveManager.SaveOptions(_options);
            Trace.WriteLine("saved = " + res);
            OnCancel();
        }
    }
}
