using System.Diagnostics;

namespace Solution
{
    internal class Program
    {
        private static void Main()
        {
            Stopwatch sw = new();
            sw.Start();

            TripletAnalyzer ta = new(@"???", 2);

            ta.FindAllMatches();
            ta.SortMatches();
            var matches = ta.TakeMatches(10);

            Console.WriteLine("RunTime: {0}ms", sw.ElapsedMilliseconds);
        }
    }
}