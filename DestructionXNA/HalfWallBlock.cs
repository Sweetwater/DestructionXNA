using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DestructionXNA
{
    class HalfWallBlock : BaseBlock
    {
        private Vector3 length = new Vector3(2.5f, 5f, 1f);

        public HalfWallBlock(Game1 game, Model model) :
            base(game, model, BlockType.HalfWall) {
        }

        protected override void CreatePhysicsBody()
        {
            PhysicsObject po = physicsObject;

            po.SetCreateProperty(1.0f, po.Elasticity, po.StaticRoughness, po.DynamicRoughness);
            po.CreateBox(Vector3.Zero, Matrix.Identity, length);
        }
    }
}
