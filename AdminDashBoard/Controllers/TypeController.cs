using Microsoft.AspNetCore.Mvc;
using Store.G04.Core;
using Store.G04.Core.Entities;

namespace AdminDashBoard.Controllers
{
    public class TypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var types =await  _unitOfWork.CreateRepository<ProductType , int>().GetAllAsync();
            return View(types);
        }

        public async Task<IActionResult> Create(ProductType type) 
        {
            try
            {
                await _unitOfWork.CreateRepository<ProductType, int>().AddAsync(type);
                await _unitOfWork.CompleteAsync();
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				ModelState.AddModelError("Name", "Please Enter New Type Name");
				return View("Index", await _unitOfWork.CreateRepository<ProductType, int>().GetAllAsync());
			}
		}

        public async Task<IActionResult> Delete(int id)
        {
            var type = await _unitOfWork.CreateRepository<ProductType, int>().GetAsync(id);
            _unitOfWork.CreateRepository<ProductType,int>().Delete(type);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
    }
}
