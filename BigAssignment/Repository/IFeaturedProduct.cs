using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigAssignment.Repository
{
    public interface IFeaturedProduct
    {
        Product Add(Product product);

        Product Update(Product product);

        Product Delete(String productID);

        Product GetProduct(String productID);

        IEnumerable<Product> GetAllProducts();

    }
}
