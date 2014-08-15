using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Components;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame {
    public class FarseerGame : Game {
        protected GraphicsDeviceManager _graphics;
        private ContentManager _content;
        private static SpriteBatch _farseerSpriteBatch;

        private SceneManager _sceneManager;
        private EntityManager _entityManager;

        private KeyboardInputComponent _keyboardInputComponent;
        #if !XBOX
        private MouseInputComponent _mouseInputComponent;
        #endif
        public FarseerPhysicsComponent _farseerPhysicsComponent;
        
        public FarseerGame() {            
            _graphics = new GraphicsDeviceManager(this);
            _content = new ContentManager(Services);

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0,16);
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;

            Window.AllowUserResizing = false;
            Window.ClientSizeChanged += new System.EventHandler(Window_ClientSizeChanged);
   
            //_scene = new SceneGraph();
            _sceneManager = new SceneManager();
            _entityManager = new EntityManager();
            _keyboardInputComponent = new KeyboardInputComponent(this);
            _keyboardInputComponent.EscapeKeyDown += new EventHandler<KeyEventArgs>(KeyboardInputComponent_EscKeyDown);
            #if !XBOX
            _mouseInputComponent = new MouseInputComponent(this);
            #endif
            _farseerPhysicsComponent = new FarseerPhysicsComponent(this);

            Components.Add(_keyboardInputComponent);
            #if !XBOX
            Components.Add(_mouseInputComponent);
            #endif
            Components.Add(_farseerPhysicsComponent);
        }

        public FarseerPhysicsComponent FarseerPhysicsComponent {
            get { return _farseerPhysicsComponent; }
        }

        public KeyboardInputComponent KeyboardInputComponent {
            get { return _keyboardInputComponent; }
        }
        #if !XBOX
        public MouseInputComponent MouseInputComponent {
            get { return _mouseInputComponent; }
        }
        #endif
        public SceneManager SceneManager{
            get { return _sceneManager; }
        }

        public EntityManager EntityManager{
            get{return _entityManager;}
        }

        public static SpriteBatch FarseerSpriteBatch {
            get { return _farseerSpriteBatch; }
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _farseerSpriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _entityManager.LoadToPhysicsSimulator(_farseerPhysicsComponent._physicsSimulator);
            _sceneManager.LoadGraphicsContent(_graphics.GraphicsDevice, _content);
            base.LoadContent();
//            base.LoadGraphicsContent(loadAllContent);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.SlateBlue);
            _sceneManager.Draw(_graphics.GraphicsDevice);

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        void Window_ClientSizeChanged(object sender, System.EventArgs e) {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;              
        }

        void KeyboardInputComponent_EscKeyDown(object sender, KeyEventArgs e) {
            Exit();
        }
    }
}
