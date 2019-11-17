using System;
using Shared;

namespace FlappyBird_Mono
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (GameMain game = new GameMain())
            {
                game.Run();
            }
        }
    }
#endif
}
