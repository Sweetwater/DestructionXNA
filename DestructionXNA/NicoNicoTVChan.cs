using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA
{
    class NicoNicoTVChan : DrawableGameComponent
    {
        private Game1 game;
        private Model model;

        private PhysicsObject physicsObject;

        private Vector3 position = new Vector3(0, 1, 0);
        private Matrix orientation = Matrix.Identity;

        private Vector3 length = new Vector3(0.264f, 0.23f, 0.16f);

        public NicoNicoTVChan(Game1 game, Model model) : base(game) {
            this.game = game;
            this.model = model;

            physicsObject = new PhysicsObject();
            physicsObject.CreateBox(position, orientation, length);
        }

        public override void Update(GameTime gameTime)
        {
            if (game.InputState.IsTrigger(Keys.Space)) {
                physicsObject.Body.MoveTo(new Vector3(0, 1, 0), orientation);
            }


            this.position = physicsObject.Body.Position;
            this.orientation = physicsObject.Body.Orientation;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix matrix = this.orientation;
            matrix.Translation = this.position;
                
            game.DrawModel(model, matrix);

            base.Draw(gameTime);
        }
    }
}
