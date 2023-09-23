namespace Solution
{
    internal class Program
    {
        private static void Main()
        {
            string[] filePathes =
            {
                @"C:\Users\Fonar\OneDrive\Рабочий стол\texts\book.txt",
                @"C:\Users\Fonar\OneDrive\Рабочий стол\texts\text3.txt",
                @"C:\Users\Fonar\OneDrive\Рабочий стол\texts\text5.txt"
            };

            TextAnalyzer ta = new(new StringTripletAnalyzer());

            ta.ParallelGetTriplets(filePathes);
        }
    }
}