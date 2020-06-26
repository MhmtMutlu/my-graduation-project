using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using bitirme.business.Abstract;
using bitirme.entity;
using bitirme.webui.Extensions;
using bitirme.webui.Identity;
using bitirme.webui.Models;

namespace bitirme.webui.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController:Controller
    {
        private IBookService _bookService;
        private IArticleService _articleService;
        private INoteService _noteService;
        private ICategoryService _categoryService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public AdminController(IBookService bookService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, INoteService noteService, IArticleService articleService)
        {
            _bookService = bookService;
            _articleService = articleService;
            _noteService = noteService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i=>i.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailModel(){
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/admin/user/list");
        }

        public IActionResult ArticleDetails(int articleId)
        {
            var entity = _articleService.GetArticleDetails(articleId);

            var model = new ArticleModel()
            {
                ArticleId = entity.ArticleId,
                Title = entity.Title,
                Url = entity.Url,
                Author = entity.Author,
                DocUrl = entity.DocUrl
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ArticleCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ArticleCreate(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Article()
                {
                    Author = model.Author,
                    Title = model.Title,
                    Url = model.Url,
                    DocUrl = model.DocUrl
                };

                if (_articleService.Create(entity))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Kayıt Eklendi!",
                        Message = "Kayıt başarılı bir şekilde eklendi.",
                        AlertType = "success"
                    });
                    return RedirectToAction("ArticleCreate");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata Oluştu!",
                    Message = _articleService.ErrorMessage,
                    AlertType = "danger"
                });
                return View(model);
            }
            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailModel model,string[] selectedRoles)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user!=null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if(result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles?? new string[]{};
                        await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("/admin/user/list");
                    }
                }
                return Redirect("/admin/user/list");
            }

            return View(model);

        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)?members:nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] {})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in model.IdsToDelete ?? new string[] {})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/admin/role/" + model.RoleId);
        }
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View();
        }
        public IActionResult BookList()
        {
            return View(new BookListViewModel()
            {
                Books = _bookService.GetAll()
            });
        }

        public IActionResult NoteList()
        {
            return View(new NoteListViewModel()
            {
                Notes = _noteService.GetAll()
            });
        }
        public IActionResult ArticleList()
        {
            return View(new ArticleListViewModel()
            {
                Articles = _articleService.GetAll()
            });
        }
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult BookCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookCreate(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Book()
                {
                    Name = model.Name,
                    Url = model.Url,
                    Stock = model.Stock,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };

                if (_bookService.Create(entity))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Kayıt Eklendi!",
                        Message = "Kayıt başarılı bir şekilde eklendi.",
                        AlertType = "success"
                    });
                    return RedirectToAction("BookList");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata Oluştu!",
                    Message = _bookService.ErrorMessage,
                    AlertType = "danger"
                });
                return View(model);
            }
            return View(model);
            
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
                _categoryService.Create(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kategori Eklendi!",
                    Message = $"{entity.Name} isimli kategori başarıyla eklendi.",
                    AlertType = "success"
                });

                return RedirectToAction("CategoryList");  
            }
            return View(model);
            
        }

        [HttpGet]
        public IActionResult BookEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _bookService.GetByIdWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new BookModel()
            {
                BookId = entity.BookId,
                Name = entity.Name,
                Url = entity.Url,
                Stock = entity.Stock,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.BookCategories.Select(i => i.Category).ToList()
            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookEdit(BookModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _bookService.GetById(model.BookId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Stock = model.Stock;
                entity.Url = model.Url;
                entity.Description = model.Description;
                entity.IsHome = model.IsHome;
                entity.IsApproved = model.IsApproved;

                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _bookService.Update(entity, categoryIds);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kayıt Güncellendi!",
                    Message = "Kayıt başarılı bir şekilde güncellendi.",
                    AlertType = "success"
                });

                return RedirectToAction("BookList");
            }

            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
            
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithBooks((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Books = entity.BookCategories.Select(p => p.Book).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;
                _categoryService.Update(entity);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Kategori Güncellendi!",
                    Message = $"{entity.Name} isimli kategori güncellendi.",
                    AlertType = "success"
                });
                return RedirectToAction("CategoryList");
            }
            return View(model);
           
        }

        public IActionResult DeleteNote(int noteId)
        {
            var entity = _noteService.GetById(noteId);
            if (entity != null)
            {
                _noteService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Not Silindi!",
                Message = $"{entity.Title} isimli not silindi.",
                AlertType = "danger"
            });

            return RedirectToAction("NoteList");
        }

        public IActionResult DeleteArticle(int articleId)
        {
            var entity = _articleService.GetById(articleId);
            if (entity != null)
            {
                _articleService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Makale Silindi!",
                Message = $"{entity.Title} isimli makale silindi.",
                AlertType = "danger"
            });

            return RedirectToAction("ArticleList");
        }
        public IActionResult DeleteBook(int bookId)
        {
            var entity = _bookService.GetById(bookId);
            if (entity != null)
            {
                _bookService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Kitap Silindi!",
                Message = $"{entity.Name} isimli kitap silindi.",
                AlertType = "danger"
            });

            return RedirectToAction("BookList");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            TempData.Put("message", new AlertMessage()
            {
                Title = "Kategori Silindi!",
                Message = $"{entity.Name} isimli kategori silindi.",
                AlertType = "danger"
            });

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId) // buna ihtiyacım olmayabilir.
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }

    }
}