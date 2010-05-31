using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DestructionXNA
{
    class WallBlock : BaseBlock
    {
        private Vector3 length = new Vector3(5f, 5f, 1f);

        public WallBlock(Game1 game, Model model) :
            base(game, model, BlockType.Wall) {
        }

        protected override void CreatePhysicsBody()
        {
            PhysicsObject po = physicsObject;

            po.SetCreateProperty(2.0f, po.Elasticity, po.StaticRoughness, po.DynamicRoughness);
            po.CreateBox(Vector3.Zero, Matrix.Identity, length);
        }
    }
}
