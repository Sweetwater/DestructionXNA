﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using JigLibX.Physics;

namespace DestructionXNA
{
    class NicoNicoTVChan : DrawableGameComponent
    {
        private Game1 game;
        private Model model;

        private PhysicsObject physicsObject;

        private Vector3 length = new Vector3(2.64f, 2.3f, 1.6f);

        public NicoNicoTVChan(Game1 game, Model model) : base(game) {
            this.game = game;
            this.model = model;

            this.physicsObject = new PhysicsObject();
            PhysicsObject po = this.physicsObject;
            po.SetCreateProperty(1.0f, po.Elasticity, po.StaticRoughness, po.DynamicRoughness);
            po.CreateBox(Vector3.Zero, Matrix.Identity, length);
            po.Body.AllowFreezing = false;
        }

        public override void Update(GameTime gameTime)
        {
            Matrix rotationMatrix = Matrix.Identity;
            Vector3 moveVector = Vector3.Zero; //new Vector3(0, 0, 0.01f);
            if (game.InputState.IsDown(Keys.Left))
            {
                moveVector = new Vector3(0, 0, 0.5f);
                rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(5));
            }
            else if (game.InputState.IsDown(Keys.Right)) {
                moveVector = new Vector3(0, 0, 0.5f);
                rotationMatrix = Matrix.CreateRotationY(-MathHelper.ToRadians(5));
            }
            else if (game.InputState.IsDown(Keys.Up))
            {
                moveVector = new Vector3(0, 0, 0.5f);
            }
            else if (game.InputState.IsDown(Keys.Down))
            {
                rotationMatrix = Matrix.CreateRotationX(-MathHelper.ToRadians(10));
            }

            if (game.InputState.IsTrigger(Keys.Space))
            {
                physicsObject.Body.ApplyWorldImpulse(new Vector3(0, 100f, 0));
            }


            physicsObject.Body.Orientation = rotationMatrix * physicsObject.Body.Orientation;
            moveVector = Vector3.Transform(moveVector, physicsObject.Body.Orientation);
            moveVector.Y = 0;
            physicsObject.Body.Velocity += moveVector;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix matrix = physicsObject.Body.Orientation;
            matrix.Translation = physicsObject.Body.Position;
                
            game.DrawModel(model, matrix);

            base.Draw(gameTime);
        }
    }
}