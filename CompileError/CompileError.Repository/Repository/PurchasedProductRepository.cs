using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.DatabaseContext.DatabaseContext;
using CompileError.Model.Model;


namespace CompileError.Repository.Repository
{
    public class PurchasedProductRepository
    {
        ProjectDbContext _projectDbContext = new ProjectDbContext();

        public bool Add(PurchasedProduct purchasedProduct)
        {
            _projectDbContext.PurchasedProducts.Add(purchasedProduct);

            return _projectDbContext.SaveChanges() > 0;
        }

        public List<PurchasedProduct> GetAll()
        {

            return _projectDbContext.PurchasedProducts.ToList();

        }

       
    }
}
