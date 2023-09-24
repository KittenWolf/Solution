namespace Solution
{
    internal class TextAnalyzer
    {
        private readonly TripletAnalyzer _analyzer = new();

        public void GetTriplets(string path, bool parallelSearching = false)
        {
            if (!File.Exists(path)) return;

            var result = _analyzer.GetResult(path, parallelSearching);

            result.PrintResults(10);
        }

        public void ParallelGetTriplets(string[] pathes, bool parallelSearching = false)
        {
            var tasks = new List<Task<SearchResult>>();

            foreach (var path in pathes)
            {
                if (!File.Exists(path)) continue;

                var task = Task.Run(() => _analyzer.GetResult(path, parallelSearching));

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
               task.Result.PrintResults(10);
            }
        }
    }
}
