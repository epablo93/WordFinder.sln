using FluentValidation;

namespace WebApplication1.Services
{
    public class WordStreamValidator : AbstractValidator<IEnumerable<string>>
    {
        public WordStreamValidator()
        {
            RuleFor(ws => ws)
                .NotNull().WithMessage("Wordstream cannot be null.");
        }
    }
}
