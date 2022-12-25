using AHY.WebApi.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AHY.WebApi.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductContext _context;

        public CategoriesController(ProductContext context)
        {
            _context = context;
        }


        [HttpGet("{id}/products")]
        public IActionResult GetWithProducts(int id)
        {
            var data = _context.Categories.Include(x => x.Products).SingleOrDefault(x => x.Id == id);
            return data != null ? Ok(data) : NotFound(id);
        }
    }

}