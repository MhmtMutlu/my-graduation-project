using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using bitirme.business.Abstract;

namespace bitirme.webui.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["category"] != null)
            {
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            }
            
            return View(_categoryService.GetAll());
        }
    }
}