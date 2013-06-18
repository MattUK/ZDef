using System;

namespace GameBase
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ZDefGame game = new ZDefGame())
            {
                game.Run();
            }
        }
    }
#endif
}

