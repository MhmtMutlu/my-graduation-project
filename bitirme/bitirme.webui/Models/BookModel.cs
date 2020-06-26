using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

        [Display(Name="Name", Prompt="Kitap ismi giriniz...")]
        [Required(ErrorMessage="İsim zorunlu bir alan!")]
        [StringLength(50, MinimumLength=3,ErrorMessage="Ürün ismi 5-50 karakter aralığında olmalıdır.")]
        public string Name { get; set; }

        [Display(Prompt="Kitap için url giriniz...")]
        [Required(ErrorMessage="Kitap urlsi zorunlu bir alan!")]
        public string Url { get; set; }

        [Display(Prompt="Stok sayısı giriniz...")]
        [Required(ErrorMessage="Stok zorunlu bir alan!")]
        [Range(1, 100000, ErrorMessage="Stok 1-1000 arasında olmalıdır.")]
        public int Stock { get; set; } 

        [Display(Prompt="kitap için bir tanım giriniz...")]
        [Required(ErrorMessage="Tanım zorunlu bir alan!")]
        [StringLength(600, MinimumLength=3,ErrorMessage="Ürün tanımı 3-600 karakter aralığında olmalıdır.")]
        public string Description { get; set; }

        [Display(Prompt="Kitap resmi urlsi giriniz...")]
        [Required(ErrorMessage="Resim urlsi zorunlu bir alan!")]
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}