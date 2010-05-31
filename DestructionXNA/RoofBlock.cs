using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JigLibX.Geometry;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA
{
    class RoofBlock : BaseBlock
    {
        Vector3 size = new Vector3(12, 10, 12);

        // コリジョンを複数しようすると重心がずれるが
        // 値を算出する方法が無いため実際に地面において
        // デバッガで高さを取得して設定する
        Vector3 offset = new Vector3(0, 2.605246538f, 0);

        public RoofBlock(Game1 game, Model model) :
            base(game, model, BlockType.Roof) {
        }

        protected override void CreatePhysicsBody()
        {
            PhysicsObject po = physicsObject;

            po.SetCreateProperty(1.0f, po.Elasticity, po.StaticRoughness, po.DynamicRoughness);

            int splitNum = 10;
            Box[] boxes = new Box[splitNum];

            // 頂点の大きさがゼロにならないよう
            // 頂点少し下を基準にする
            float height = size.Y - 1;
            float splitHeight = height / splitNum;
            float positionY = splitHeight / 2;

            for (int i = 0; i < boxes.Length; i++)
            {
                // 底辺:高さ = 頂点からの高さ:length という式を
                // 解いてXとZの長さを算出する
                float topDownward = size.Y - (splitHeight * (i + 1));
                float lengthX = (topDownward * size.X) / size.Y;
                float lengthZ = (topDownward * size.Z) / size.Y;

                Vector3 position = new Vector3(-lengthX / 2, positionY, -lengthZ / 2);
                Vector3 length = new Vector3(lengthX, splitHeight, lengthZ);

                boxes[i] = new Box(position, Matrix.Identity, length);

                positionY += splitHeight;
            }

            po.CreateBoxes(Vector3.Zero, Matrix.Identity, boxes);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix matrix = Matrix.Identity;
            matrix.Translation += offset;
            matrix *= this.physicsObject.Body.Orientation;
            matrix.Translation += this.physicsObject.Body.Position;

            game.DrawModel(model, matrix);

            game.DebugDrawer.Draw(physicsObject);
        }
    }
}
