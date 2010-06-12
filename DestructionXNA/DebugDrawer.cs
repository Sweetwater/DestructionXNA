using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JigLibX.Physics;
using DestructionXNA.Utility;



namespace DestructionXNA
{
    public class DebugDrawer : DrawableGameComponent
    {
        BasicEffect basicEffect;
        List<VertexPositionColor> vertexData;

        DestructionXNA game;

        public DebugDrawer(DestructionXNA game)
            : base(game)
        {
            this.game = game;
            this.vertexData = new List<VertexPositionColor>();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            basicEffect = new BasicEffect(this.GraphicsDevice, null);
        }

        public override void Draw(GameTime gameTime)
        {
            if (vertexData.Count == 0) return;

            GraphicsDevice.VertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            this.basicEffect.AmbientLightColor = Vector3.One;
            this.basicEffect.View = game.View;
            this.basicEffect.Projection = game.Projection;
            this.basicEffect.VertexColorEnabled = true;

            this.basicEffect.Begin();
            foreach (EffectPass pass in this.basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip,
                    vertexData.ToArray(), 0, vertexData.Count - 1);

                pass.End();
            }
            this.basicEffect.End();

            vertexData.Clear();

            base.Draw(gameTime);
        }


        #region Alex's addition for Body Renderer

        public void DrawShape(List<Vector3> shape, Color color)
        {
            if (vertexData.Count > 0)
            {
                Vector3 v = vertexData[vertexData.Count - 1].Position;
                vertexData.Add(new VertexPositionColor(v, new Color(0, 0, 0, 0)));
                vertexData.Add(new VertexPositionColor(shape[0], new Color(0, 0, 0, 0)));
            }

            foreach (Vector3 p in shape)
            {
                vertexData.Add(new VertexPositionColor(p, color));
            }
        }

        public void DrawShape(List<Vector3> shape, Color color, bool closed)
        {
            DrawShape(shape, color);

            Vector3 v = shape[0];
            vertexData.Add(new VertexPositionColor(v, color));
        }

        public void DrawShape(List<VertexPositionColor> shape)
        {
            if (vertexData.Count > 0)
            {
                Vector3 v = vertexData[vertexData.Count - 1].Position;
                vertexData.Add(new VertexPositionColor(v, new Color(0, 0, 0, 0)));
                vertexData.Add(new VertexPositionColor(shape[0].Position, new Color(0, 0, 0, 0)));
            }

            foreach (VertexPositionColor vps in shape)
            {
                vertexData.Add(vps);
            }
        }

        public void DrawShape(VertexPositionColor[] shape)
        {
            if (vertexData.Count > 0)
            {
                Vector3 v = vertexData[vertexData.Count - 1].Position;
                vertexData.Add(new VertexPositionColor(v, new Color(0, 0, 0, 0)));
                vertexData.Add(new VertexPositionColor(shape[0].Position, new Color(0, 0, 0, 0)));
            }

            foreach (VertexPositionColor vps in shape)
            {
                vertexData.Add(vps);
            }
        }

        public void DrawShape(List<VertexPositionColor> shape, bool closed)
        {
            DrawShape(shape);

            VertexPositionColor v = shape[0];
            vertexData.Add(v);
        }
       
        #endregion



        public void Draw(PhysicsObject physicsObject)
        {
            if (game.DebugDrawer.Enabled)
            {
                VertexPositionColor[] wf;
                 wf = physicsObject.Body.CollisionSkin.GetLocalSkinWireframe();

                // if the collision skin was also added to the body
                // we have to transform the skin wireframe to the body space
                 if (physicsObject.Body.CollisionSkin != null)
                {
                    physicsObject.Body.TransformWireframe(wf);
                }

                game.DebugDrawer.DrawShape(wf);
            }
        }
    }
}
