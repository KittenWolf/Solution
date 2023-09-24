using System.Collections.Concurrent;
using System.Diagnostics;

namespace Solution
{
    internal class TripletAnalyzer
    {
        private readonly int _matchesInARow = 3;
        private readonly decimal _optimalChunkSize = 25000;

        public SearchResult GetResult(string path, bool parallelSearching = false)
        {
            Stopwatch sw = new();
            sw.Start();

            string content = File.ReadAllText(path).Replace("\r\n", " ").ToLower();
            string[] words = (from word in content.Split(' ') where word.Length >= 3 select word).ToArray();

            var result = parallelSearching 
                ? ParallelSearch(words) 
                : Search(words);

            sw.Stop();

            return new SearchResult(path, result, sw.ElapsedMilliseconds);
        }

        private IDictionary<string, int> Search(string[] content)
        {
            var result = new Dictionary<string, int>();

            CheckWords(content, result);

            return result;
        }
        private IDictionary<string, int> ParallelSearch(string[] content)
        {            
            var chunks = GetOptimalWordChunks(content);

            var tasks = new List<Task>();
            var result = new ConcurrentDictionary<string, int>();

            foreach (var wordChunk in chunks)
            {
                tasks.Add(Task.Run(() => CheckWords(wordChunk, result)));
            }

            Task.WaitAll(tasks.ToArray());

            return result;
        }

        private List<string[]> GetOptimalWordChunks(string[] words)
        {
            decimal chunks = Math.Ceiling(words.Length / _optimalChunkSize);
            int wordsCount = (int)Math.Ceiling(words.Length / chunks);
             
            return words.Chunk(wordsCount).ToList();
        }
        private void CheckWords(string[] words, IDictionary<string, int> result)
        {
            foreach (var word in words)
            {
                int last = 0;
                int repeats = 0;

                foreach (var @char in word)
                {
                    repeats = (last == @char) ? repeats + 1 : 1;
                    last = @char;

                    if (repeats == _matchesInARow)
                    {
                        string triplet = new((char)last, repeats);

                        if (!result.ContainsKey(triplet))
                        {
                            //ArgumentException. Idk why...
                            result.TryAdd(triplet, 1);
                        }
                        else
                        {
                            result[triplet]++;
                        }

                        repeats = 0;
                        last = 0;
                    }
                }
            }
        }
    }
}
