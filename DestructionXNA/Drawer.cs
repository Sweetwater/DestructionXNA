using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FarseerPhysicsTutorial1 {

	public class Drawer {
		readonly GraphicsDevice graphicsDevice;
		BasicEffect effect;
		Vector2[] vertices = new Vector2[32768];
		int[] indices = new int[32768];
		VertexDeclaration declaration;
		int numberVertices;
		int numberIndices;

		public Drawer(GraphicsDevice graphicsDevice) {
			this.graphicsDevice = graphicsDevice;
			effect = new BasicEffect(graphicsDevice, null);
			effect.View = Matrix.Identity;
			declaration = new VertexDeclaration(graphicsDevice, new VertexElement[]{
				new VertexElement(0, 0, VertexElementFormat.Vector2, VertexElementMethod.Default, VertexElementUsage.Position, 0),
			});
		}

		public void Begin(Matrix world) {
			numberIndices = 0;
			numberVertices = 0;
			effect.Begin();
			effect.CurrentTechnique.Passes[0].Begin();
			var viewport = graphicsDevice.Viewport;
			effect.Projection = world;
		}
		public void End() {
			graphicsDevice.VertexDeclaration = declaration;
			graphicsDevice.DrawUserIndexedPrimitives(
				PrimitiveType.LineList, vertices, 0, numberVertices, indices, 0, numberIndices / 2);
			effect.CurrentTechnique.Passes[0].End();
			effect.End();
		}

		public void DrawClosedPolyline(Vector2[] points) {
			if (points.Length <= 1)
				return;

			var nextNumberVertices = numberVertices + points.Length;
			if (nextNumberVertices > vertices.Length) {
				Array.Resize(ref vertices, Math.Max(vertices.Length * 2, nextNumberVertices));
			}
			var nextNumberIndices = numberIndices + points.Length * 2;
			if (nextNumberIndices > indices.Length) {
				Array.Resize(ref indices, Math.Max(indices.Length * 2, nextNumberIndices));
			}

			Array.Copy(points, 0, vertices, numberVertices, points.Length);
			var previous = numberVertices + points.Length - 1;
			for (int i = 0; i < points.Length; i++) {
				indices[numberIndices++] = previous;
				indices[numberIndices++] = previous = numberVertices + i;
			}

			numberVertices = nextNumberVertices;

		}
	}

}
