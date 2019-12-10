using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Repository.Repository;
using CompileError.Model.Model;

namespace CompileError.Manager.Manager
{
    public class PurchasedProductManager
    {
        PurchasedProductRepository _purchaseProductRepository = new PurchasedProductRepository();

        public bool  Add(PurchasedProduct purchasedProduct)
        {
            return _purchaseProductRepository.Add(purchasedProduct);
        }

        public List<PurchasedProduct> GetAll()
        {
            return _purchaseProductRepository.GetAll();

        }
    }
}
