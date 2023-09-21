namespace Solution
{
    internal class TripletAnalyzer
    {
        private readonly string _path;
        private readonly int _matchesInARow = 3;
        private Dictionary<string, int> _result = new();

        public TripletAnalyzer(string path)
        {
            _path = path;
        }
        public TripletAnalyzer(string path, int matchesInARow) : this(path)
        {
            _matchesInARow = matchesInARow;
        }

        public void FindAllMatches()
        {
            if (!File.Exists(_path)) return;

            using StreamReader sr = File.OpenText(_path);
            int repeats = 0;
            int current = 0;
            int last = 0;

            while ((current = sr.Read()) > 0)
            {
                if (current == 13 || current == 10) continue;

                repeats = (last == current) ? repeats + 1 : 1;
                last = current;

                if (repeats == _matchesInARow)
                {
                    string triplet = new((char)last, repeats);

                    if (!_result.ContainsKey(triplet))
                    {
                        _result.Add(triplet, 1);
                    }
                    else
                    {
                        _result[triplet]++;
                    }

                    repeats = 0;
                    last = 0;
                }
            }
        }
        public void SortMatches(bool descendingOrder = true)
        {
            var sortedMatches = descendingOrder
                ? _result.OrderByDescending(x => x.Value)
                : _result.OrderBy(x => x.Value);

            _result = sortedMatches.ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> TakeMatches()
        {
            return _result;
        }
        public Dictionary<string, int> TakeMatches(int number)
        {
            var matches = _result.Take(number);

            return matches.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
