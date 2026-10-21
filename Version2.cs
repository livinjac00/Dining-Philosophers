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

        for (var professor = 0; professor < professorCount; professor++) //starts each professor's thread
        {
            var number = professor;
            var thread = new Thread(() => Eat(number, forks));
            thread.Start();
        }

        Thread.Sleep(1000);
    }

    private static void Eat(int professor, List<object> forks)
    {
        var leftFork = professor;
        var rightFork = (professor + 1) % forks.Count;

        if (professor % 2 == 0) //every even numbered thread gets the left fork first and then the right fork
        {
            EatLeftThenRight(professor, forks, leftFork, rightFork);
            return;
        }

        EatRightThenLeft(professor, forks, leftFork, rightFork);
    }

    private static void EatLeftThenRight(int professor, List<object> forks, int leftFork, int rightFork)
    {
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

    private static void EatRightThenLeft(int professor, List<object> forks, int leftFork, int rightFork)
    {
        lock (forks[rightFork])
        {
            Console.WriteLine($"Professor {professor + 1} picked up the RIGHT fork.");
            Thread.Sleep(100);

            Console.WriteLine($"Professor {professor + 1} is waiting for the LEFT fork.");
            lock (forks[leftFork])
            {
                Console.WriteLine($"Professor {professor + 1} takes a bite.");
            }
        }
    }
}
