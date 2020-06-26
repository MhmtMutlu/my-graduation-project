using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using bitirme.data.Abstract;
using bitirme.data.Concrete.EfCore;
using bitirme.business.Abstract;
using bitirme.business.Concrete;
using bitirme.webui.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using bitirme.webui.EmailService;
using Microsoft.Extensions.Configuration;

namespace bitirme.webui
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //! VERİ TABANI
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=nnyDb"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => { //! KAYIT OLMA İŞLEMLERİ
                // password
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // lockout
                options.Lockout.MaxFailedAccessAttempts = 5; // 5 kere yanlış girdikten sonra 2 dk bekler
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true; // 20 dk içinde işlem yapmazsa çıkarılır.
                options.ExpireTimeSpan = TimeSpan.FromDays(60); // 60 dakika işlem yapmazsa çıkarılır.
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Library.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });

            //! KULLANILACAK DOSYALARIN BELİRTİLMESİ
            services.AddScoped<IArticleRepository, EfCoreArticleRepository>();
            services.AddScoped<INoteRepository, EfCoreNoteRepository>();
            services.AddScoped<ILessonRepository, EfCoreLessonRepository>();
            services.AddScoped<IDepartmentRepository, EfCoreDepartmentRepository>();
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
            services.AddScoped<IBookRepository, EfCoreBookRepository>();
            
            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<INoteService, NoteManager>();
            services.AddScoped<ILessonService, LessonManager>();
            services.AddScoped<IDepartmentService, DepartmentManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IBookService, BookManager>();

            services.AddScoped<IEmailSender,SmtpEmailSender>(i=> 
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"])
                );

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
        {
            //! PDF, JPG GİBİ DOSYALAR VE BOOTSTRAP İÇİN HEDEF BELİRLEME
            app.UseStaticFiles(); // wwwroot
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                    RequestPath = "/modules"
            });
            //! GELİŞTİRME AŞAMASINDA OLDUĞUMUZUN BELİRLENMESİ
            if (env.IsDevelopment())
            {
                SeedDataBase.Seed();
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            
            //! SAYFA YÖNLENDİRMELERİ
            app.UseEndpoints(endpoints =>
            {
                //! Article
                endpoints.MapControllerRoute(
                    name: "adminarticlelist",
                    pattern: "admin/articlelist",
                    defaults: new {controller="Admin",action="ArticleList"}
                );

                endpoints.MapControllerRoute(
                    name: "adminarticlecreate",
                    pattern: "admin/articlecreate",
                    defaults: new {controller="Admin",action="ArticleCreate"}
                );

                endpoints.MapControllerRoute(
                    name: "adminarticledetails",
                    pattern: "admin/details/{id?}",
                    defaults: new {controller="Admin",action="Details"}
                );

                //! Lesson
                endpoints.MapControllerRoute(
                    name: "adminnotelist",
                    pattern: "lesson/notelist",
                    defaults: new {controller="Admin",action="NoteList"}
                );

                endpoints.MapControllerRoute(
                    name: "lessoncreatenote",
                    pattern: "lesson/notecreate",
                    defaults: new {controller="Lesson",action="NoteCreate"}
                );

                endpoints.MapControllerRoute(
                    name: "lessondetails",
                    pattern: "lesson/details/{id?}",
                    defaults: new {controller="Lesson",action="Details"}
                );

                endpoints.MapControllerRoute(
                    name: "lessonindex",
                    pattern: "lesson/index/{id?}",
                    defaults: new {controller="Lesson",action="Index"}
                );

                //! Department
                endpoints.MapControllerRoute(
                    name: "departmentsarticles",
                    pattern: "departments/articles/{id?}",
                    defaults: new {controller="Departments",action="Articles"}
                );

                endpoints.MapControllerRoute(
                    name: "departmentslessons",
                    pattern: "departments/lessons/{id?}",
                    defaults: new {controller="Departments",action="Lessons"}
                );

                endpoints.MapControllerRoute(
                    name: "departments",
                    pattern: "departments/index",
                    defaults: new {controller="Departments",action="Index"}
                );

                //! Account 
                endpoints.MapControllerRoute(
                    name: "accountmanage",
                    pattern: "account/manage/{id?}",
                    defaults: new {controller="Account",action="Manage"}
                );

                //! Roles
                endpoints.MapControllerRoute(
                    name: "adminuseredit",
                    pattern: "admin/user/{id?}",
                    defaults: new {controller="Admin",action="UserEdit"}
                );

                endpoints.MapControllerRoute(
                    name: "adminusers",
                    pattern: "admin/user/list",
                    defaults: new {controller="Admin",action="UserList"}
                );

                endpoints.MapControllerRoute(
                    name: "adminroles",
                    pattern: "admin/role/list",
                    defaults: new {controller="Admin",action="RoleList"}
                );

                endpoints.MapControllerRoute(
                    name: "adminrolecreate",
                    pattern: "admin/role/create",
                    defaults: new {controller="Admin",action="RoleCreate"}
                );

                endpoints.MapControllerRoute(
                    name: "adminroleedit",
                    pattern: "admin/role/{id?}",
                    defaults: new {controller="Admin",action="RoleEdit"}
                );

                //! Book
                endpoints.MapControllerRoute(
                    name: "adminbooks",
                    pattern: "admin/books",
                    defaults: new {controller="Admin",action="BookList"}
                );

                endpoints.MapControllerRoute(
                    name: "adminbookcreate",
                    pattern: "admin/books/create",
                    defaults: new {controller="Admin",action="BookCreate"}
                );

                endpoints.MapControllerRoute(
                    name: "adminbookedit",
                    pattern: "admin/books/{id?}",
                    defaults: new {controller="Admin",action="BookEdit"}
                );

                //! Category
                endpoints.MapControllerRoute(
                    name: "admincategories",
                    pattern: "admin/categories",
                    defaults: new {controller="Admin",action="CategoryList"}
                );

                endpoints.MapControllerRoute(
                    name: "admincategorycreate",
                    pattern: "admin/categories/create",
                    defaults: new {controller="Admin",action="CategoryCreate"}
                );

                endpoints.MapControllerRoute(
                    name: "admincategoryedit",
                    pattern: "admin/categories/{id?}",
                    defaults: new {controller="Admin",action="CategoryEdit"}
                );

                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "search",
                    defaults: new {controller="Library",action="Search"}
                );

                endpoints.MapControllerRoute(
                    name: "bookdetails",
                    pattern: "{url}",
                    defaults: new {controller="Library", action="Details"}
                );

                endpoints.MapControllerRoute(
                    name: "books",
                    pattern: "books/{category?}",
                    defaults: new {controller="Library", action="List"}
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        
            SeedIdentity.Seed(userManager, roleManager, configuration).Wait();
        }
    }
}
