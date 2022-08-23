namespace lab01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread started");
            Console.Write("Enter n: ");
            int.TryParse(Console.ReadLine(), out int n);

            var task = new Task<double>(() =>
            {
                Console.WriteLine("Task started");
                var res = Calculate(n);
                Console.WriteLine("Task ended");
                return res;
            });
            task.Start();
            Console.WriteLine("Result: {0}", task.Result);
            task.Wait();
            Console.WriteLine("Main thread ended");
        }

        static double Calculate(int n)
        {
            double sum = 0;
            for(int k = 0; k <= n; k++)
            {
                var temp = 1 / Math.Pow(2, k);
                Console.WriteLine(temp);
                sum += temp;
                Thread.Sleep(1000);
            }

            return sum;
        }
    }
}