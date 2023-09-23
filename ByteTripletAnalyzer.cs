using System.Diagnostics;
using System.Linq.Expressions;

namespace Solution
{
    internal class ByteTripletAnalyzer : TripletAnalyzer
    {
        public override SearchResult? FindAllMatches(string path)
        {
            Stopwatch sw = new();
            sw.Start();

            if (!File.Exists(path)) return null;

            using StreamReader sr = File.OpenText(path);
            int repeats = 0;
            int current = 0;
            int last = 0;

            Dictionary<string, int> result = new();

            while ((current = sr.Read()) > 0)
            {
                if (current == 13 || current == 10) continue;

                repeats = (last == current || last == current - 32 || last == current + 32) ? repeats + 1 : 1;
                last = current;

                if (repeats == _matchesInARow)
                {
                    string triplet = new string((char)last, repeats).ToLower();

                    if (!result.ContainsKey(triplet))
                    {
                        result.Add(triplet, 1);
                    }
                    else
                    {
                        result[triplet]++;
                    }

                    repeats = 0;
                    last = 0;
                }
            }

            sw.Stop();
            return new SearchResult(path, result, sw.ElapsedMilliseconds);
        }
    }
}
