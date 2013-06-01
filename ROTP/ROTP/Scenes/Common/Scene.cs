using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ROTP.Scenes.Common
{
    public class Scene : DrawableGameComponent
    {
        private bool _isPopup;
        private TimeSpan _transitionOnTime = TimeSpan.Zero;
        private TimeSpan _transitionOffTime = TimeSpan.Zero;
        private float _transitionPosition = 1;
        private SceneState _sceneState = SceneState.TransitionOn;
        private bool _othersceneHasFocus;
        private readonly SceneManager _sceneManager;
        private bool _isExiting;

        #region getter / setter
        public bool IsPopup
        {
            get { return _isPopup; }
            set { _isPopup = value; }
        }

        public TimeSpan TransitionOnTime
        {
            get { return _transitionOnTime; }
            set { _transitionOnTime = value; }
        }

        public TimeSpan TransitionOffTime
        {
            get { return _transitionOffTime; }
            set { _transitionOffTime = value; }
        }

        public float TransitionPosition
        {
            get { return _transitionPosition; }
            set { _transitionPosition = value; }
        }

        public SceneState SceneState
        {
            get { return _sceneState; }
            set { _sceneState = value; }
        }

        public bool OthersceneHasFocus
        {
            get { return _othersceneHasFocus; }
            set { _othersceneHasFocus = value; }
        }

        public bool IsExiting
        {
            get { return _isExiting; }
            set { _isExiting = value; }
        }

        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
        }

        protected bool IsActive
        {
            get
            {
                return !_othersceneHasFocus &&
                    (_sceneState == SceneState.TransitionOn ||
                    _sceneState == SceneState.Active);
            }
        }

        public SceneManager SceneManager
        {
            get { return _sceneManager; }
        } 
        #endregion

        protected Scene(SceneManager sceneMgr)
            : base(sceneMgr.Game)
        {
        	_sceneManager = sceneMgr;
        }

        public virtual void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            _othersceneHasFocus = otherSceneHasFocus;

            //sortie de la scene?
            if (_isExiting)
            {
                _sceneState = SceneState.TransitionOff;

                if (!UpdateTransition(gameTime, _transitionOffTime, 1))
                    _sceneManager.RemoveScene(this);
            }
            //une autre scene devant?
            else if (coveredByOtherScene)
                _sceneState = UpdateTransition(gameTime, _transitionOffTime, 1)
                    ? SceneState.TransitionOff : SceneState.Hidden;
            //scene active
            else
            {
                _sceneState = UpdateTransition(gameTime, _transitionOnTime, -1)
                    ? SceneState.TransitionOn : SceneState.Active;
            }
        }

        private bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
        	float transitionDelta = time == TimeSpan.Zero
                ? 1 : (float) (gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);

            _transitionPosition += transitionDelta * direction;

            bool endTransition = ((direction < 0) && (_transitionPosition <= 0))
                || ((direction > 0) && (_transitionPosition >= 1));

            if (endTransition)
        		_transitionPosition = MathHelper.Clamp(_transitionPosition, 0, 1);

            return !endTransition;
        }

        public void Remove()
        {
            if (_transitionOffTime == TimeSpan.Zero)
                _sceneManager.RemoveScene(this);
            else
                _isExiting = true;
        }

        public void Add()
        {
            _sceneManager.AddScene(this);
        }

        public virtual void HandleInput() { }
    }
}
