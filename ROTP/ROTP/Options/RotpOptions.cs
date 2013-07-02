using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ROTP.Options
{
    [Serializable]
    public class RotpOptions
    {
        private bool _isFullScreen;
        private Vector2 _resolution;
        private bool _isBgmOn;
        private bool _isSoundOn;

        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set { _isFullScreen = value; }
        }

        public Vector2 Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public bool IsBgmOn
        {
            get { return _isBgmOn; }
            set { _isBgmOn = value; }
        }

        public bool IsSoundOn
        {
            get { return _isSoundOn; }
            set { _isSoundOn = value; }
        }
    }
}
