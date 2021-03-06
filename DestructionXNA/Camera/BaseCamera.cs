﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA.Camera
{
    class BaseCamera : GameComponent
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

        public BaseCamera(DestructionXNA game) : base(game)
        {
            this.game = game;
            this.position = new Vector3(0, 10, 30);
            this.target = new Vector3(0, 0, 0);
            InitializeProjection();
            UpdateViewMatrix();
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

        protected void UpdateViewMatrix()
        {
            this.view = Matrix.CreateLookAt(position, target, up);
        }
    }
}