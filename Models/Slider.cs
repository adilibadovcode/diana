using System.ComponentModel.DataAnnotations;

namespace DianaApp.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required,MaxLength(64),MinLength(3)]
        public string Title { get; set; }
        [Required,MaxLength(128),MinLength(3)]
        public string Text { get; set; }
        public bool? IsLeft { get; set; }
    }
}
