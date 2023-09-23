namespace Solution
{    
    internal abstract class TripletAnalyzer
    {
        protected readonly int _matchesInARow = 3;

        public abstract SearchResult? FindAllMatches(string path);
    }
}
