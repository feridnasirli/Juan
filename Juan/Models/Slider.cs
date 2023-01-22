using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juan.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:30)]
        public string Title1 { get; set; }
        [StringLength(maximumLength: 30)]
        public string Title2 { get; set; }
        public string RedirectUrl { get; set; }
        [StringLength(maximumLength: 10)]
        public string RedirectUrlText { get; set; }
        public  string Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
