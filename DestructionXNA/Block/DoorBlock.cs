﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JigLibX.Geometry;
using Microsoft.Xna.Framework.Input;
using DestructionXNA.Utility;

namespace DestructionXNA.Block
{
    class DoorBlock : BaseBlock
    {
        Vector3 size = new Vector3(5f, 5f, 1f);

        // コリジョンを複数しようすると重心がずれるが
        // 値を算出する方法が無いため実際に地面において
        // デバッガで高さを取得して設定する
        Vector3 offset = new Vector3(0, -0.46413851f, 0);

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
        }
    }
}
