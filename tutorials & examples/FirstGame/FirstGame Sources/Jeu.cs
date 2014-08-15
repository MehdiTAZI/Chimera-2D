using System;
using Chimera.Graphics.Effects.Breaking;
using Chimera.Graphics.Effects;
using Chimera.Helpers.Interface;
using Chimera.Physics;
using Chimera.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Chimera.Helpers;
using Chimera.Audio;
using System.Collections.Generic;
namespace FirstGame
{
    public class Jeu : IDrawable, IUpdateable
    {
        private PeriodicFunction func,addgems,lose;
        public TextWriter text;
        Texture2D lifetext;
        private uint mpos=0;
        public List<Image> gems;
        public Microsoft.Xna.Framework.GameTime time;
        public Color scrcol;
        public List<Image> lifes;
        private List<Microsoft.Xna.Framework.Vector2> gemsvect;
        private List<BoundingBox> gemscol;
           int valx, valy;
        BoundingBox mousecol;
        public Image MouseImg;
        private int score;
        AudioFile flose, fwin, fmusic;
        private Color[] cols ={ Color.MediumSeaGreen, Color.GhostWhite, Color.DarkViolet, Color.CornflowerBlue, Color.DarkMagenta
                               ,Color.DarkSeaGreen,Color.Navy,Color.OrangeRed,Color.DarkOliveGreen,Color.DodgerBlue };
        bool soundbefore = false;
        void function()
        {
            
        }
        void losing()
        {
            //eff.Update();
            MotionBlur.Draw();
            
        }
        
        public Jeu()
        {
            func = new PeriodicFunction(function);
            flose = new AudioFile();
            fwin = new AudioFile();
            fmusic = new AudioFile();
            lose = new PeriodicFunction(losing);
            lose.Enable = false;
            
            addgems = new PeriodicFunction(function);
                 MouseImg = new Image();
                 mousecol = new BoundingBox();
                 text = new TextWriter();
                
            func.Initialize(new TimeSpan(0,0,10));//Decrementé avec le temp
            func.Enable = true;
            addgems.Initialize(new TimeSpan(0, 0, 10));//Decrementé avec le temp
            addgems.Enable = true;
            lose.Initialize(new TimeSpan(0, 0, 1));
            gems = new List<Image>();
            gemscol = new List<BoundingBox>();
            lifes = new List<Image>();
            gemsvect = new List<Microsoft.Xna.Framework.Vector2>();
            for (int i = 0; i < 3; i++)
            {
                gemscol.Add(new BoundingBox());
                gems.Add(new Image());
                gemsvect.Add(new Microsoft.Xna.Framework.Vector2());
            }
         for (int i = 0; i < 13; i++)
             lifes.Add(new Image());
        }
        private void Initgems()
        {
            for (int j = 0; j < gemsvect.Count; j++)
            {
                gemsvect[j] = new Microsoft.Xna.Framework.Vector2(Randomize.GenerateInteger(-10, 10), Randomize.GenerateInteger(-5, 5));
                if (gemsvect[j] == Microsoft.Xna.Framework.Vector2.Zero)
                    gemsvect[j] = new Microsoft.Xna.Framework.Vector2(1, 2);
            }
            int i = 0;
            
            foreach (Image gem in gems)
            {

                gem.Initialize(new Microsoft.Xna.Framework.Vector2(80, 80));
                gem.Stretch = true;
                gem.Color = cols[Randomize.GenerateInteger(0, 9)];
                int val = Randomize.GenerateInteger(0, 100);

                if (gemsvect[i].X > 0)
                    valx = 0;
                else
                    valx = 800;

                if (gemsvect[i].Y > 0)
                    valy = Randomize.GenerateInteger(0,300);
                else
                    valy = Randomize.GenerateInteger(300, 600);

                gem.Position = new Microsoft.Xna.Framework.Vector2(valx,valy);
                
                gemscol[i].Initialize(gem);
                
                i++;
            }
        }
        public void Initalize()
        {


            fmusic.Initialize();
            flose.Initialize();
            fwin.Initialize();
            text.Initialize(new Microsoft.Xna.Framework.Vector2(0,580));
            text.Color = Color.Black;
            text.Text = "Score : 0";
            Chimera.GUI.MainWindow.Properties.Title = "Eat The Gems With The Same Screen Color  | Each Life Give You Bonus Score +10 | Last Score : 0";
            score = 0;
            scrcol = cols[Randomize.GenerateInteger(0, 9)];
            MouseImg.Initialize(new Microsoft.Xna.Framework.Vector2(70, 80));
            Microsoft.Xna.Framework.Vector2 vect;
            vect = Microsoft.Xna.Framework.Vector2.Zero;
            foreach (Image life in lifes)
            {
                life.Initialize(new Microsoft.Xna.Framework.Vector2(50, 50));
                life.Stretch = true;
                life.Position = vect;
                vect += new Microsoft.Xna.Framework.Vector2(life.Size.X+10, 0);
            }

            Initgems();
            
           
           Chimera.Graphics.Mouse.SetImage(MouseImg);
           mousecol.Initialize(MouseImg);

        }
        public void LoadGraphicsContentGem(Microsoft.Xna.Framework.Graphics.SpriteBatch sprite, Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            foreach (Image gem in gems)
                gem.LoadGraphicsContent(sprite, texture);
            
        }
        public void LoadGraphicsContentLife(Microsoft.Xna.Framework.Graphics.SpriteBatch sprite, Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            foreach (Image life in lifes)
                life.LoadGraphicsContent(sprite, texture);

            lifetext = lifes[0].Texture;
        }
        
        public void Draw()
        {
            lose.Update(time);
            foreach (Image gem in gems)
                gem.Draw();

            foreach (Image life in lifes)
               life.Draw();

           text.Draw();
            Chimera.Input.Mouse.Graphic.Draw();

            
        }

        public void Update()
        {

            int i = 0;
            bool die = false;
            Chimera.Input.Mouse.Update(true);
            mousecol.Update();

            foreach (Image gem in gems)
            {
                gem.Position += gemsvect[i];
                
                if (gem.Position.X > 800 || gem.Position.Y > 600 || gem.Position.X < -gem.Size.X || gem.Position.Y < -gem.Size.Y)
                {
                    gemsvect[i] = new Microsoft.Xna.Framework.Vector2(Randomize.GenerateInteger(-10, 10), Randomize.GenerateInteger(-5, 5));
                    if (gemsvect[i] == Microsoft.Xna.Framework.Vector2.Zero)
                        gemsvect[i] = new Microsoft.Xna.Framework.Vector2(1, 2);
                    gem.Color = cols[Randomize.GenerateInteger(0, 9)];
                    if (gemsvect[i].X > 0)
                        valx = 0;
                    else
                        valx = 800;

                    if (gemsvect[i].Y > 0)
                        valy = Randomize.GenerateInteger(0, 300);
                    else
                        valy = Randomize.GenerateInteger(300, 600);

                    gem.Position = new Microsoft.Xna.Framework.Vector2(valx, valy);
                
                    
                    gemscol[i].Update();

                }
                gemscol[i].Update();

                if (gemscol[i].IsCollid(mousecol))
                {
                       
                    if (gem.Color != scrcol)
                    {
                        if (lifes.Count > 0)
                            lifes.RemoveAt(lifes.Count - 1);
                        else
                            die = true;

                        

                        lose.Reset();

                        score -= 7;
                        if (score < 0)
                            score = 0;
                        flose.Play("Content\\lose.wav", SoundType.Sound);
                        
                    }
                    else
                    {
                            score += (5 +(lifes.Count*10));
                            
                            fwin.Play("Content\\win.wav", SoundType.Sound);
                            soundbefore = true;
                            
                    }

                    gem.Color = cols[Randomize.GenerateInteger(0, 9)];

                    gemsvect[i] = new Microsoft.Xna.Framework.Vector2(Randomize.GenerateInteger(-10, 10), Randomize.GenerateInteger(-5, 5));
                    if (gemsvect[i] == Microsoft.Xna.Framework.Vector2.Zero)
                        gemsvect[i] = new Microsoft.Xna.Framework.Vector2(1, 2);

                    if (gemsvect[i].X > 0)
                        valx = 0;
                    else
                        valx = 800;

                    if (gemsvect[i].Y > 0)
                        valy = Randomize.GenerateInteger(0, 300);
                    else
                        valy = Randomize.GenerateInteger(300, 600);

                    gem.Position = new Microsoft.Xna.Framework.Vector2(valx, valy);
                
                    gemscol[i].Update();
                    
                }
                i++;
            }
            
            addgems.Update(time);
            
            if (!addgems.Enable)
            {
                if (gems.Count < 20)
                {
                    addgems.Reset();
                    Image img = new Image();
                    Microsoft.Xna.Framework.Vector2 vect;
                    BoundingBox bb = new BoundingBox();
                    img.LoadGraphicsContent(gems[0].SpriteBatch, gems[0].Texture);
                    img.Initialize(new Microsoft.Xna.Framework.Vector2(80, 80));
                    img.Stretch = true;
                    img.Color = cols[Randomize.GenerateInteger(0, 9)];
                    
                    

                    vect = new Microsoft.Xna.Framework.Vector2(Randomize.GenerateInteger(-10, 10), Randomize.GenerateInteger(-5, 5));
                    if (vect == Microsoft.Xna.Framework.Vector2.Zero)
                        vect = new Microsoft.Xna.Framework.Vector2(1, 2);


                    if (vect.X > 0)
                        valx = 0;
                    else
                        valx = 800;

                    if (vect.Y > 0)
                        valy = Randomize.GenerateInteger(0, 300);
                    else
                        valy = Randomize.GenerateInteger(300, 600);

                    img.Position = new Microsoft.Xna.Framework.Vector2(valx, valy);

                    bb.Initialize(img);

                    gems.Add(img);
                    gemsvect.Add(vect);
                    gemscol.Add(bb);

                }
            }
            
            func.Update(time);
            if (!func.Enable)
            {
                func.Reset();
                scrcol = cols[Randomize.GenerateInteger(0, 9)];
               // MouseImg.Color = cols[Randomize.GenerateInteger(0, 9)];
            }

            MotionBlur.Update();
            text.Text = "Score : " + score.ToString();
            if (!fmusic.IsPlaying())
                fmusic.Play("Content\\Music.mp3", SoundType.Stream);
 

            fmusic.Update();
            flose.Update();
            fwin.Update();
            if (die)
            {
                
                SpriteBatch gemsprite = gems[0].SpriteBatch;
                Texture2D gemstext = gems[0].Texture;
                Chimera.GUI.MainWindow.Properties.Title = "Eat The Gems With The Same Screen Color  | Each Life Give You Bonus Score +10 | Last Score : " + score.ToString();
                gemscol.Clear();
                gems.Clear();
                gemsvect.Clear();
                lifes.Clear();
                score /=4 ;
                for (int j = 0; j < 3; j++)
                {
                    gemscol.Add(new BoundingBox());
                    gems.Add(new Image());
                    gemsvect.Add(new Microsoft.Xna.Framework.Vector2());
                }
                for (int k = 0; k < 13; k++)
                    lifes.Add(new Image());

                LoadGraphicsContentGem(gemsprite, gemstext);
                LoadGraphicsContentLife(gemsprite, lifetext);

                Microsoft.Xna.Framework.Vector2 vect = Microsoft.Xna.Framework.Vector2.Zero;
                foreach (Image life in lifes)
                {
                    life.Initialize(new Microsoft.Xna.Framework.Vector2(50, 50));
                    life.Stretch = true;
                    life.Position = vect;
                    vect += new Microsoft.Xna.Framework.Vector2(life.Size.X + 10, 0);
                }

                Initgems();
            }
        }
    }

}
