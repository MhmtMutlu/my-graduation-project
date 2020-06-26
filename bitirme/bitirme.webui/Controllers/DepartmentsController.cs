using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using bitirme.webui.EmailService;
using bitirme.webui.Extensions;
using bitirme.webui.Identity;
using bitirme.webui.Models;
using bitirme.business.Abstract;

namespace bitirme.webui.Controllers
{
    public class DepartmentsController:Controller
    {
        private IDepartmentService _departmentService;
        private ILessonService _lessonService;
        private IArticleService _articleService;
        public DepartmentsController(IDepartmentService departmentService, ILessonService lessonService, IArticleService articleService)
        {
            _departmentService = departmentService;
            _lessonService = lessonService;
            _articleService = articleService;
        }
        public IActionResult Index()
        {
            var departmentViewModel = new DepartmentListViewModel()
            {
                Departments = _departmentService.GetAll()
            };
            return View(departmentViewModel);
        }
        
        public IActionResult Lessons(int id)
        {
            var entity = _departmentService.GetByIdWithLesson(id);

            var model = new DepartmentModel()
            {
                DepartmentId = entity.DepartmentId,
                Name = entity.Name,
                Url = entity.Url,
                ImageUrl = entity.ImageUrl,
                Lessons = entity.DepartmentLessons.Select(i => i.Lesson).ToList()
            };

            return View(model);
        }

        public IActionResult Articles(int id)
        {
            var entity = _departmentService.GetByIdWithArticles(id);

            var model = new DepartmentModel()
            {
                DepartmentId = entity.DepartmentId,
                Name = entity.Name,
                Url = entity.Url,
                ImageUrl = entity.ImageUrl,
                Articles = entity.DepartmentArticle.Select(i => i.Article).ToList()
            };

            return View(model);
        }
    }
}