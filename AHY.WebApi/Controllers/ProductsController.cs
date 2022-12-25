using AHY.WebApi.Abstract;
using AHY.WebApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AHY.WebApi.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProducts() => Ok(await _productRepository.GetAllAsync());

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var data = await _productRepository.GetById(id);

            if (data == null)
                return NotFound(id);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (product != null)
            {
                await _productRepository.Create(product);
                return Created("", product);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Remove(id);
                return NoContent();
            }
            return NotFound(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var status = await _productRepository.Update(product);
            return status ? NoContent() : NotFound(product.Id);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile formFile)
        {
            if (formFile != null)
            {
                var extension = Path.GetExtension(formFile.FileName);
                var newName = Guid.NewGuid().ToString() + "." + formFile.Name + extension;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", newName);

                using (var stream = System.IO.File.Create(path))
                {
                    await formFile.CopyToAsync(stream);
                }
                return Ok(formFile.Name + " created.");
            }
            return BadRequest();
        }


    }
}
