using System;
using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DestructionXNA.PhysicObjects
{

    /// <summary>
    /// Helps to combine the physics with the graphics.
    /// </summary>
    public abstract class PhysicObject : DrawableGameComponent
    {

        protected Body body;
        protected CollisionSkin collision;

        protected Model model;
        protected Vector3 color;

        protected Vector3 scale = Vector3.One;

        public Body PhysicsBody { get { return body; } }
        public CollisionSkin PhysicsSkin { get { return collision; } }
        public Vector3 Scale { get { return scale; } }

        Context context;
        public Context Context { get { return context; } }

        protected static Random random = new Random();

        public PhysicObject(Game game, Context context, Model model)
            : base(game)
        {
            this.model = model;
            this.context = context;
            color = new Vector3(random.Next(255), random.Next(255), random.Next(255));
            color /= 255.0f;
        }

        public PhysicObject(Game game, Context context)
            : base(game)
        {
            this.model = null;
            this.context = context;
            color = new Vector3(random.Next(255), random.Next(255), random.Next(255));
            color /= 255.0f;
        }

        protected Vector3 SetMass(float mass)
        {
            PrimitiveProperties primitiveProperties =
                new PrimitiveProperties(PrimitiveProperties.MassDistributionEnum.Solid, PrimitiveProperties.MassTypeEnum.Density, mass);

            float junk; Vector3 com; Matrix it, itCoM;

            collision.GetMassProperties(primitiveProperties, out junk, out com, out it, out itCoM);
            body.BodyInertia = itCoM;
            body.Mass = junk;

            return com;
        }
        Matrix[] boneTransforms = null;
        int boneCount = 0;

        public abstract void ApplyEffects(BasicEffect effect);
        public override void Draw(GameTime gameTime)
        {
            if (model != null)
            {
                if (boneTransforms == null || boneCount != model.Bones.Count)
                {
                    boneTransforms = new Matrix[model.Bones.Count];
                    boneCount = model.Bones.Count;
                }

                model.CopyAbsoluteBoneTransformsTo(boneTransforms);

                Camera camera = context.Camera;
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {

                        // the body has an orientation but also the primitives in the collision skin
                        // owned by the body can be rotated!
                        if (body.CollisionSkin != null)
                            effect.World = boneTransforms[mesh.ParentBone.Index] * Matrix.CreateScale(scale) * body.CollisionSkin.GetPrimitiveLocal(0).Transform.Orientation * body.Orientation * Matrix.CreateTranslation(body.Position);
                        else
                            effect.World = boneTransforms[mesh.ParentBone.Index] * Matrix.CreateScale(scale) * body.Orientation * Matrix.CreateTranslation(body.Position);

                        effect.View = camera.View;
                        effect.Projection = camera.Projection;

                        ApplyEffects(effect);

                        //if (!this.PhysicsBody.IsActive)
                        //    effect.Alpha = 0.4f;
                        //else
                        //    effect.Alpha = 1.0f;


                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                    }
                    mesh.Draw();
                }

            }

            if (context.DebugDrawer.Enabled)
            {

                wf = collision.GetLocalSkinWireframe();

                // if the collision skin was also added to the body
                // we have to transform the skin wireframe to the body space
                if (body.CollisionSkin != null)
                {
                    body.TransformWireframe(wf);
                }

                context.DebugDrawer.DrawShape(wf);
            }


            // base.Draw(gameTime);
        }

        VertexPositionColor[] wf;

    }
}
