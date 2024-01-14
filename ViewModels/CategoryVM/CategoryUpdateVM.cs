using System.ComponentModel.DataAnnotations;

namespace DianaApp.ViewModels.CategoryVM
{
    public class CategoryUpdateVM
    {
        public int Id { get; set; }
        [MaxLength(16),Required]
        public string Name { get; set; }
    }
}
