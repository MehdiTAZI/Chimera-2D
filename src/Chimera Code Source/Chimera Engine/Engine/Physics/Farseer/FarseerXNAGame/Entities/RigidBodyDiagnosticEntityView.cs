using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Scene;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Sprites;

using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Dynamics;
using Chimera.Physics.Farseer.FarseerGames.FarseerXNAPhysics.Collisions;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Entities {
    public class RigidBodyDiagnosticEntityView : SceneObject, IEntityView  {
        private RigidBody _rigidBody;
        private Sprite _vertexSprite;
        private Sprite _collisionSprite;
        private ContactList _contactList;

        public RigidBodyDiagnosticEntityView(RigidBody rigidBody, Sprite vertexSprite, Sprite collisionSprite) {
            _rigidBody = rigidBody;
            _vertexSprite = vertexSprite;
            _collisionSprite = collisionSprite;
            _contactList = new ContactList(0);
            _rigidBody.Collision += new EventHandler<CollisionEventArgs>(_rigidBody_Collision);
        }

        void _rigidBody_Collision(object sender, CollisionEventArgs e) {
            _contactList = new ContactList(e.Contacts);
        }


        public void Update() {

        }

        public void Render(GraphicsDevice graphicsDevice) {
            foreach (Vector2 vertex in _rigidBody.Geometry.WorldVertices) {
                _vertexSprite.Position = vertex;
                _vertexSprite.Draw(graphicsDevice);
            }
            _vertexSprite.Draw(graphicsDevice);
                        
            foreach (Contact contact in _contactList) {
                _collisionSprite.Position = contact.Position;
                _collisionSprite.Draw(graphicsDevice);
            }
            _contactList.Clear();
        }

        public void LoadGraphicsContent(GraphicsDevice graphicsDevice, ContentManager contentManager) {
            _vertexSprite.LoadGraphicsContent(graphicsDevice, contentManager);
            _collisionSprite.LoadGraphicsContent(graphicsDevice, contentManager);
        }        
    }
}
