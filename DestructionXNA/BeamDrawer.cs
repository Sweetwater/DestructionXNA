using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DestructionXNA.Block
{
    class BeamDrawer
    {
        private DestructionXNA game;
        private Texture2D texture;
        private Vector3 length;

        BasicEffect basicEffect;
        VertexDeclaration vertexDeclaration;
        VertexPositionTexture[] pointList;

        short[] triangleStripIndices;

        public BeamDrawer(DestructionXNA game, Texture2D texture, Vector3 length) {
            this.game = game;
            this.texture = texture;
            this.length = length;

            InitializeEffect();
            InitializeTriangleStrip();
        }

        private void InitializeEffect()
        {
            vertexDeclaration = new VertexDeclaration(
                game.GraphicsDevice,
                VertexPositionTexture.VertexElements);

            basicEffect = new BasicEffect(game.GraphicsDevice, null);
            basicEffect.Projection = game.Projection;
            basicEffect.World = Matrix.Identity;

            basicEffect.TextureEnabled = true;
            basicEffect.Texture = this.texture;
        }

        private void InitializeTriangleStrip()
        {
            pointList = new VertexPositionTexture[4];
            pointList[0].TextureCoordinate = new Vector2(0, 0);
            pointList[1].TextureCoordinate = new Vector2(0, 1);
            pointList[2].TextureCoordinate = new Vector2(1, 0);
            pointList[3].TextureCoordinate = new Vector2(1, 1);

            triangleStripIndices = new short[4];
            for (int i = 0; i < triangleStripIndices.Length; i++)
            {
                triangleStripIndices[i] = (short)i;
            }
        }


        public void Draw(Vector3 headPosition, Vector3 tailPosition, Matrix orientation) {

            UpdatePointList(ref headPosition, ref tailPosition, ref orientation);

            game.GraphicsDevice.VertexDeclaration = vertexDeclaration;
            game.GraphicsDevice.RenderState.CullMode = CullMode.None;

            basicEffect.Begin();
            basicEffect.View = game.View;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleStrip,
                    pointList,
                    0,
                    pointList.Length,
                    triangleStripIndices,
                    0,
                    2
                );

                pass.End();
            }
            basicEffect.End();

            game.GraphicsDevice.RenderState.CullMode = CullMode.CullCounterClockwiseFace;
        }

        private void UpdatePointList(ref Vector3 headPosition, ref Vector3 tailPosition, ref Matrix orientation)
        {
            Vector3 offset = new Vector3(0, length.Y / 2, 0);
            offset = Vector3.Transform(offset, orientation);

            float distance = Vector3.Distance(tailPosition, headPosition);
            float u = 1 - (distance / this.length.Z);

            pointList[0].Position = tailPosition + offset;
            pointList[1].Position = tailPosition - offset;
            pointList[0].TextureCoordinate.X = u;
            pointList[1].TextureCoordinate.X = u;

            pointList[2].Position = headPosition + offset;
            pointList[3].Position = headPosition - offset;
        }
    }
}
