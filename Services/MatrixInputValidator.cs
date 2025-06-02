using FluentValidation;

namespace WebApplication1.Services
{
    public class MatrixInputValidator : AbstractValidator<IEnumerable<string>>
    {
        public MatrixInputValidator()
        {
            RuleFor(matrix => matrix)
                .NotNull().WithMessage("Matrix cannot be null.")
                .Must(m => m.Any()).WithMessage("Matrix cannot be empty.");

            RuleFor(matrix => matrix)
                .Must(matrix =>
                {
                    if (matrix == null || !matrix.Any()) return true;
                    int cols = matrix.First().Length;
                    return matrix.All(row => row.Length == cols);
                })
                .WithMessage("All rows must have the same length.");

            RuleFor(matrix => matrix)
                .Must(matrix =>
                {
                    if (matrix == null) return true;
                    int rows = matrix.Count();
                    int cols = matrix.FirstOrDefault()?.Length ?? 0;
                    return rows <= 64 && cols <= 64 && cols > 0;
                })
                .WithMessage("Matrix size cannot exceed 64x64 and must have at least one column.");
        }
    }
}
