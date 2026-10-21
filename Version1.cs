public static class Program
{
    public static void Main()
    {
        const int professorCount = 5;
        var forks = new List<object>
        {
            new object(),
            new object(),
            new object(),
            new object(),
            new object()
        };

        for (var professor = 0; professor < professorCount; professor++) //starts each professor's thread and runs the eat method
        {
            var number = professor;
            var thread = new Thread(() => Eat(number, forks));
            thread.Start();
        }

        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("A deadlock happens here because each professor has the left lock and are waiting for the right lock as well.");
    }

    private static void Eat(int professor, List<object> forks)
    {
        var leftFork = professor;
        var rightFork = (professor + 1) % forks.Count;

        lock (forks[leftFork])
        {
            Console.WriteLine($"Professor {professor + 1} picked up the LEFT fork.");
            Thread.Sleep(100);

            Console.WriteLine($"Professor {professor + 1} is waiting for the RIGHT fork.");
            lock (forks[rightFork])
            {
                Console.WriteLine($"Professor {professor + 1} takes a bite.");
            }
        }
    }
}
