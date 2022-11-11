using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var catagoryFromDbSingle = _db.Categories.SingleOrDefault();

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var catagoryFromDbSingle = _db.Categories.SingleOrDefault();

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfuly";
            return RedirectToAction("Index");
        }
    }
}
