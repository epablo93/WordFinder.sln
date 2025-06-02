using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using WebApplication1.Core;

namespace WebApplication1.Services
{
    public class WordFinderService : IWordFinderService
    {
        private readonly IValidator<IEnumerable<string>> _matrixValidator;
        private readonly IValidator<IEnumerable<string>> _wordStreamValidator;

        public WordFinderService(IValidator<IEnumerable<string>> matrixValidator, IValidator<IEnumerable<string>> wordStreamValidator)
        {
            _matrixValidator = matrixValidator;
            _wordStreamValidator = wordStreamValidator;
        }

        public WordFinderResult Find(IEnumerable<string> matrix, IEnumerable<string> wordstream)
        {
            var matrixValidation = _matrixValidator.Validate(matrix);
            var wordStreamValidation = _wordStreamValidator.Validate(wordstream);
            var errors = matrixValidation.Errors.Concat(wordStreamValidation.Errors).Select(e => e.ErrorMessage).ToList();
            if (errors.Any())
            {
                return new WordFinderResult { Success = false, Errors = errors };
            }
            try
            {
                var finder = new WebApplication1.WordFinder(matrix.ToArray());
                var found = finder.Find(wordstream);
                return new WordFinderResult { Success = true, Words = found };
            }
            catch (Exception ex)
            {
                return new WordFinderResult { Success = false, Errors = new[] { "Unexpected error: " + ex.Message } };
            }
        }
    }
}
