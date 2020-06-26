using System.Collections.Generic;
using bitirme.entity;

namespace bitirme.webui.Models
{
    public class BookDetailModel
    {
        public Book Book { get; set; }
        public List<Category> Categories { get; set; }
    }
}