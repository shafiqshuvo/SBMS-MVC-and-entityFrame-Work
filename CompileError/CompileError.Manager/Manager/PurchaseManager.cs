using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.Repository.Repository;

namespace CompileError.Manager.Manager
{
   
    public class PurchaseManager
    {
        PurchaseRepository _purchaseRepository = new PurchaseRepository(); 

        public bool Add(Purchase purchase)
        {
            return _purchaseRepository.Add(purchase);
        }

        public List<Purchase> GetAll()
        {
            return _purchaseRepository.GetAll();
        }
    }
}
