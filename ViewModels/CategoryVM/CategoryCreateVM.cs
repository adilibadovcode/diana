using System.ComponentModel.DataAnnotations;

namespace DianaApp.ViewModels.CategoryVM
{
    public class CategoryCreateVM
    {
        [MaxLength(16),Required]
        public string Name { get; set; }
    }
}
