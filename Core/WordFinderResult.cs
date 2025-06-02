namespace WebApplication1.Core
{
    public class WordFinderResult
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Words { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
