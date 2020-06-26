using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using bitirme.entity;

namespace bitirme.data.Concrete.EfCore
{
    public static class SeedDataBase
    {
        public static void Seed()
        {
            var context = new LibraryContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }
                if (context.Books.Count() == 0)
                {
                    context.Books.AddRange(Books);
                    context.AddRange(BookCategories);
                }
            }
            context.SaveChanges();

            var context1 = new DepartmentContext();
            if (context1.Database.GetPendingMigrations().Count() == 0)
            {
                if (context1.Departments.Count() == 0)
                {
                    context1.Departments.AddRange(Departments);
                }
                if (context1.Lessons.Count() == 0)
                {
                    context1.Lessons.AddRange(Lessons);
                    context1.AddRange(DepartmentLessons);
                }
                if (context1.Notes.Count() == 0)
                {
                    context1.Notes.AddRange(Notes);
                    context1.AddRange(LessonNotes);
                }
                if (context1.Articles.Count() == 0)
                {
                    context1.Articles.AddRange(Articles);
                    context1.AddRange(DepartmentArticles);
                }
            }
            context1.SaveChanges();
        }
        private static Category[] Categories = {
            new Category() { Name = "Kişisel Gelişim", Url = "kisisel-gelisim" },
            new Category() { Name = "Araştırma/Tarih", Url = "arastirma-tarih" },
            new Category() { Name = "Bilimsel/Mühendislik", Url = "bilimsel-mühendislik" },
            new Category() { Name = "Felsefe", Url = "felsefe" }
        };
        private static Book[] Books = {
            new Book() { Name = "Ted Gibi Konuş", Url = "tedgibikonus", Author = "Carmine Gallo", Stock = 10, ImageUrl = "1.jpg", Description = "Topluluk önünde rahat ve akıcı konuşmak, dinleyenleri ilgisi çekmek ve sunum taktiklerini içeren müthiş bir kitap.", IsApproved = true, IsHome = true},
            new Book() { Name = "Tongue Fu", Url = "tonguefu", Author = "Sam Horn", Stock = 15, ImageUrl = "2.jpg", Description = "Bu kitapla düzgün ve etkili iletişim kurmanın yolları öğreneceksiniz ve her zaman tartışmadan uzak kalacaksınız.", IsApproved = true, IsHome = true},
            new Book() { Name = "Beden Dili", Url = "bedendili", Author = "Joe Navarro", Stock = 0, ImageUrl = "3.jpg", Description = "Karşınızdakinin bedenine bakarak aklını okumak mümkün. İnsanları okumanın en hızlı yöntemi bu kitapta.", IsApproved = true, IsHome = false},
            new Book() { Name = "Bye Bye Türkçe", Url = "byebyeturkce", Author = "Oktay Sinanoğlu", Stock = 20, ImageUrl = "4.jpg", Description = "Dilimizin nasıl yozlaştırıldığı ve yabancı dilde eğitimin zararlarını bu kitabı okuyunca kavrayacaksınız.", IsApproved = true, IsHome = true},
            new Book() { Name = "Nutuk", Url = "nutuk", Author = "Mustafa Kemal Atatürk", Stock = 25, ImageUrl = "5.jpg", Description = "Dönemin şartlarını ve olayları birinci ağızdan öğrenin. İleri seviyelere ulaşabilmek için ders alınılması gereken kitap.", IsApproved = true, IsHome = true},
            new Book() { Name = "Gazi Mustafa Kemal Atatürk", Url = "gazimkemalataturk", Author = "İlber Ortaylı", Stock = 15, ImageUrl = "6.jpg", Description = "Günümüzün önde gelen tarihçisi İlber Ortaylı'nın ağzından Mustafa Kemal Atatürk.", IsApproved = true, IsHome = true},
            new Book() { Name = "Zamanın Kısa Tarihi", Url = "zamaninkisatarihi", Author = "Stephen Hawking", Stock = 10, ImageUrl = "7.jpg", Description = "Tekerlekli sandalyede oturmasına karşın Hawking'in zihni uzayın sonsuzluğunda her yere ulaşıyor ve evrenin gizemini açıklıyor.", IsApproved = true, IsHome = true},
            new Book() { Name = "Türlerin Kökeni", Url = "turlerinkokeni", Author = "Charles Darwin", Stock = 10, ImageUrl = "8.jpg", Description = "Darwinci kuramı yakından tanımak isteyenler için bire bir, bir kitap.", IsApproved = false, IsHome = true},
            new Book() { Name = "Bilimin Büyüsü", Url = "biliminbuyusu", Author = "Celal Şengör", Stock = 5, ImageUrl = "9.jpg", Description = "Ülkemizde adını sıkça duyduğumuz öncü bilim adamlarından Celal Şengör'ün bilimin önemi ve gerekliliği hakkındaki yorumları.", IsApproved = true, IsHome = true},
            new Book() { Name = "Var mısın ki Yok Olmaktan Korkuyorsun?", Url = "varmısınkiyokolmaktankorkuyorsun", Author = "Farabi", Stock = 15, ImageUrl = "10.jpg", Description = "Kendine sadece ilme adayan ünlü düşünür Farabi'nin felsefi kitabı.", IsApproved = true, IsHome = true},
            new Book() { Name = "Devlet", Url = "devlet", Author = "Platon", Stock = 10, ImageUrl = "11.jpg", Description = "Sokrates'in öğrencisi Platon'un düşlediği en iyi devleti anlattığı kitap.", IsApproved = true, IsHome = true},
            new Book() { Name = "Sokrates'in Savunması", Url = "sokratesinsavunmasi", Author = "Platon", Stock = 5, ImageUrl = "12.jpg", Description = "Platon ile Sokrates arasında geçen 4 diyalog üzerine yazılmış ve diyaloglarla felsefeyi yazıya aktarmış bir kitap.", IsApproved = true, IsHome = true},
            new Book() { Name = "İttihat ve Terakki Cemiyeti", Url = "ittihatveterakki", Author = "Kazım Karabekir", Stock = 15, ImageUrl = "13.jpg", Description = "1. Dünya Savaşı Dönemi Osmanlı'sının yönetiminde olan ittihatçıları birinci ağızdan gözlemleyin.", IsApproved = true, IsHome = true},
            new Book() { Name = "Enver", Url = "enver", Author = "Murat Bardakçı", Stock = 0, ImageUrl = "14.jpg", Description = "İttihatçı lider olan Enver Paşa'nın tamamı belgeleri dayalı hayatı, hareketleri ve içinde bulunduğu ortam.", IsApproved = true, IsHome = false},
            new Book() { Name = "Osmanlı Tarihinde Efsaneler ve Gerçekler", Url = "osmanlitarihinde", Author = "Halil İnalcık", Stock = 10, ImageUrl = "15.jpg", Description = "Türkiye Cumhuriyet'inin önemli tarihçisi İnalcık'ın araştırmaları ile Osmanlı tarihi gerçekleri.", IsApproved = true, IsHome = true},
            new Book() { Name = "Türklerin Altın Çağı", Url = "turklerinaltincagi", Author = "İlber Ortaylı", Stock = 5, ImageUrl = "16.jpg", Description = "İlber Ortaylı ile diyalog şeklinde Türklerin altın çağları.", IsApproved = true, IsHome = true},
            new Book() { Name = "Dördüncü Sanayi Devrimi", Url = "dorduncusanayi", Author = "Klaus Schwab", Stock = 5, ImageUrl = "17.jpg", Description = "Yapay zeka ve sanayi 4.0'ın gelişimi ve dünyadaki yeri.", IsApproved = true, IsHome = true},
            new Book() { Name = "Elektrik Enerjisi Üretimi İletimi ve Dağıtımı", Url = "elektrikenerjisi", Author = "Erdal Turgut", Stock = 10, ImageUrl = "18.jpg", Description = "Elektrik enerjisi üzerine detaylı ve öğretici bir kitap.", IsApproved = true, IsHome = true},
            new Book() { Name = "Elektrik Tesislerinde Arızalar", Url = "elektriktesislerindearizalar", Author = "Selahattin Küçük", Stock = 0, ImageUrl = "19.jpg", Description = "Elektrik tesisleri ve elektrik tesislerinde yaşanabilecek sorunları, örnekleriyle ele almış bilgilendirici bir kitap.", IsApproved = true, IsHome = false},
            new Book() { Name = "Makine Öğrenmesi", Url = "makineogrenmesi", Author = "Sinan Uğuz", Stock = 10, ImageUrl = "20.jpg", Description = "Makine öğrenmesini python programlama dili ile detaylı bir şekilde öğrenin.", IsApproved = true, IsHome = true}
        };
        private static BookCategory[] BookCategories = {
            new BookCategory() { Book = Books[0], Category = Categories[0] },
            new BookCategory() { Book = Books[1], Category = Categories[0] },
            new BookCategory() { Book = Books[2], Category = Categories[0] },
            new BookCategory() { Book = Books[3], Category = Categories[0] },
            new BookCategory() { Book = Books[3], Category = Categories[1] },
            new BookCategory() { Book = Books[4], Category = Categories[1] },
            new BookCategory() { Book = Books[5], Category = Categories[1] },
            new BookCategory() { Book = Books[6], Category = Categories[2] },
            new BookCategory() { Book = Books[7], Category = Categories[2] },
            new BookCategory() { Book = Books[8], Category = Categories[2] },
            new BookCategory() { Book = Books[9], Category = Categories[3] },
            new BookCategory() { Book = Books[10], Category = Categories[3] },
            new BookCategory() { Book = Books[11], Category = Categories[3] },
            new BookCategory() { Book = Books[12], Category = Categories[0] },
            new BookCategory() { Book = Books[13], Category = Categories[0] },
            new BookCategory() { Book = Books[14], Category = Categories[0] },
            new BookCategory() { Book = Books[15], Category = Categories[0] },
            new BookCategory() { Book = Books[16], Category = Categories[2] },
            new BookCategory() { Book = Books[17], Category = Categories[2] },
            new BookCategory() { Book = Books[18], Category = Categories[2] },
            new BookCategory() { Book = Books[19], Category = Categories[2] }
        };
        private static Department[] Departments = {
            new Department() { Name = "Elektrik-Elektronik Mühendisliği", Url = "eem", ImageUrl = "eem.jpg" },
            new Department() { Name = "Endüstri Mühendisliği", Url = "em", ImageUrl = "em.jpg" },
            new Department() { Name = "İnşaat Mühendisliği", Url = "im", ImageUrl = "im.jpg" },
            new Department() { Name = "Mimarlık", Url = "mimarlik", ImageUrl = "mimarlik.jpg" },
            new Department() { Name = "İç Mimarlık ve Çevre Tasarimi", Url = "icmimarlik", ImageUrl = "icmimarlik.jpg" },
            new Department() { Name = "İktisat", Url = "iktisat", ImageUrl = "iktisat.jpg" },
            new Department() { Name = "İşletme", Url = "isletme", ImageUrl = "isletme.jpg" },
            new Department() { Name = "Siyaset Bilimi ve Kamu Yönetimi", Url = "siyaset", ImageUrl = "siyaset.jpg" },
            new Department() { Name = "Beslenme ve Diyetetik", Url = "beslenme", ImageUrl = "beslenme.jpg" },
            new Department() { Name = "Hemşirelik", Url = "hemsirelik", ImageUrl = "hemsirelik.jpg" },
            new Department() { Name = "Fizyoterapi ve Rehabilitasyon", Url = "fizyoterapi", ImageUrl = "fizyoterapi.jpg" },
            new Department() { Name = "Psikoloji", Url = "psikoloji", ImageUrl = "psikoloji.jpg" }
        };

        private static Lesson[] Lessons = {
            new Lesson() { Name = "EEM422 Haberleşme Laboratuvarı", Url = "haberlesmelab" },
            new Lesson() { Name = "İSG412 İş Sağlığı ve Güvenliği 2", Url = "isgiki" },
            new Lesson() { Name = "EEM327 Elektrik Tesisleri", Url = "elektriktesisleri" },
            new Lesson() { Name = "EEM325 Haberleşme Mühendisliği Temelleri", Url = "haberlesmemühtemelleri" },
            new Lesson() { Name = "EEM319 İş Hukuku", Url = "ishukuku" }
        };

        private static DepartmentLesson[] DepartmentLessons = {
            new DepartmentLesson() { Department = Departments[0], Lesson = Lessons[0] },
            new DepartmentLesson() { Department = Departments[0], Lesson = Lessons[1] },
            new DepartmentLesson() { Department = Departments[0], Lesson = Lessons[2] },
            new DepartmentLesson() { Department = Departments[0], Lesson = Lessons[3] },
            new DepartmentLesson() { Department = Departments[0], Lesson = Lessons[4] }
        };

        //? Title To Url
        // private static string ChangeToUrl(string title)
        // {
        //     return title.Replace(" ","").ToLower();
        // }

        private static Note[] Notes = {
            new Note() { Title = "Haberleşme Laboratuvarı Deney Föyü", Url = "haberlesmedeneyfoyu", Author = "Mehmet Mutlu", Description = "Hocamızın hazırladığı deney föyleri ektedir.", DocUrl = "haberlesmelab.PDF" },
            new Note() { Title = "Haberleşme Laboratuvarı Deney Sonu Raporu", Url = "haberlesmedeneysonuraporu", Author = "Mehmet Mutlu", Description = "Deney sonu raporu olarak verilen ödev ektedir.", DocUrl = "deneysonuraporu.PDF" },
            new Note() { Title = "İş Sağlığı ve Güvenliği Son Ders Slaytı", Url = "isg2sondersslayt", Author = "Mehmet Mutlu", Description = "Son derste işlenen slayt ektedir.", DocUrl = "isg2sonders.PDF" }
        };

        private static LessonNote[] LessonNotes = {
            new LessonNote() { Lesson = Lessons[0], Note = Notes[0] },
            new LessonNote() { Lesson = Lessons[0], Note = Notes[1] },
            new LessonNote() { Lesson = Lessons[1], Note = Notes[2] }
        };

        private static Article[] Articles = {
            new Article() { Title = "3D Input Convolutional Neural Networks for P300 Signal Detection", Author = "Zeki Oralhan", Url = "zekioralhanarticle", DocUrl = "zekioralhanarticle" },
            new Article() { Title = "Simultaneous Optimization of Network Reconfiguration and DG Installation Using Heuristic Algorithms", Author = "Ahmet Doğan", Url = "ahmetdoganarticle", DocUrl = "ahmetdoganarticle" },
            new Article() { Title = "Heuristic Optimization of EV Charging Schedule Considering Battery Degradation Cost. Elektronika Ir Elektrotechnika", Author = "Ahmet Doğan", Url = "ahmetdoganarticle2", DocUrl = "ahmetdoganarticle2" }
        };

        private static DepartmentArticle[] DepartmentArticles = {
            new DepartmentArticle() { Department = Departments[0], Article = Articles[0] },
            new DepartmentArticle() { Department = Departments[0], Article = Articles[1] },
            new DepartmentArticle() { Department = Departments[0], Article = Articles[2] }
        };
    }
}