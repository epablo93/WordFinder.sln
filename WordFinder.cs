using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace WebApplication1
{
    /// <summary>
    /// WordFinder efficiently searches for words in a character matrix horizontally and vertically.
    /// </summary>
    public class WordFinder
    {
        private readonly string[] _matrix;
        private readonly int _rows;
        private readonly int _cols;
        private readonly string[] _verticals;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix.ToArray();
            _rows = _matrix.Length;
            _cols = _matrix[0].Length;
            _verticals = new string[_cols];
            for (int col = 0; col < _cols; col++)
            {
                var vertical = new char[_rows];
                for (int row = 0; row < _rows; row++)
                    vertical[row] = _matrix[row][col];
                _verticals[col] = new string(vertical);
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordCounts = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var uniqueWords = new HashSet<string>(wordstream, StringComparer.OrdinalIgnoreCase);
            uniqueWords.AsParallel().ForAll(word =>
            {
                if (string.IsNullOrEmpty(word) || (word.Length > _cols && word.Length > _rows))
                    return;
                if (ExistsInMatrix(word))
                {
                    wordCounts[word] = wordstream.Count(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
                }
            });
            return wordCounts
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Take(10)
                .Select(kvp => kvp.Key)
                .ToArray();
        }

        private bool ExistsInMatrix(string word)
        {
            foreach (var row in _matrix)
                if (row.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            foreach (var col in _verticals)
                if (col.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}
