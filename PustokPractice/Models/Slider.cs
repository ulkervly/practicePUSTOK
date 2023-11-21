using System.ComponentModel.DataAnnotations;

namespace PustokPractice.Models
{
    public class Slider
    {
        public int id {  get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        public string ImageUrL {  get; set; }
        public string RedirectUrl {  get; set; }
    }
}
