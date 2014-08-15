using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace SpriteAndAnimation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        
        //Create Background
        //Creer un Arriere Plan
        Chimera.Graphics.BackGround bg;
        //Create floor where the player will walk
        //Creé le sol sur quoi le joeur va marcher
        Chimera.Graphics.Image floor;
        //Create an sprite animation
        //Creer une animation de sprite
        Chimera.Graphics.Animation anim;
        //Create a player
        //Creer un joeur
        Chimera.Graphics.Sprite player;
        //Create scroll effect
        //Creer l'effet scroll ( Defilement)
        Chimera.Graphics.Effects.Scroller scroll;
        Rectangle winrec;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Constructor of the class
            //Constructeur des class
            anim = new Chimera.Graphics.Animation();
            player = new Chimera.Graphics.Sprite();
            scroll = new Chimera.Graphics.Effects.Scroller();
            floor = new Chimera.Graphics.Image();
            bg = new Chimera.Graphics.BackGround();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //Initialize the engine
            //Initialize Le Moteur
            Chimera.Engine.Initialize(this,graphics);

            //Size Of The Animation
            //Taille de l'animation
            anim.Initialize(new Vector2(64,64));
            //Display 24 Frame Per second
            //Afficher 24 Image par seconde
            anim.FramesPerSecond = 24;
            //Animation Positon
            //Position de l'animation
            anim.Position = new Vector2(0, 300);
            //Initialize The Scroll Animation
            //Initialise l'animation du defilement
            scroll.Initialize(graphics, anim);
            //Size Of Player
            //Taille Du Joeur
            player.Initialize(new Vector2(35,77));
            //Player Position
            //Position du joeur
            player.Position = new Vector2(200,250);
            //Size Of The Floor
            //Taille du sol
            floor.Initialize(new Vector2(1010, 171));
            //Floor Position
            //Position du sol
            floor.Position = new Vector2(0, 200);
            //winrec will have the screen rectangle(X,Y,Width,Height)
            //Winrec aura la le rectangle de l'ecran (X,Y,Largeur,Longeur)
            winrec = Chimera.GUI.MainWindow.Properties.ClientBounds;
            //the backgroud will have the size of screen
            //Le Fond decran aura la taille de lecran
            bg.Initialize(new Vector2(winrec.Width, winrec.Height));
            //Stretch the bg;
            //Redimensioner le fond d'ecran
            bg.Stretch = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Read The Previous Tutorial
            //Lire Le  Cours precedent
            texture = Content.Load<Texture2D>("animation");
            anim.LoadGraphicsContent(spriteBatch, texture);
            texture = Content.Load<Texture2D>("player");
            player.LoadGraphicsContent(spriteBatch, texture);
            texture = Content.Load<Texture2D>("floor");
            floor.LoadGraphicsContent(spriteBatch, texture);
            texture = Content.Load<Texture2D>("bg");
            bg.LoadGraphicsContent(spriteBatch, texture);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here


            //If KeyBoard Left Key Is holding so 
            //Si la touche de direction gauche est maintenu(pressé)
            if (Chimera.Input.KeyBoard.IsHoldingKey(Keys.Left))
            {
                //Select the frist animation
                //Selection de la premiere animation
                player.Current_Annimation = 1;
                //Move The player left
                //Bouger le joeur agauche
                player.Position += new Vector2(-2, 0);
                //Next Action
                //Action Suivante                
                player.NextFrame(gameTime);
            }

            //If KeyBoard Left Right Is holding so 
            //Si la touche de direction droite est maintenu(pressé)
            
            else if (Chimera.Input.KeyBoard.IsHoldingKey(Keys.Right))
            {
                //Select the second animation
                //Selection de la seconde animation
                player.Current_Annimation = 2;
                //Move The player right
                //Bouger le joeur droite
                player.Position += new Vector2(2, 0);
                //Next Action
                //Action Suivante                
                player.NextFrame(gameTime);
            }
            
            //If The Player Go Over one of the screen sides...he will be moved to the opposite side
            //si le joeur depasse l'une des limite de l4ecran alors il sera deplacer sur l'autre borne
            if (player.Position.X + player.Size.X > winrec.Width)
                player.Position = new Vector2(0, player.Position.Y);
            else if(player.Position.X+player.Size.X <0)
                player.Position = new Vector2(winrec.Width-player.Size.X, player.Position.Y);

            //Update All The Compenent
            //Met a jour toutes les composantes
            anim.Update(gameTime);
            scroll.Update();
            Chimera.Input.KeyBoard.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            //Display All The Compenent
            //Affiche toutes les composantes
            bg.Draw();
            floor.Draw();
            player.Draw();
            anim.Draw();

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
