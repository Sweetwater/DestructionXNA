using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using JigLibX.Physics;
using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Math;
using DestructionXNA.Utility;

namespace DestructionXNA
{
    class Floor : DrawableGameComponent
    {
        private DestructionXNA game;
        private Model model;

        private PhysicsObject physicsObject;
        public Body Body
        {
            get { return physicsObject.Body; }
        }
        public CollisionSkin CollisionSkin
        {
            get { return physicsObject.CollisionSkin; }
        }

        public Floor(DestructionXNA game, Model model)
            : base(game)
        {
            this.game = game;
            this.model = model;

            this.physicsObject = new PhysicsObject("Floor");
            this.physicsObject.SetCreateProperty(1.0f, 0.2f, 0.7f, 0.6f);
            this.physicsObject.CreatePlane(Vector3.Up, 0);
            this.physicsObject.Body.Immovable = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.DrawModel(model, Matrix.Identity);
            base.Draw(gameTime);
        }
    }
}
