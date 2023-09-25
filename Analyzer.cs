using System.Collections.Concurrent;
using System.Diagnostics;

namespace Solution
{
    internal class Analyzer
    {
        private readonly decimal _optimalChunkSize = 10000;

        public SearchResult GetResult(string path, bool parallelSearching = false)
        {
            Stopwatch sw = new();
            sw.Start();

            string content = File.ReadAllText(path);
            content = new(content.Where(c => !char.IsPunctuation(c)).ToArray());
            string[] words = (from word in content.ToLower().Split(' ') where word.Length == 3 select word).ToArray();

            var result = parallelSearching 
                ? ParallelSearch(words) 
                : Search(words);

            sw.Stop();

            return new SearchResult(path, result, sw.ElapsedMilliseconds);
        }

        private IDictionary<string, int> Search(string[] content)
        {
            var result = new Dictionary<string, int>();

            CountWords(content, result);

            return result;
        }
        private IDictionary<string, int> ParallelSearch(string[] content)
        {            
            var chunks = GetOptimalWordChunks(content);

            var tasks = new List<Task>();
            var result = new ConcurrentDictionary<string, int>();

            foreach (var wordChunk in chunks)
            {
                tasks.Add(Task.Run(() => CountWords(wordChunk, result)));
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
        private void CountWords(string[] words, Dictionary<string, int> result)
        {
            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                {
                    result.Add(word, 1);
                }
                else
                {
                    result[word]++;
                }
            }
        }
        private void CountWords(string[] words, ConcurrentDictionary<string, int> result)
        {
            foreach (var word in words)
            {
                result.AddOrUpdate(word, 1, (key, value) => value + 1);                
            }
        }
    }
}
