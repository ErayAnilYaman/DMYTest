using DMYTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProductService
    {
        void Update(Product product);
        void Delete(Product product);

        void Add(Product product);
        List<Product> GetAll();
        Product GetById(int id);
    }
}
