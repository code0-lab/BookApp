using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class ProductBook
    {
        [Display(Name ="Product Id")]
        public int BookId { get; set; }
        [Display(Name ="Book Name")]
        [StringLength(100)]
        [Required] //[Required(ErrorMesage = Buraya istenen hata mesajı yazılabilir.)]
        public string? BookName { get; set; }
        [Display(Name ="Book Description (max 250)")]
        [StringLength(250)]
        public string? Description { get; set; } = string.Empty;
        [Display(Name ="PageCount (min 10 page)")]
        [Range(10,9999999)]
        public int? PageCount { get; set; }
        [Display(Name ="Book Image Id")]
        public string Image { get; set; } = string.Empty;
        [Display(Name ="Activity status")]
        public bool IsActive { get; set; }
        [Display(Name ="Book Category")]
        [Required]
        public int? CategoryId { get; set; }
    }
}
