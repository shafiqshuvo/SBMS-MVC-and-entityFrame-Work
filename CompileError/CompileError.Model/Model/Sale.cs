using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileError.Model.Model
{
    public class Sale
    {


        //public int Id { get; set; }
        //public DateTime Date { get; set; }
        //public string Code { get; set; }
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }
        //public List<SalesDetails> SalesDetails { get; set; }

        public Sale()
        {
            SalesDetails = new List<SalesDetail>();
        }
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Date { get; set; }

        public string Code { get; set; }
        public List<SalesDetail> SalesDetails{ get; set;  }
    }
}
