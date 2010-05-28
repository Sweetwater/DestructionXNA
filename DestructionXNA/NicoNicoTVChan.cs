using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DestructionXNA
{
    class NicoNicoTVChan : DrawableGameComponent
    {
        private Game1 game;
        private Model model;

        private Vector3 position = new Vector3(0, 0.2f, 0);

        public NicoNicoTVChan(Game1 game, Model model) : base(game) {
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

            Matrix matrix = Matrix.CreateTranslation(position);
            game.DrawModel(model, matrix);

            base.Draw(gameTime);
        }
    }
}
