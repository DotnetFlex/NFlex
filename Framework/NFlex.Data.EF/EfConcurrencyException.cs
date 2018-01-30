using System;

namespace NFlex.Data.EF
{
    public class EfConcurrencyException:Exception
    {
        public EfConcurrencyException(Exception ex) : base(ex.Message, ex) { }
    }
}
