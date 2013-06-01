using System;

namespace ROTP
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ROTP game = new ROTP())
            {
                game.Run();
            }
        }
    }
#endif
}

