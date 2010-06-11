using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DestructionXNA.Block;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DestructionXNA
{
    public class House : GameComponent
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

        private enum State {
            Disable,
            Build,
            Destruct,
        }
        private State state;

        private float destructionDistance = 5f;
        private float destrcutionRate = 0.8f;
        private int destructionCount;

        private int rebuildCount = 60 * 30;
        private int rebuildCounter;

        private float rebuildRange = 50;

        private DestructionXNA game;
        private Layout[] blockLayouts;
        private BaseBlock[] blocks;
        private Vector3[] buildPositions;

        public House(DestructionXNA game) : base(game) {
            this.game = game;

            InitializeBlockLayouts();
            CreateBlocks();

            this.state = State.Disable;
        }

        private void InitializeBlockLayouts()
        {
            this.blockLayouts = new Layout[] {
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
        }

        private void CreateBlocks()
        {
            this.blocks = new BaseBlock[blockLayouts.Length];
            this.buildPositions = new Vector3[blocks.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                switch (blockLayouts[i].type)
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
            }

            this.destructionCount = (int)Math.Ceiling(blocks.Length * destrcutionRate);
        }

        private void EnableBlocks()
        {
            foreach (BaseBlock block in blocks)
            {
                block.Enable();
                game.Components.Add(block);
            }
        }

        private void DisableBlocks()
        {
            foreach (BaseBlock block in blocks)
            {
                block.Disable();
                game.Components.Remove(block);
            }
            this.state = State.Disable;
        }

        public void Build(Vector3 basePositoin) {
            DisableBlocks();

            basePositoin.Y = 0;

            for (int i = 0; i < blocks.Length; i++)
            {
                Vector3 position = blockLayouts[i].position + basePositoin;
                Matrix orientation = Matrix.CreateRotationY(blockLayouts[i].rotationY);

                blocks[i].MoveTo(position, orientation);
                buildPositions[i] = position;
            }

            EnableBlocks();
            this.state = State.Build;
        }

        public override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Disable:
                    break;
                case State.Build:
                    CheckDestruction();
                    break;
                case State.Destruct:
                    if (rebuildCounter > 0)
                    {
                        rebuildCounter--;
                    }
                    else
                    {
                        Vector3 basePosition = Vector3.Zero;
                        basePosition.X = GetRebuildPosition();
                        basePosition.Z = GetRebuildPosition();
                        Build(basePosition);
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        private float GetRebuildPosition() {
            float randomValue = ((float)game.Random.NextDouble() - 0.5f) * 2;
            float position = rebuildRange * randomValue;
            return position;
        }

        private void CheckDestruction()
        {
            int numDestruction = 0;
            for (int i = 0; i < blocks.Length; i++)
            {
                float distance = Vector3.Distance(blocks[i].Position, buildPositions[i]);
                if (distance > destructionDistance) {
                    numDestruction++;
                }
            }

            if (numDestruction >= destructionCount) {
                rebuildCounter = rebuildCount;
                this.state = State.Destruct;
            }
        }
    }
}
