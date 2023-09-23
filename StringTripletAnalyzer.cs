using System.Diagnostics;

namespace Solution
{
    internal class StringTripletAnalyzer : TripletAnalyzer
    {
        public override SearchResult? FindAllMatches(string path)
        {
            Stopwatch sw = new();
            sw.Start();

            if (!File.Exists(path)) return null;

            string content = File.ReadAllText(path);
            content = content.Replace("\r\n", "").ToLower();

            int last = 0;
            int repeats = 0;

            Dictionary<string, int> result = new();

            foreach (var @char in content)
            {
                repeats = (last == @char) ? repeats + 1 : 1;
                last = @char;

                if (repeats == _matchesInARow)
                {
                    string triplet = new((char)last, repeats);

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
