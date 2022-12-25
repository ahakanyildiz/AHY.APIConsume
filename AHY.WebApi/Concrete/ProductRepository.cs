using AHY.WebApi.Abstract;
using AHY.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace AHY.WebApi.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            return product !?? product;
           
        }

        public async Task<Product> Create(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public void Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Remove(product);
            _context.SaveChanges();
        }

        public async Task<bool> Update(Product product)
        {
            var unchangedEntity = await GetById(product.Id);
            if (unchangedEntity != null)
            {
                _context.Products.Entry(unchangedEntity).CurrentValues.SetValues(product);
                var ok = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

