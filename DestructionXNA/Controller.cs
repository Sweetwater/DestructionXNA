using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DestructionXNA.Tvchan;
using Microsoft.Xna.Framework.Input;

namespace DestructionXNA.Block
{
    class TVChanController : GameComponent
    {
        private DestructionXNA game;

        private NicoNicoTVChan tvChan;
        public NicoNicoTVChan TVChan {
            set { tvChan = value; }
        }

        private Beam beam;
        public Beam Beam
        {
            set { beam = value; }
        }

        public TVChanController(DestructionXNA game) : base(game) {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            if (game.InputState.IsDown(Keys.Left))
            {
                tvChan.TurnLeft();
            }
            else if (game.InputState.IsDown(Keys.Right))
            {
                tvChan.TurnRight();
            }
            else if (game.InputState.IsDown(Keys.Down))
            {
                tvChan.Spin();
            }

            if (game.InputState.IsDown(Keys.Up))
            {
                tvChan.MoveForward();
            }

            if (game.InputState.IsTrigger(Keys.Space))
            {
                tvChan.Jump();
            }

            if (game.InputState.IsTrigger(Keys.B))
            {
                Vector3 beamFirePos = new Vector3(0, 1, 2);

                Matrix matrix = tvChan.Body.Orientation;
                matrix.Translation = Vector3.Transform(beamFirePos, matrix);
                matrix.Translation += tvChan.Body.Position;

                beam.Fire(matrix);
            }
        }    
    }
}
