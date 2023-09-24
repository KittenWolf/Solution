namespace Solution
{
    internal class SearchResult
    {
        private readonly long _procesingTime = 0;
        private readonly string _path = "Undefiend path";

        private IDictionary<string, int> _result;

        public SearchResult(string path, IDictionary<string, int> result, long time)
        {
            _path = path;
            _result = result;
            _procesingTime = time;
        }

        public void SortMatches(bool descendingOrder = true)
        {
            var sortedMatches = descendingOrder
                ? _result.OrderByDescending(x => x.Value)
                : _result.OrderBy(x => x.Value);

            _result = sortedMatches.ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> TakeMatches(int count)
        {
            return _result.Take(count).ToDictionary(x => x.Key, x => x.Value);
        }

        public void PrintResults(int count, bool descendingOrder = true)
        {
            Console.WriteLine("\n{0}", _path);

            SortMatches(descendingOrder);
            var matches = TakeMatches(count);
            int i = 0;

            foreach (var match in matches)
            {
                i++;
                Console.WriteLine("{0}) {1} -> {2}", i, match.Key, match.Value);
            }

            Console.WriteLine("RunTime: {0}ms", _procesingTime);
        }
    }
}
