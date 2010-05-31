using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DestructionXNA
{
    abstract class BaseBlock : DrawableGameComponent
    {
        public enum BlockType {
            Wall,
            HalfWall,
            Roof,
            Door,
        }

        protected Game1 game;
        protected Model model;

        protected PhysicsObject physicsObject;

        protected BlockType type;
        public BlockType Type
        {
            get { return type; }
        }

        public Vector3 Position
        {
            get { return physicsObject.Body.Position; }
            set { physicsObject.Body.MoveTo(value, physicsObject.Body.Orientation); }
        }

        public Matrix Orientation
        {
            get { return physicsObject.Body.Orientation; }
            set { physicsObject.Body.MoveTo(physicsObject.Body.Position, value); }
        }

        public BaseBlock(Game1 game, Model model, BlockType type) : base(game) {
            this.game = game;
            this.model = model;
            this.type = type;

            physicsObject = new PhysicsObject();
            CreatePhysicsBody();
            physicsObject.Body.AllowFreezing = true;
        }

        protected abstract void CreatePhysicsBody();

        public override void  Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix matrix = Matrix.Identity;
            matrix *= this.physicsObject.Body.Orientation;
            matrix.Translation = this.physicsObject.Body.Position;

            game.DrawModel(model, matrix);

            game.DebugDrawer.Draw(physicsObject);

            base.Draw(gameTime);
        }
    }
}
