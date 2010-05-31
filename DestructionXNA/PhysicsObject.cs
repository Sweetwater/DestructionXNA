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
    public class PhysicsObject
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
        public float Mass {
            get { return mass; }
        }

        private float elasticity = 0.8f;
        public float Elasticity {
            get { return elasticity; }
        }

        private float staticRoughness = 0.8f;
        public float StaticRoughness {
            get { return staticRoughness; }
        }

        private float dynamicRoughness = 0.7f;
        public float DynamicRoughness {
            get { return dynamicRoughness; }
        }

        private Vector3 centerOfMass = Vector3.Zero;
        public Vector3 CenterOfMass {
            get { return centerOfMass; }
        }

        public PhysicsObject()
        {
            this.body = new Body();
        }

        public void SetCreateProperty(float mass, float elasticity, float staticRoughness, float dynamicRoughness)
        {
            this.mass = mass;
            this.elasticity = elasticity;
            this.staticRoughness = staticRoughness;
            this.dynamicRoughness = dynamicRoughness;
        }

        public void CreateBox(Vector3 position, Matrix orientation, Vector3 length) {
            this.collisionSkin = new CollisionSkin(this.Body);

            Box primitiveBox = new Box(position, orientation, length);

            MaterialProperties materialProp = new MaterialProperties(
                elasticity, staticRoughness, dynamicRoughness);

            CollisionSkin.AddPrimitive(primitiveBox, materialProp);

            SetupProperty(position, orientation);
        }

        public void CreateBoxes(Vector3 position, Matrix orientation, Box[] primitiveBoxes)
        {
            this.collisionSkin = new CollisionSkin(this.Body);

            MaterialProperties materialProp = new MaterialProperties(
                elasticity, staticRoughness, dynamicRoughness);

            foreach (Box box in primitiveBoxes)
            {
                CollisionSkin.AddPrimitive(box, materialProp);
            }

            SetupProperty(position, orientation);
        }

        public void CreatePlane(Vector3 normal, float d) {
            this.collisionSkin = new CollisionSkin(null);

            JigLibX.Geometry.Plane primitivePlane = 
                new JigLibX.Geometry.Plane(normal, d);

            MaterialProperties materialProp = new MaterialProperties(
                elasticity, staticRoughness, dynamicRoughness);

            CollisionSkin.AddPrimitive(primitivePlane, materialProp);

            PhysicsSystem.CurrentPhysicsSystem.CollisionSystem.AddCollisionSkin(this.CollisionSkin);
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

            this.centerOfMass = outCenterOfMass;

            body.BodyInertia = outInertiaTensorCoM;
            body.Mass = outMass;
            body.MoveTo(position, orientation);
            body.CollisionSkin = CollisionSkin;
            body.EnableBody();
        }
    }
}
