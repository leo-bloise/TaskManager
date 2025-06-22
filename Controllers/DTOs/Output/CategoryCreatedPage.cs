using TaskManager.Models.Entities;

namespace TaskManager.Controllers.DTOs.Output
{
    public class CategoryCreatedPage : Page<CategoryCreatedResponse>
    {
        public CategoryCreatedPage(List<CategoryCreatedResponse> data, int page, int size, int totalItems, int totalPage) : base(data, page, size, totalItems, totalPage)
        {
        }

        public static CategoryCreatedPage Adapt(Page<Category> page)
        {
            return new CategoryCreatedPage(
                page.Data.Select(category => CategoryCreatedResponse.Adapt(category)).ToList(),
                page.PageNumber,
                page.PageSize,
                page.TotalItems,
                page.TotalPages
            );
        }
    }
}
