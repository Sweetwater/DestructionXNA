using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA.Block
{
    class Camera : GameComponent
    {
        protected DestructionXNA game;

        protected Matrix view;
        public Matrix View {
            get { return view; }
        }

        protected Matrix projection;
        public Matrix Projection {
            get { return projection; }
        }

        protected Vector3 target;
        public Vector3 Target {
            set { 
                target = value;
                UpdateViewMatrix();
            }
        }

        protected Vector3 position;
        public Vector3 Position
        {
            set {
                position = value;
                UpdateViewMatrix();
            }
        }

        protected Vector3 refarence;
        protected Vector3 up = Vector3.Up;

        public Camera(DestructionXNA game) : base(game)
        {
            this.game = game;
            this.position = new Vector3(0, 10, 30);
            this.target = new Vector3(0, 0, 0);
            UpdateViewMatrix();
        }

        public override void Initialize()
        {
            InitializeProjection();
            base.Initialize();
        }

        private void InitializeProjection()
        {
            float viewWidth = game.GraphicsDevice.Viewport.Width;
            float viewHeight = game.GraphicsDevice.Viewport.Height;

            float aspectRatio = viewWidth / viewHeight;
            
            this.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                aspectRatio,
                0.005f,
                1000.0f);
        }

        private void UpdateViewMatrix()
        {
            this.view = Matrix.CreateLookAt(position, target, up);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}