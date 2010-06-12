using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using JigLibX.Physics;
using DestructionXNA.Block;
using JigLibX.Collision;
using DestructionXNA.Utility;

namespace DestructionXNA.Tvchan
{
    class NicoNicoTVChan : DrawableGameComponent
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

        private Vector3 length = new Vector3(2.64f, 2.3f, 1.6f);

        public Vector3 Position {
            set { physicsObject.Body.Position = value; }
        }

        public Beam Beam { get; set; }

        public NicoNicoTVChan(DestructionXNA game, Model model) : base(game) {
            this.game = game;
            this.model = model;

            this.physicsObject = new PhysicsObject("NicoNicoTVChan");
            PhysicsObject po = this.physicsObject;
            po.SetCreateProperty(10.0f, po.Elasticity, po.StaticRoughness, po.DynamicRoughness);
            po.CreateBox(Vector3.Zero, Matrix.Identity, length);
            po.Body.AllowFreezing = false;
        }

        public override void Update(GameTime gameTime)
        {
            Matrix rotationMatrix = Matrix.Identity;
            Vector3 moveVector = Vector3.Zero;
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

            if (game.InputState.IsTrigger(Keys.B))
            {
                Vector3 beamFirePos = new Vector3(0, 0, 2);

                Matrix matrix = this.physicsObject.Body.Orientation;
                matrix.Translation = Vector3.Transform(beamFirePos, matrix);
                matrix.Translation += this.physicsObject.Body.Position;

                Beam.Fire(matrix);
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

            game.DebugDrawer.Draw(physicsObject);

            base.Draw(gameTime);
        }
    }
}
