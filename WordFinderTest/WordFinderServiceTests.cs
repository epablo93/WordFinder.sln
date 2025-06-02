using FluentValidation;
using WebApplication1.Services;
using Xunit;
using Assert = Xunit.Assert;


namespace WordFinderTest
{
    public class WordFinderServiceTests
    {
        private readonly WordFinderService _service;

        public WordFinderServiceTests()
        {
            _service = new WordFinderService(new MatrixInputValidator(), new WordStreamValidator());
        }

        [Fact]
        public void Find_ReturnsWordsFoundInMatrix_Top10()
        {
            var matrix = new List<string>
            {
                "coldw",
                "ionhi",
                "lwnoc",
                "dhsni",
                "chill"
            };
            var wordStream = new List<string> { "cold", "wind", "snow", "chill", "cold", "chill", "wind", "wind" };
            var result = _service.Find(matrix, wordStream);
            Assert.True(result.Success);
            Assert.Contains("cold", result.Words);
            Assert.Contains("wind", result.Words);
            Assert.Contains("chill", result.Words);
            Assert.DoesNotContain("snow", result.Words); // not present in matrix
            Assert.Equal(3, result.Words.Count());
        }

        [Fact]
        public void Find_ReturnsEmpty_WhenNoWordsFound()
        {
            var matrix = new List<string> { "abcde", "fghij", "klmno", "pqrst", "uvwxy" };
            var wordStream = new List<string> { "cold", "wind", "snow", "chill" };
            var result = _service.Find(matrix, wordStream);
            Assert.True(result.Success);
            Assert.Empty(result.Words);
        }

        [Fact]
        public void Find_HandlesEmptyMatrix_ReturnsError()
        {
            var matrix = new List<string>();
            var wordStream = new List<string> { "cold" };
            var result = _service.Find(matrix, wordStream);
            Assert.False(result.Success);
            Assert.Contains("Matrix cannot be empty.", result.Errors);
        }

        [Fact]
        public void Find_HandlesEmptyWordStream_ReturnsError()
        {
            var matrix = new List<string> { "coldw", "ionhi", "lwnoc", "dhsni", "chill" };
            var wordStream = new List<string>();
            var result = _service.Find(matrix, wordStream);
            Assert.False(result.Success);
            Assert.Contains("Wordstream cannot be null.", result.Errors);
        }
    }

}
