
namespace TaskManager.Models.Entities
{
    public class CategoryPage : Page<Category>
    {
        public CategoryPage(List<Category> data, int page, int size, int totalItems, int totalPage) : base(data, page, size, totalItems, totalPage)
        {
        }
    }
}
