using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Scenes.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Scenes
{
    /**
     * ne pas modifier!!! cette classe est la pour montrer la base d'une classe heritant de Scene
     */
    class BasicScene : Scene
    {
        /**
         * le  constructeur prennant en parametre le scene manager
         * pour avoir les scene manager, il faut soit etre dans une scene (par un getter) soit dans le game (par la déclaration).
         */
        public BasicScene(SceneManager manager)
            : base(manager)
        {
            //pour de beaux fondus utiliser ceci
            //transition de debut
            TransitionOnTime = TimeSpan.FromSeconds(1);
            //transition de fin
            TransitionOffTime = TimeSpan.FromSeconds(1);
        }

        /**
         * charger le contenu ici
         * le Content et accessible par SceneManager.Game.Content
         * (à tester avec Game.Content seulement)
         */
        protected override void LoadContent()
        {
        }

        /**
         * décharger le contenu ici
         */
        protected override void UnloadContent()
        {
        }

        /**
         * la methode pour update
         * elle n'a pas le meme prototype attention
         * 
         * /!\ NE PAS GERER LES INPUTS ICI MAIS DANS LA METHODE HandleInput() /!\
         * les scenes étant superposé, les inputs géré ici le serai même quand la fenetre n'a pas le focus
         */
        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            /*
             * commencer par un appel a la base
             * coveredByOtherScene peut etre forcé à faux selon le cas.
             * 
             * plus d'explications sont a venir quand j'aurai (ou toi qui est en train de lire)
             * plus de scene pour tester les différents cas.
             */
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

            //si la scene est active
            if (IsActive)
            {
                // alors coder ici
            }
        }

        /**
         * gerer les inputs ici
         * le SceneManager appelle cette fonction seulement pour la scene qui à le focus.
         */
        public override void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                SceneManager.Game.Exit();
        }

        /**
         * l'affichage se fait ici
         * 
         * le SpriteBatch est accessible par SceneManager.SpriteBatch
         * le GraphicDevice est accessible soit par SceneManager.GraphicsDevice soit directement GraphicsDevice
         * 
         * /!\    si on utilise le spritebatch pour faire de la 2d    /!\
         * /!\ puis on affiche de la 3D, il peu y avoir des problemes /!\
         * /!\  d'affiche de la 3D. cela est dut au spritebatch qui   /!\
         * /!\       modifie certaines option du GraphicDevice.       /!\
         */
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // affichage des transitions
            if (TransitionPosition > 0)
            {
                // de base transparent vers noir utiliser (1f - TransitionAlpha) -> noir vers transparent
                SceneManager.FadeBackBufferToBlack(1f - TransitionAlpha);

                // see http://blogs.msdn.com/b/shawnhar/archive/2010/06/18/spritebatch-and-renderstates-in-xna-game-studio-4-0.aspx
                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            }
        }
    }
}
