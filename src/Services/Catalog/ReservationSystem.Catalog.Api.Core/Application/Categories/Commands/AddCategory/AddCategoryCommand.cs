using ReservationSystem.Catalog.Core.Application.Services;

namespace ReservationSystem.Catalog.Core.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommand : CommandBase
    {
        public string CategoryName { get; }

        public AddCategoryCommand(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
