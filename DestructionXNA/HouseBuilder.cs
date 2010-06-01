using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DestructionXNA.Block;
using Microsoft.Xna.Framework;

namespace DestructionXNA
{
    public static class HouseBuilder
    {
        private struct Layout {
            public Vector3 position;
            public float rotationY;
            public string type;
            public Layout(
                float x, float y, float z,
                float rotationY,
                string type) {

                this.position = new Vector3(x, y, z);
                this.rotationY = MathHelper.ToRadians(rotationY);
                this.type = type;
            }
        }


        public static void Build(DestructionXNA game) {
            Layout[] layouts = {
                // 1階
                new Layout(-5.5f, 2.5f, 2.5f,  90, "wall"),
                new Layout(-5.5f, 2.5f, -2.5f,  90, "wall"),
                new Layout(-2.5f, 2.5f, -4.5f,  0, "wall"),
                new Layout(2.5f, 2.5f, -4.5f,  0, "wall"),
                new Layout(5.5f, 2.5f, -2.5f,  90, "wall"),
                new Layout(5.5f, 2.5f, 2.5f,  90, "wall"),
                new Layout(-3.75f, 2.5f, 4.5f,  0, "halfWall"),
                new Layout(3.75f, 2.5f, 4.5f,  0, "halfWall"),
                new Layout(0f, 5f, 4.5f,  0, "door"),
                // 2階
                new Layout(-5.5f, 7.5f, 2.5f,  90, "wall"),
                new Layout(-5.5f, 7.5f, -2.5f,  90, "wall"),
                new Layout(-2.5f, 7.5f, -4.5f,  0, "wall"),
                new Layout(2.5f, 7.5f, -4.5f,  0, "wall"),
                new Layout(5.5f, 7.5f, -2.5f,  90, "wall"),
                new Layout(5.5f, 7.5f, 2.5f,  90, "wall"),
                new Layout(-3.75f, 7.5f, 4.5f,  0, "halfWall"),
                new Layout(3.75f, 7.5f, 4.5f,  0, "halfWall"),
                // 3階
                //new Layout(-5.5f, 12.5f, 2.5f,  90, "wall"),
                //new Layout(-5.5f, 12.5f, -2.5f,  90, "wall"),
                //new Layout(-2.5f, 12.5f, -4.5f,  0, "wall"),
                //new Layout(2.5f, 12.5f, -4.5f,  0, "wall"),
                //new Layout(5.5f, 12.5f, -2.5f,  90, "wall"),
                //new Layout(5.5f, 12.5f, 2.5f,  90, "wall"),
                //new Layout(-2.5f, 12.5f, 4.5f,  0, "wall"),
                //new Layout(2.5f, 12.5f, 4.5f,  0, "wall"),
                //// 4階
                //new Layout(0, 20f, 0,  0, "roof"),
                new Layout(0, 15f, 0,  0, "roof"),
            };

            BaseBlock[] blocks = new BaseBlock[layouts.Length];

            for (int i = 0; i < layouts.Length; i++)
            {
                Vector3 position = layouts[i].position;
                Matrix matrix = Matrix.CreateRotationY(layouts[i].rotationY);

                switch (layouts[i].type)
                {
                    case "wall":
                        blocks[i] = new WallBlock(game, game.wallBlockModel);
                        break;
                    case "halfWall":
                        blocks[i] = new HalfWallBlock(game, game.halfWallBlockModel);
                        break;
                    case "roof":
                        blocks[i] = new RoofBlock(game, game.roofBlockModel);
                        break;
                    case "door":
                        blocks[i] = new DoorBlock(game, game.doorBlockModel);
                        break;
                }

                blocks[i].MoveTo(position, matrix);
                game.Components.Add(blocks[i]);
            }
        }
    }
}
