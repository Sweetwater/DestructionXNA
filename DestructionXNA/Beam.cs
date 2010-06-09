﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JigLibX.Physics;

namespace DestructionXNA.Block
{
    class Beam : DrawableGameComponent
    {
        private DestructionXNA game;
        private Model model;

        private PhysicsObject physicsObject;

        private Vector3 length = new Vector3(1, 1, 2);
        private Vector3 velocity;

        public Body Body {
            get { return physicsObject.Body; }
        }

        public Beam(DestructionXNA game, Model model) : base(game) {
            this.game = game;
            this.model = model;

            this.physicsObject = new PhysicsObject();
            physicsObject.SetCreateProperty(1, 0.8f, 0.8f, 0.7f);
            physicsObject.CreateBox(Vector3.Zero, Matrix.Identity, length);
            physicsObject.Body.ApplyGravity = false;
            physicsObject.Body.AllowFreezing = false;
        }

        public void Fire(Matrix matrix) {
            Vector3 position = matrix.Translation;
            Matrix orientatation = matrix;
            orientatation.Translation = Vector3.Zero;

            this.physicsObject.Body.MoveTo(position, orientatation);

            this.velocity = Vector3.Transform(new Vector3(0, 0, 10), orientatation);
            
            //this.physicsObject.Body.Orientation = matrix;
            //this.physicsObject.Body.Position = matrix.Translation;
        }

        public override void Update(GameTime gameTime)
        {
           //X  this.physicsObject.Body.Force = new Vector3(0, 0, 10000);
           this.physicsObject.Body.Velocity = velocity;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix matrix = physicsObject.Body.Orientation;
            matrix.Translation = physicsObject.Body.Position;

            game.DrawModel(model, matrix);

            game.DebugDrawer.Draw(physicsObject);

            base.Draw(gameTime);
        }
    }
}