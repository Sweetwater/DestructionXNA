﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DestructionXNA
{
    class Floor : DrawableGameComponent
    {
        private Game1 game;
        private Model model;

        public Floor(Game1 game, Model model)
            : base(game)
        {
            this.game = game;
            this.model = model;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Debug.WriteLine("NicoTVChan.Draw");

            game.DrawModel(model, Matrix.Identity);

            base.Draw(gameTime);
        }
    }
}
