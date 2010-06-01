using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JigLibX.Geometry;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA.Block
{
    class DoorBlock : BaseBlock
    {
        Vector3 size = new Vector3(5f, 10f, 1f);

        // コリジョンを複数しようすると重心がずれるが
        // 値を算出する方法が無いため実際に地面において
        // デバッガで高さを取得して設定する
        Vector3 offset = new Vector3(0, -0.603569f, 0);

        public DoorBlock(DestructionXNA game, Model model) :
            base(game, model, BlockType.Roof) {

            positionOffset = offset;
        }

        protected override void CreatePhysicsBody()
        {
            PhysicsObject po = physicsObject;

            po.SetCreateProperty(1.0f, po.Elasticity, po.StaticRoughness, po.DynamicRoughness);

            float chipSize = size.X / 5;

            Vector3 pillarLength = new Vector3(chipSize, size.Y, size.Z);
            Vector3 leftPosition = new Vector3(0, 0, 0);
            Vector3 rightPosition = new Vector3(chipSize * 4, 0, 0);

            Vector3 topBarLength = new Vector3(chipSize * 3, chipSize, size.Z);
            Vector3 topPosition = new Vector3(chipSize, size.Y - chipSize, 0);

            Box leftPillar = new Box(leftPosition, Matrix.Identity, pillarLength);
            Box rightPillar = new Box(rightPosition, Matrix.Identity, pillarLength);
            Box TopBar = new Box(topPosition, Matrix.Identity, topBarLength);

            Box[] boxes = { leftPillar, rightPillar, TopBar };
            po.CreateBoxes(Vector3.Zero, Matrix.Identity, boxes);

            //int splitNum = 10;
            //Box[] boxes = new Box[splitNum];

            //// 頂点の大きさがゼロにならないよう
            //// 頂点少し下を基準にする
            //float height = size.Y - 1;
            //float splitHeight = height / splitNum;
            //float positionY = splitHeight / 2;

            //for (int i = 0; i < boxes.Length; i++)
            //{
            //    // 底辺:高さ = 頂点からの高さ:length という式を
            //    // 解いてXとZの長さを算出する
            //    float topDownward = size.Y - (splitHeight * (i + 1));
            //    float lengthX = (topDownward * size.X) / size.Y;
            //    float lengthZ = (topDownward * size.Z) / size.Y;

            //    Vector3 position = new Vector3(-lengthX / 2, positionY, -lengthZ / 2);
            //    Vector3 length = new Vector3(lengthX, splitHeight, lengthZ);

            //    boxes[i] = new Box(position, Matrix.Identity, length);

            //    positionY += splitHeight;
            //}

        }
    }
}
