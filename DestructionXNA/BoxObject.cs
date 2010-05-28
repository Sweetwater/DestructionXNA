using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Math;
using JigLibX.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#if false

namespace DestructionXNA.PhysicObjects {
	public class BoxObject : PhysicObject {
		public BoxObject(Game game, Context context, Model model, Vector3 sideLengths,
			Matrix orientation, Vector3 position)
			: base(game, context, model) {
			// 1.	ボディを作る。
			body = new Body();
			// 2.	ボディを指定して新しい衝突スキンを作る。
			collision = new CollisionSkin(body);
			// 3.	プリミティブを作って衝突スキンに追加する。
			collision.AddPrimitive(
				new Box(-0.5f * sideLengths, orientation, sideLengths),
				new MaterialProperties(0.8f, 0.8f, 0.7f));
			// 4.	ボディのSkinプロパティに衝突スキンを設定する。
			body.CollisionSkin = this.collision;
			// 5.	指定の質量から、質量特性を計算する。
			Vector3 com = SetMass(1.0f);
			// 6.	初期の位置と姿勢をボディに適用する。
			body.MoveTo(position, Matrix.Identity);
			// 7.	衝突スキンを重心位置で補正する。
			collision.ApplyLocalTransform(new Transform(-com, Matrix.Identity));
			// 8.	ボディを有効化する。
			body.EnableBody();
			this.scale = sideLengths;

			color = Vector3.One;
		}
		public override void ApplyEffects(BasicEffect effect) {
			effect.DiffuseColor = color;
		}
	}
}
#endif