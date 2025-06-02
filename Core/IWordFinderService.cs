namespace WebApplication1.Core
{
    public interface IWordFinderService
    {
        WordFinderResult Find(IEnumerable<string> matrix, IEnumerable<string> wordstream);
    }
}
