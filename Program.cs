﻿namespace Solution
{
    internal class Program
    {
        private static void Main()
        {
            string[] filePathes =
            {

            };

            TextAnalyzer ta = new();

            //ta.ParallelGetTriplets(filePathes, false);
            ta.GetTriplets(@"", true);
        }
    }
}