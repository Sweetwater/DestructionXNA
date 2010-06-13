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

        private Vector3 length = new Vector3(2.64f, 2.3f, 1.6f);

        private PhysicsObject physicsObject;
        public Body Body
        {
            get { return physicsObject.Body; }
        }
        public CollisionSkin CollisionSkin
        {
            get { return physicsObject.CollisionSkin; }
        }

        public void MoveTo(Vector3 position, Matrix orientation) {
            this.Body.MoveTo(position, orientation);
        }

        public NicoNicoTVChan(DestructionXNA game, Model model) : base(game) {
            this.game = game;
            this.model = model;

            this.physicsObject = new PhysicsObject("NicoNicoTVChan");
            PhysicsObject po = this.physicsObject;
            po.SetCreateProperty(2, 2f, 1f, 1f);
            po.CreateBox(Vector3.Zero, Matrix.Identity, length);
            po.Body.AllowFreezing = false;
        }

        private float turnSpeed = 0.2f;
        private float turnYSpeed = MathHelper.ToRadians(3f);
        private float spinXSpeed = MathHelper.ToRadians(-10);
        private Matrix rotationMatrix = Matrix.Identity;

        private float speed = 0.5f;
        private Vector3 moveVelocity;

        private Vector3 jumpImpulse = new Vector3(0, 100f, 0);

        public void TurnLeft() {
            moveVelocity.Z = turnSpeed;
            rotationMatrix = Matrix.CreateRotationY(turnYSpeed);
        }

        public void TurnRight()
        {
            moveVelocity.Z = turnSpeed;
            rotationMatrix = Matrix.CreateRotationY(-turnYSpeed);
        }

        public void MoveForward() {
            moveVelocity.Z = speed;
        }

        public void Spin()
        {
            rotationMatrix = Matrix.CreateRotationX(spinXSpeed);
        }

        public void Jump() {
            Body.ApplyWorldImpulse(jumpImpulse);
        }

        public override void Update(GameTime gameTime)
        {
            Matrix orientation = Body.Orientation;
            Matrix newOrientation = rotationMatrix * orientation;

            moveVelocity = Vector3.Transform(moveVelocity, newOrientation);
            moveVelocity.Y = 0;

            Body.Orientation = newOrientation;
            Body.Velocity += moveVelocity;

            rotationMatrix = Matrix.Identity;
            moveVelocity = Vector3.Zero;

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
