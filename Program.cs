using System;

namespace Computer_Science_end_project
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            
            using (var game = new Game1())
                game.Run();
           
          
        }
    }
}
