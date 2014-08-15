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
using Chimera.Audio;
using Chimera;
using Chimera.Input;
using Chimera.Game_Feature.SceneManager;


namespace FirstGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        SpriteFont spritefont;

        Intro intro;
        Jeu game;
    
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            intro = new Intro();
            game = new Jeu();
            
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

            Engine.Initialize(this, graphics);
            
            
            
            base.Initialize();
            Chimera.Graphics.Effects.MotionBlur.Initialize(graphics, spriteBatch);
            game.Initalize();
            intro.Initialize();
            
            Chimera.Input.Mouse.Graphic.Initialize(new Vector2(70, 80));
            
            SceneManager.Add("Intro", new Scene(intro.Draw, intro.Update));
            SceneManager.Add("Jeu", new Scene(game.Draw, game.Update));
           
            SceneManager.Current = "Intro";// TO CHANGE
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Logo");
            intro.bg.LoadGraphicsContent(spriteBatch,texture);

            texture = Content.Load<Texture2D>("Heart");
            game.LoadGraphicsContentLife(spriteBatch, texture);

            texture = Content.Load<Texture2D>("Star");
            game.MouseImg.LoadGraphicsContent(spriteBatch, texture);
            texture = Content.Load<Texture2D>("Gem");
            game.LoadGraphicsContentGem(spriteBatch, texture);

            spritefont = Content.Load<SpriteFont>("Font");
            game.text.LoadGraphicsContent(spriteBatch, spritefont);
            
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
            if (KeyBoard.IsKeyPressed(Keys.Escape))
                this.Exit();
            else if (KeyBoard.IsKeyPressed(Keys.F11))
                Chimera.Helpers.GameOptions.Properties.ToggleFullScreen();

            game.time = gameTime;
            // TODO: Add your update logic here
            SceneManager.Update();
            KeyBoard.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            graphics.GraphicsDevice.Clear(game.scrcol);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            SceneManager.Draw();
           
            spriteBatch.End();
       
            base.Draw(gameTime);
        }
    }
}
