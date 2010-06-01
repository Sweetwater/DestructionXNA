using System;

namespace DestructionXNA.Block
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DestructionXNA game = new DestructionXNA())
            {
                game.Run();
            }
        }
    }
}

