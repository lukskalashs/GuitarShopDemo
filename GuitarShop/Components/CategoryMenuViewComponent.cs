using GuitarShop.Data;
using GuitarShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private IRepositoryWrapper _repo;
        public CategoryMenuViewComponent(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            var model = new CategoryListViewModel
            {
                Categories = _repo.Category.FindAll(),
                SelectedCategory = (string)RouteData?.Values["id"]
            };
            return View(model);
        }
    }
}