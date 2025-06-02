namespace WebApplication1.Models
{
    public class WordFinderRequest
    {
        public IEnumerable<string> Matrix { get; set; } = Array.Empty<string>();
        public IEnumerable<string> WordStream { get; set; } = Array.Empty<string>();
    }
}
