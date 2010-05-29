﻿using System;
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
    class Floor : DrawableGameComponent
    {
        private Game1 game;
        private Model model;

        private PhysicsObject physicsObject;

        public Floor(Game1 game, Model model)
            : base(game)
        {
            this.game = game;
            this.model = model;

            this.physicsObject = new PhysicsObject();
            this.physicsObject.CreatePlane(Vector3.Up, 0);
            this.physicsObject.Body.Immovable = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.DrawModel(model, Matrix.Identity);
            base.Draw(gameTime);
        }
    }
}
