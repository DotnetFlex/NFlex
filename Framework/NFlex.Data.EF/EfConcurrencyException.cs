using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Data.EF
{
    public class EfConcurrencyException:Exception
    {
        public EfConcurrencyException(Exception ex) : base(ex.Message, ex) { }
    }
}
