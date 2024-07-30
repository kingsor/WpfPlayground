
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            List<Task<int>> tasks = Enumerable.Range(1, 5)
                .Select(Calculate)
                .ToList();

            var taskResults = await Task.WhenAll(tasks);

            foreach(var task in taskResults)
            {
                Console.WriteLine(task);
            }
            
            Console.ReadLine();
        }

        private static async Task<int> Calculate(int order)
        {
            Console.WriteLine($"[{DateTime.Now}] Task started {order}");
            var number = Random.Shared.Next(100, 2000);
            Console.WriteLine($"Delay: {number}");
            await Task.Delay(number);
            Console.WriteLine($"[{DateTime.Now}] Task completed {order}");
            return order;
        }
    }
}
