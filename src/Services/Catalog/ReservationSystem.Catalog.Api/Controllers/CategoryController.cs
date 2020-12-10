using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Catalog.Api.Contracts.Category;
using ReservationSystem.Catalog.Core.Application.Categories;
using ReservationSystem.Catalog.Core.Application.Categories.Commands.AddCategory;

namespace ReservationSystem.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesCommandService _categoriesCommandService;
        private readonly ICategoriesQueryService _categoriesQueryService;

        public CategoryController(ICategoriesCommandService categoriesCommandService, ICategoriesQueryService categoriesQueryService)
        {
            _categoriesCommandService = categoriesCommandService;
            _categoriesQueryService = categoriesQueryService;
        }

        [HttpGet]
        [Route("{categoryId}", Name = nameof(GetCategory))]
        [ProducesResponseType(typeof(CategoryDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCategory([FromRoute] int categoryId, CancellationToken cancellationToken)
        {
            var categoryDetails = await _categoriesQueryService.CategoryDetails(categoryId, cancellationToken);

            return Ok(categoryDetails);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CategoryId), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCategory([FromBody] AddCategoryBody body, CancellationToken cancellationToken)
        {
            var addedCategoryId = await _categoriesCommandService.AddCategory(
                new AddCategoryCommand(body.CategoryName),
                cancellationToken);

            return CreatedAtRoute(nameof(GetCategory), addedCategoryId.Value, addedCategoryId);
        }
    }
}