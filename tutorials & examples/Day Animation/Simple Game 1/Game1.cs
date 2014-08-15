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

//Using NameSpace
//Utilisation des espaces de nom
using Chimera.Graphics;
using Chimera.Graphics.Effects;
using Chimera.Game_Feature.SceneManager;
namespace Simple_Game_1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Create Texture 
        //Creé une texture
        Texture2D texture;
        //Main Screen
        //Ecran Principale
        game game;
        //Into Screen
        //Ecran d'intro
        intro intro;

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Create Game
            //Crere le jeu
            game = new game();
            //Create Intro Screen
            //Ecran d'ntroduction
            intro = new intro();
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
            //Initialize le moteur
            Chimera.Engine.Initialize(this, graphics);
            //Add Intro and game screnn to the scen manager
            //Ajouter les scene intro et game au scene manager
            SceneManager.Add("intro", intro.update);
            SceneManager.Add("game", game.update);
            //Current Scene
            //La Scene Courante
            SceneManager.Current = "intro";

            //Initaize the game and intro scene
            //Initialse les scene intro et game
            game.initalize();
            intro.initalize();
            Chimera.GUI.MainWindow.Properties.Title = "Graphics Desing by Ahmed Kechkach";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Load Texture for all the cloud
            //Charger les texture pour tout les nuage
            foreach(Image img in game.cloud)
            {
                texture = Content.Load<Texture2D>("cloud");
                img.LoadGraphicsContent(spriteBatch, texture);
            }
            //Load textures
            //Charger les textures
            texture = Content.Load<Texture2D>("sky");
            game.sky.LoadGraphicsContent(spriteBatch, texture);

            texture = Content.Load<Texture2D>("sun");
            game.sun.LoadGraphicsContent(spriteBatch, texture);

            texture = Content.Load<Texture2D>("sol");
            game.sol.LoadGraphicsContent(spriteBatch, texture);

            texture = Content.Load<Texture2D>("logo");
            intro.bg.LoadGraphicsContent(spriteBatch, texture);
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

            //Upadate the current scene
            //met a jour la scene current
            SceneManager.Execute();   
     
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
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            //Draw the current scene
            //dessiner la scene courante
            
            if (SceneManager.Current == "intro")
                intro.draw();
            else if (SceneManager.Current == "game")
                game.draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
