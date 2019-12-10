using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompileError.DatabaseContext.DatabaseContext;
using CompileError.Model.Model;

namespace CompileError.Repository.Repository
{
    public class SalesDetailRepository
    {
        ProjectDbContext _projectDbContext = new ProjectDbContext();


        // public bool Add(SalesDetail salesDetail)
        public bool Add(SalesDetail salesDetail)
        {
            _projectDbContext.SalesDetails.Add(salesDetail);

            return _projectDbContext.SaveChanges() > 0;
        }

        public List<SalesDetail> GetAll()
        {
            return _projectDbContext.SalesDetails.ToList();

        }
    }
}
