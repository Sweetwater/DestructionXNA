using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JigLibX.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA.Camera
{
    class ThirdPersonCamera : BaseCamera
    {
        private static Body nullBody = new Body();

        private Body targetBody;
        public Body TargetBody {
            set { targetBody = value; }
        }

        private Vector3 targetOffset = new Vector3(0, 3, 0);

        private float positionOffsetY = 10;
        private float positionOffsetZ = -30;

        private float yawAngle;

        public ThirdPersonCamera(DestructionXNA game)
            : base(game) {

            this.targetBody = nullBody;
        }

        public override void Update(GameTime gameTime)
        {
            yawAngle = MathHelper.ToRadians(45);

            // TODO 転んで水平回転したときにカメラ位置が反転するバグを修正する
            Vector3 right = targetBody.Orientation.Right;
            Vector3 cross = Vector3.Cross(right, Vector3.Up);
            Vector3 positionVector = cross * positionOffsetZ;
            positionVector.Y = positionOffsetY;

            Matrix cameraRotation = Matrix.CreateFromYawPitchRoll(yawAngle, 0, 0);
            positionVector = Vector3.Transform(positionVector, cameraRotation);

            position = targetBody.Position + positionVector;
            target = targetBody.Position + targetOffset;

            UpdateViewMatrix();
        }
    }
}
