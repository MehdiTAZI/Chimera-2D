using Chimera;
using System.Collections.Generic;
using Chimera.Graphics;
using System;
using Chimera.Game_Feature.SceneManager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Simple_Game_1
{
    public class game
    {
        //List Of Cloud Image
        //List Des Images de nuage
        public List<Image> cloud;
        //Scroll Effect
        //Effet de defilement
        public List<Chimera.Graphics.Effects.Scroller> scroll; 

        public Image sky, sun, sol;
        public game()
        {
            //creation Of 3 cloud images
            //Creation de 3 image de nuage
            cloud = new List<Image>(3);
            //creation Of 3 scroll effect
            //Creation de 3 effet de defilement
            scroll = new List<Chimera.Graphics.Effects.Scroller>(3);
            //Object Creation
            //Creation des objet
            cloud.Add(new Image());
            cloud.Add(new Image());
            cloud.Add(new Image());
            scroll.Add(new Chimera.Graphics.Effects.Scroller());
            scroll.Add(new Chimera.Graphics.Effects.Scroller());
            scroll.Add(new Chimera.Graphics.Effects.Scroller());
            sky = new Image();
            sun = new Image();
            sol = new Image();
            
        }
        public void initalize()
        {
            //Initialize compenents position and size on the screen
            //initialiser la position et la taille des composantes sur l'ecran
            cloud[0].Initialize(new Vector2(297, 163));
            cloud[0].Position = new Vector2(20,0);
            scroll[0].Initialize(new Rectangle(0,0,800,600),cloud[0]);
            scroll[0].Speed = new Vector2(-0.8f, 0);
            //initialize cloud position and size on the screen
            //initialiser la position et la taile des nuage sur l'ecran
            for(int i=1;i<cloud.Count;i++)
            {
                cloud[i].Initialize(new Vector2(297, 163));
                
                cloud[i].Position += new Vector2( cloud[i - 1].Position.X + cloud[i - 1].Size.X + 20,0) ;
                scroll[i].Initialize(new Rectangle(0, 0, 800, 600), cloud[i]);
                scroll[i].Speed = new Vector2(-i*0.5f, 0);
            }
            sky.Initialize(new Vector2(800, 600));
            sky.Stretch = true;
            sun.Initialize(new Vector2(239, 172));
            sun.Stretch = true;
            sun.Position = new Vector2(800,450);
            
            sol.Initialize(new Vector2(800, 150));
            sol.Position = new Vector2(0, 450);
            sol.Stretch = true;


            foreach (Image img in cloud)
            {
                img.Color = new Color(100, 100, 100, 255);
            }
            sky.Color = new Color(100, 100, 100, 255);
            sun.Color = new Color(100, 100, 100, 255);
            sol.Color = new Color(100, 100, 100, 255);
            
        }
        public void draw()
        {
            //Display ALL THE COMPENENTS ON THE SCREEN
            //Afficher toutes les composantes sur l'ecran

            sky.Draw();
            sun.Draw();
            sol.Draw();
            foreach (Image img in cloud)
                img.Draw();
        }
        //Sin angle value
        //valeur de l'angle du sin
        float v=0;
        //Alpha value of all the compenents ,used for simulate the day to night effect
        //Valeur alpha pour toutes les composantes,utilisé pour la simulation d'une jounrné
        float col = 0;
        byte colv=0;
        public void update()
        {

            float x=sun.Position.X;
            float y = sun.Position.Y;

            //USED FOR Sun Mouvement
            //Utiliser pour le mouvement du soleil
            x-=0.6f;
            v -= 0.004f;
            
            sun.Position = new Vector2(x,(float) System.Math.Sin(v)+y );


            if (sun.Position.X + sun.Size.X < 0)
            {
                v = 0;
                sun.Position = new Vector2(800, 450);
            }
            //Update the effects
            //Met a jours les effets
            foreach (Chimera.Graphics.Effects.Scroller scrl in scroll)
                scrl.Update();
            

            if (x > 400)
                col += 0.5f;
            else if(x<200)
                col -= 0.3f;

            if (col > 255)
                col = 255;
            else if (col < 0)
                col = 0;
            colv = (byte)col;

            
            foreach (Image img in cloud)
            {
                img.Color = new Color(colv,colv,colv,255);
            }
            sky.Color = new Color(colv, colv, colv, 255);
            sun.Color = new Color(colv, colv, colv, 255);
            sol.Color = new Color(colv, colv, colv, 255);
        }
    }
}
