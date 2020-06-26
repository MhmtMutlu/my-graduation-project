using System.Linq;
using bitirme.business.Abstract;
using bitirme.entity;
using bitirme.webui.Extensions;
using bitirme.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace bitirme.webui.Controllers
{
    public class LessonController:Controller
    {
        private ILessonService _lessonService;
        private INoteService _noteService;
        public LessonController(ILessonService lessonService, INoteService noteService)
        {
            _lessonService = lessonService;
            _noteService = noteService;
        }
        
        public IActionResult Index(int id)
        {
            var entity = _lessonService.GetByIdWitNotes(id);

            var model = new LessonModel()
            {
                LessonId = entity.LessonId,
                Name = entity.Name,
                Url = entity.Url,
                Notes = entity.LessonNotes.Select(i => i.Note).ToList()
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var entity = _noteService.GetNoteDetails(id);

            var model = new NoteModel()
            {
                NoteId = entity.NoteId,
                Author = entity.Author,
                Url = entity.Url,
                Title = entity.Title,
                Description = entity.Description,
                DocUrl = entity.DocUrl
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult NoteCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NoteCreate(NoteModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Note()
                {
                    Author = model.Author,
                    Title = model.Title,
                    Url = model.Url,
                    Description = model.Description,
                    DocUrl = model.DocUrl
                };

                if (_noteService.Create(entity))
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Kayıt Eklendi!",
                        Message = "Kayıt başarılı bir şekilde eklendi.",
                        AlertType = "success"
                    });
                    return RedirectToAction("Index");
                }
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata Oluştu!",
                    Message = _noteService.ErrorMessage,
                    AlertType = "danger"
                });
                return View(model);
            }
            return View(model);
            
        }
    }
}