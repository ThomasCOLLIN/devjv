using ROTP.Scenes.Menus.Common;
using ROTP.Scenes.Common;
using System;
using Microsoft.Xna.Framework;
using ROTP.Save;
using System.Diagnostics;
using ROTP.Options;

namespace ROTP.Scenes.Menus
{
    class OptionsMenu : Menu
    {
        private RotpOptions _options;

        private MenuItem _fs;
        private MenuItem _bgm;
        private MenuItem _sound;

        #region events
        public event EventHandler SoundOptionChanged;
        public event EventHandler MusicOptionChanged;
        public event EventHandler FullScreenOptionChanged;

        private void InitHandlers()
        {
        }

        public void OnSoundChange()
        {
            if (SoundOptionChanged != null)
                SoundOptionChanged(this, new EventArgs());
        }

        public void OnMusicChange()
        {
            if (MusicOptionChanged != null)
                MusicOptionChanged(this, new EventArgs());
        }

        public void OnFullScreenChange()
        {
            if (FullScreenOptionChanged != null)
                FullScreenOptionChanged(this, new EventArgs());
        }

        #endregion

        public OptionsMenu(SceneManager manager)
            : base(manager, "Options")
        {
            InitHandlers();
            _options = Game1.RotpOptions;

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
            OnFullScreenChange();
        }

        private void OnBgmSelected(object sender, EventArgs args)
        {
            _options.IsBgmOn = !_options.IsBgmOn;
            OnMusicChange();
        }

        private void OnSoundSelected(object sender, EventArgs args)
        {
            _options.IsSoundOn = !_options.IsSoundOn;
            OnFullScreenChange();
        }

        private void OnOkSelected(object sender, EventArgs args)
        {
            bool res = SaveManager.SaveOptions(_options);
            Trace.WriteLine("saved = " + res);
            OnCancel();
        }
    }
}
