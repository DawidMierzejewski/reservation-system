using FluentValidation;

namespace ReservationSystem.Catalog.Core.Application.Categories.Commands.AddCategory
{
    public class Validator : AbstractValidator<AddCategoryCommand>
    {
        public Validator()
        {
            RuleFor(p => p.CategoryName).NotEmpty().MaximumLength(4);
        }
    }
}
