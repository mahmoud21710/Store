using AdminDashBoard.Helper;
using AdminDashBoard.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core;
using Store.G04.Core.Entities;


namespace AdminDashBoard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var product = await _unitOfWork.CreateRepository<Product, int>().GetAllAsync();

            var mappedproduct = _mapper.Map<IEnumerable<ProductViewModel>>(product);

            return View(mappedproduct);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                if (model.Image != null)
                {
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                else 
                {
                    model.PictureUrl = "images/products/th";
                }
                var mappedProduct = _mapper.Map<Product>(model);
                await _unitOfWork.CreateRepository<Product,int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id) 
        {
            var product =await _unitOfWork.CreateRepository<Product,int>().GetAsync(id);

            var mappedProduct = _mapper.Map<ProductViewModel>(product);

            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                if (model.Image != null) 
                {
                    PictureSettings.DeleteFile(model.PictureUrl, "products");
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
            }
            var product =_mapper.Map<Product>(model);
            _unitOfWork.CreateRepository<Product,int>().Update(product); 
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0) 
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.CreateRepository<Product, int>().GetAsync(id);

            var mappedProduct = _mapper.Map<ProductViewModel>(product);

            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel model)
        {
            try
            {
                var product = await _unitOfWork.CreateRepository<Product, int>().GetAsync(model.Id);
                if (product.PictureUrl != null)
                {
                    PictureSettings.DeleteFile(product.PictureUrl, "products");
                }
                _unitOfWork.CreateRepository<Product, int>().Delete(product);

                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ) 
            {
                return View(model);
            }
        }

    }
}
