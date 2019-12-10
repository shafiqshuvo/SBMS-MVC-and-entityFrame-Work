using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.Model.Model;
using CompileError.DatabaseContext.DatabaseContext;

namespace CompileError.Repository.Repository
{
    public class PurchaseRepository
    {
        ProjectDbContext _projectDbContext = new ProjectDbContext();
        public bool Add(Purchase purchase)
        {
            _projectDbContext.Purchases.Add(purchase);

            return _projectDbContext.SaveChanges() > 0;
        }

        public List<Purchase> GetAll()
        {

            return _projectDbContext.Purchases.ToList();


        }

    }
}
