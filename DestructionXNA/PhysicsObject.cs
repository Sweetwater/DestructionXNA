using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using JigLibX.Physics;
using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Math;

namespace DestructionXNA
{
    class PhysicsObject
    {
        private Body body;
        public Body Body { 
            get{ return body; }
        }

        public CollisionSkin collisionSkin;
        public CollisionSkin CollisionSkin { 
            get { return collisionSkin;}
        }

        private float mass = 1.0f;
        private float elasticity = 0.8f;
        private float staticRoughness = 0.8f;
        private float dynamicRoughness = 0.7f;

        public PhysicsObject()
        {
            this.body = new Body();
            this.collisionSkin = new CollisionSkin(body);
        }

        // TODO 外からプロパティを設定するなら実装する
        //public void SetProperty()
        //{
        //}

        public void CreateBox(Vector3 position, Matrix orientation, Vector3 length) {
            Box primitiveBox = new Box(position, orientation, length);

            MaterialProperties materialProp = new MaterialProperties(
                elasticity, staticRoughness, dynamicRoughness);

            CollisionSkin.AddPrimitive(primitiveBox, materialProp);

            SetupProperty(position, orientation);
        }

        public void CreatePlane(Vector3 normal, float d) {
            JigLibX.Geometry.Plane primitivePlane = 
                new JigLibX.Geometry.Plane(Vector3.Up, 0);

            MaterialProperties materialProp = new MaterialProperties(
                elasticity, staticRoughness, dynamicRoughness);

            CollisionSkin.AddPrimitive(primitivePlane, materialProp);

            SetupProperty(Vector3.Zero, Matrix.Identity);
        }

        private void SetupProperty(Vector3 position, Matrix orientation )
        {
            PrimitiveProperties primitiveProp = new PrimitiveProperties(
                PrimitiveProperties.MassDistributionEnum.Solid,
                PrimitiveProperties.MassTypeEnum.Density,
                mass);

            float outMass;
            Vector3 outCenterOfMass;
            Matrix outInertiaTensor;
            Matrix outInertiaTensorCoM;
            CollisionSkin.GetMassProperties(
                primitiveProp,
                out outMass,
                out outCenterOfMass,
                out outInertiaTensor,
                out outInertiaTensorCoM);

            CollisionSkin.ApplyLocalTransform(
                new Transform(-outCenterOfMass, orientation));

            body.BodyInertia = outInertiaTensorCoM;
            body.Mass = outMass;
            body.MoveTo(position, orientation);
            body.CollisionSkin = CollisionSkin;
            body.EnableBody();
        }
    }
}
