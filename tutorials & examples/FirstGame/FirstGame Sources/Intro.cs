using System;
using Chimera.Audio;
using Chimera.Graphics;
using Chimera.Graphics.Effects.PartialFading;
using Chimera.Graphics.Effects;
using Chimera.Helpers.Interface;
using Chimera.Game_Feature.SceneManager;
namespace FirstGame
{
    public class Intro:IDrawable,IUpdateable
    {
        public BackGround bg;
        private PartialFadingSquare eff;
        AudioFile file;
        
       
       public Intro()
        {
            bg = new BackGround();
            eff = new PartialFadingSquare(bg);
            file = new AudioFile();
            
        }
        public void Initialize()
        {
            //eff.Speed = 10;
            bg.Stretch=true;
            
            bg.Initialize(new Microsoft.Xna.Framework.Vector2(800, 600));
            
            eff.Speed = 10;
            file.Initialize();
            eff.Initialize(50);
        }
        bool testplay = false;
        public void Draw()
        {
            if (!testplay)
            {
                file.Play("Content\\intro.wav", SoundType.Sound);
                testplay = true;
            }
            bg.Draw();
            
            eff.Draw();
        }
        public void Update()
        {
            if(testplay)
            file.Update();

            eff.Update();
            if (!eff.Enable)
                SceneManager.Current = "Jeu";
        }

    }
}
