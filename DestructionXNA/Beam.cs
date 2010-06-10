using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JigLibX.Physics;
using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Math;

namespace DestructionXNA.Block
{
    class Beam : DrawableGameComponent
    {
        private DestructionXNA game;
        private Texture2D texture;
        private Model model;

        private PhysicsObject physicsObject;

        private const int boxSplitNum = 10;
        private const float boxBaseLengthZ = 10;
        private const float boxSplitLengthZ = boxBaseLengthZ / boxSplitNum;
        private Vector3 boxBaseLength = new Vector3(5, 5, boxBaseLengthZ);
        private Vector3 boxSplitLength;

        private Vector3 moveVelocity = new Vector3(0, 0, 10);
        private Vector3 velocity;

        private Vector3 firePosition;

        private BeamDrawer drawer;

        public Body Body {
            get { return physicsObject.Body; }
        }

        public Beam(DestructionXNA game, Texture2D texture, Model model) : base(game) {
            this.game = game;
            this.texture = texture;
            this.model = model;

            InitializePhysicsObject();

            drawer = new BeamDrawer(game, texture, boxBaseLength);
        }


        private void InitializePhysicsObject()
        {
            this.physicsObject = new PhysicsObject();

            int textureWidth = 512;
            int textureHeight = 128;

            boxSplitLength.X = boxBaseLength.X;
            boxSplitLength.Y = boxBaseLength.Y * texture.Height / textureHeight;
            boxSplitLength.Z = boxSplitLengthZ * texture.Width / textureWidth;

            CreateCollision();
            physicsObject.Body.ApplyGravity = false;
            physicsObject.Body.AllowFreezing = false;
        }

        private void CreateCollision() {
            physicsObject.CreateBox(Vector3.Zero, Matrix.Identity, boxSplitLength);
        }

        private void DisableCollision()
        {
            physicsObject.Body.CollisionSkin.RemoveAllPrimitives();
        }

        Vector3 GetCollisionAddPosition(int numPrimitives) {
            Vector3 position = Vector3.Zero;
            position.X = -boxSplitLength.X / 2;
            position.Y = -boxSplitLength.Y / 2;
            position.Z = numPrimitives * -boxSplitLength.Z;

            return position;
        }

        private void EnableCollision()
        {
            int numPrimitives = physicsObject.Body.CollisionSkin.NumPrimitives;
            if (numPrimitives != 0) return;
        
            Vector3 position = GetCollisionAddPosition(numPrimitives);
            Primitive box = new Box(position, Matrix.Identity, boxSplitLength);
            physicsObject.Body.CollisionSkin.AddPrimitive(box, 0);
        }

        private void AddCollision()
        {
            int numPrimitives = physicsObject.Body.CollisionSkin.NumPrimitives;
            if (numPrimitives >= boxSplitNum) return;

            Vector3 position = GetCollisionAddPosition(numPrimitives);
            Primitive box = new Box(position, Matrix.Identity, boxSplitLength);
            physicsObject.Body.CollisionSkin.AddPrimitive(box, 0);
        }

        public void Fire(Matrix matrix) {
            DisableCollision();
            EnableCollision();

            Vector3 position = matrix.Translation;
            Matrix orientatation = matrix;
            orientatation.Translation = Vector3.Zero;

            this.physicsObject.Body.MoveTo(position, orientatation);
            this.velocity = Vector3.Transform(moveVelocity, orientatation);

            this.firePosition = position;
        }

        public override void Update(GameTime gameTime)
        {
            //Vector3 scale = new Vector3(1, 1, 1.00002f);

            //Matrix matrix = Body.Orientation;
            //scale = Vector3.Transform(scale, matrix);
            //matrix *= Matrix.CreateScale(scale);

            //Body.MoveTo(Body.Position, matrix);
            //Transform transform = this.collisionBox.Transform;
            //transform.Orientation *= Matrix.CreateScale(0,0,1.2f);
            //this.collisionBox.Transform = transform;

            int numPrimitives = this.Body.CollisionSkin.NumPrimitives;
            if (numPrimitives < boxSplitNum) {
                float distance = Vector3.Distance(Body.Position, firePosition);
                if (distance > numPrimitives * boxSplitLength.Z)
                {
                    AddCollision();
                }
            }

            this.physicsObject.Body.Velocity = velocity;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix matrix = physicsObject.Body.Orientation;
            matrix.Translation = physicsObject.Body.Position;

            //game.DrawModel(model, matrix);

            drawer.Draw(GetHeadPosition(), GetTailPosition(), this.Body.Orientation);

            game.DebugDrawer.Draw(physicsObject);

            base.Draw(gameTime);
        }

        private Vector3 GetTailPosition()
        {
            Vector3 tailPosition = firePosition;
            Vector3 headPosition = GetHeadPosition();
            float distance = Vector3.Distance(tailPosition, headPosition);

            if (distance > boxBaseLengthZ) {
                Vector3 offset = new Vector3(0, 0, -boxBaseLengthZ);
                offset = Vector3.Transform(offset, this.Body.Orientation);
                tailPosition = headPosition + offset;
            }

            return tailPosition;
        }

        private Vector3 GetHeadPosition() {
            Vector3 offset = new Vector3(0, 0, boxSplitLengthZ);
            offset = Vector3.Transform(offset, this.Body.Orientation);

            Vector3 headPosition = this.Body.Position + offset;
            return headPosition;
        }
    }
}
