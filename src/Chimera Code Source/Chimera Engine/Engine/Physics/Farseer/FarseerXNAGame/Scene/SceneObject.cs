using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene; 

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene {
    public class SceneObject {
        private Vector2 _position;
        private float _orientation;

        public Vector2 Position {
            get{return ConvertUnits.ToMeters(_position);}
            set { _position = ConvertUnits.ToPixels(value); }              
        }

        public float Orientation {
            get { return _orientation; }
            set { _orientation = value; }
        }
         
        public SceneObject() {
            _position = Vector2.Zero;
            _orientation = 0;
        }

        public void Draw(GraphicsDevice graphicsDevice) {
            //this Update is for updating things like pos and orientation prior
            //to drawing. (entity/view is a good example)
            if (this is IUpdateable) {
                ((IUpdateable)this).Update(); 
            } 
            if (this is IRenderable) {
                ((IRenderable)this).Render(graphicsDevice);
            }            
        }


    }
} 
