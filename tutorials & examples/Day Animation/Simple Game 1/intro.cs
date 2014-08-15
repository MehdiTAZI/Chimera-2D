using Chimera;
using Chimera.Graphics;
using Chimera.Graphics.Effects;
using Chimera.Game_Feature.SceneManager;
using Microsoft.Xna.Framework;

namespace Simple_Game_1
{
    public class intro
    {
        //Background
        //Arriere plan
        public BackGround bg;
        //Fading Effect
        //Effet Fading
        Fading effect;
        int nb = 1;
        public intro()
        {

            bg = new BackGround();
            effect = new Fading();

        }

        public void initalize()
        {
            //Initalize The Effects
            //Initalisation Des Effet
           bg.Initialize(new Vector2(800, 600));
            //Alow the image stretching
            //Permetre le redimensionement de l'image
           bg.Stretch = true;
            //The Effect Will Be Applied on the backgroud bg
            //L'effet sera appliqué sur l'arriere plan bg
           effect.Initialize(bg);
            //Start And End Value Of Fading Effect
            //Valeur de debut et de fin d'effet
           effect.SetEffectParameter(0, 255);
        }
        public void draw()
        {
            bg.Draw();
        }
        public void update()
        {
            /*
             * This source code allow you to do fading effect twice time from 0 to 255 and from 255 to 0 alpha value
             * Ce Code source vous permet d'utliser l'effet fading 2 fois la premier depuis la valeur 0 a 255 et la second de 255 a 0
             */
                effect.Update();
            if(!effect.Enable)
            {
                if (nb < 2)
                {
                    nb++;
                    effect.Reset();
                    effect.SetEffectParameter(255, 0);
                }
                else
                {
                    // put current scene on game screen
                    //mettre la scene courante sur l'ecran du jeu
                    SceneManager.Current = "game";
                }

            }


        }
    }
}
