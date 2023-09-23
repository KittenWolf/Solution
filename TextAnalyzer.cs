namespace Solution
{
    internal class TextAnalyzer
    {
        private List<Task<SearchResult>> _tasks = new();
        private readonly TripletAnalyzer _analyzer;

        public TextAnalyzer(TripletAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }

        public void GetTriplets(string path)
        {
            var result = _analyzer.FindAllMatches(path);

            result?.PrintResults(10);
        }

        public void ParallelGetTriplets(params string[] pathes)
        {
            foreach (var path in pathes)
            {
                var task = Task.Run(() => _analyzer.FindAllMatches(path));
                _tasks.Add(task);
            }

            Task.WaitAll(_tasks.ToArray());

            foreach (var task in _tasks)
            {
               task.Result?.PrintResults(10);
            }
        }
    }
}
