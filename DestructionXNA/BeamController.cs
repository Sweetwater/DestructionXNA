using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JigLibX.Physics;
using Microsoft.Xna.Framework;

namespace DestructionXNA.Block
{
    class BeamController : Controller
    {
        Beam beam;
        Vector3 force = new Vector3(0, 0, 10);

        public BeamController(Beam beam)
        {
            this.beam = beam;
        }
        public override void UpdateController(float dt)
        {
            if (beam == null) return;
        }
    }
}
