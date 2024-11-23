using Microsoft.AspNetCore.Mvc;
using Store.G04.Core;
using Store.G04.Core.Entities;

namespace AdminDashBoard.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var Brands =await _unitOfWork.CreateRepository<ProductBrand,int>().GetAllAsync();


            return View(Brands);
        }
        public async Task<IActionResult> Create(ProductBrand brand) 
        {
            try
            {
                await _unitOfWork.CreateRepository<ProductBrand, int>().AddAsync(brand);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Name", "Please Enter New Brand Name");
                return View("Index",await _unitOfWork.CreateRepository<ProductBrand, int>().GetAllAsync());
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.CreateRepository<ProductBrand, int>().GetAsync(id);

            _unitOfWork.CreateRepository<ProductBrand,int>().Delete(brand);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }
    }
}
