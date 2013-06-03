using System;

namespace ROTP.Scenes.Menus.Common
{
    class MenuItem
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public event EventHandler Selected;

        
        public MenuItem(string text)
        {
            _text = text;
        }

        public void OnItemSelected()
        {
            if (Selected != null)
                Selected(this, new EventArgs());
        }
    }
}
