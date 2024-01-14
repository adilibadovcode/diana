using System.ComponentModel.DataAnnotations;

namespace DianaApp.ViewModels.SliderVM
{
    public class SliderUpdateVM
    {
        [Required]
        public string ImageUrl { get; set; }
        [Required, MaxLength(64), MinLength(3)]
        public string Title { get; set; }
        [Required, MaxLength(128), MinLength(3)]
        public string Text { get; set; }
        public sbyte Position { get; set; }
    }
}
