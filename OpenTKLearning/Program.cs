namespace OpenTKLearning
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            using var game = new Game(800, 600, "OpenTK Learning");
            game.Run();
        }
    }
}
