using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class ProductFactory
    {
        public static ProductEntity Create(ProductModel model)
        {
            return new ProductEntity
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price
            };
        }

        public static ProductModel Create(ProductEntity entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Price = entity.Price
            };
        }
    }
}
