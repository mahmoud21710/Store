using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Error;
using Store.G04.Ropository.Data.Contexts;

namespace Store.G04.APIs.Controllers
{
   
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")] // Get : api / buggey / notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);

            //if(brand is  null) return NotFound(new {Message = "Brand with Id : 100 is Not Found ",StatusCode =StatusCodes.Status404NotFound});
            if(brand is  null) return NotFound(new ApiErrorResponse(404));

            return Ok(brand);
        }

        [HttpGet("servererror")] // Get : api / buggey / servererror
        public async  Task<IActionResult> GetServerRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandtostring = brand.ToString(); // will throw exception (nullrefernce)

            return Ok(brandtostring);
        }

        [HttpGet("badrequest")] // Get : api / buggey / badrequest
        public IActionResult GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("badrequest/{id}")] // Get : api / buggey / bad
        public IActionResult GetBadRequestError(int id) //validation error
        {
            return Ok();
        }

        [HttpGet("unauthorized")] // Get : api / buggey / unauthorized
        public IActionResult GetUnAuthorizedError() //validation error
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
