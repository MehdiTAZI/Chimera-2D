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
//Using Chimera Namespace
//Utilisation De l'espace de nom de chimera
using Chimera;
namespace SimpleImage
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //This variable used to load the texture of character
        //Cette variable servira pour charger les texture des personnage
        Texture2D texture;
        //Declare 5 variables Of Image
        //Declare 5 variables de type Image
        Chimera.Graphics.Image boy, girl, light, block1, block2;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Allocates The Variables
            //Alloue Les Variables
            boy = new Chimera.Graphics.Image();
            girl = new Chimera.Graphics.Image();
            light = new Chimera.Graphics.Image();
            block1 = new Chimera.Graphics.Image();
            block2 = new Chimera.Graphics.Image();
         
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
            //Initialize The Engine
            //Initialise Le Moteur
            Engine.Initialize(this, graphics);

            //Initialize Character Size
            //Initiaize La Taille Des Personnages
            boy.Initialize(new Vector2(101,171));
            girl.Initialize(new Vector2(101,171));
            light.Initialize(new Vector2(101,171));
            block1.Initialize(new Vector2(101, 171));
            block2.Initialize(new Vector2(101, 171));

            //Initialize Character Position
            //Initiaize La Position Des Personnages
            boy.Position = new Vector2(200, 200);
            block1.Position= new Vector2(200, 290);
            light.Position = new Vector2(200, 200);
            girl.Position = new Vector2(400, 200);
            block2.Position = new Vector2(400, 290);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("boy");
            boy.LoadGraphicsContent(spriteBatch, texture);

            texture = Content.Load<Texture2D>("girl");
            girl.LoadGraphicsContent(spriteBatch, texture);

            texture = Content.Load<Texture2D>("light");
            light.LoadGraphicsContent(spriteBatch, texture);

            texture = Content.Load<Texture2D>("block");
            block1.LoadGraphicsContent(spriteBatch, texture);
            block2.LoadGraphicsContent(spriteBatch, texture);
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

            //If The Left Key Is Pressed
            //Si La Touche Gauche est cliqué
            if (Chimera.Input.KeyBoard.IsKeyPressed(Keys.Left))
            {
                //Put the "light" Image at the some position like "boy" image
                //Mettre l'image "light" a la meme positon que celle du joeur "boy"
                light.Position = new Vector2(200, 200);
                //Set Current Window Title to This Is A Boy
                //Ecrire This Is A Boy Dans La Fenetre principale
                Chimera.GUI.MainWindow.Properties.Title = "This Is A Boy";
            }

            //If The Right Key Is Pressed
            //Si La Touche Droite est cliqué
            else if (Chimera.Input.KeyBoard.IsKeyPressed(Keys.Right))
            {
                //Put the "light" Image at the some position like "Girl" image
                //Mettre l'image "light" a la meme positon que celle du joeur Girl
                light.Position = new Vector2(400, 200);
                //Set Current Window Title to This Is A Boy
                //Ecrire This Is A Boy Dans La Fenetre principale
                Chimera.GUI.MainWindow.Properties.Title = "This Is A Girl";
            }
            // TODO: Add your update logic here

            //Update The Keyboard
            //Met a jour le clavier
            Chimera.Input.KeyBoard.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Begin Drawing With AlphaBlend Effect
            //Commencer le dessin l'effect aplhablend(tranparence)
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            //Draw The Images
            //Dessiner Les Images
            light.Draw();
            block1.Draw();
            block2.Draw();
            boy.Draw();
            girl.Draw();

            //End Drawing
            //Fin Du Dessin
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
