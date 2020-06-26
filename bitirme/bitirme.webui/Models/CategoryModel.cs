using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage="Kategori adı zorunludur!")]
        [StringLength(20,MinimumLength=5,ErrorMessage="Kategori için 5-20 arasında bir değer girilmelidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage="Kategori urlsi zorunludur!")]
        [StringLength(20,MinimumLength=5,ErrorMessage="Kategori için 5-20 arasında bir değer girilmelidir.")]
        public string Url { get; set; }
        public List<Book> Books { get; set; }
    }
}