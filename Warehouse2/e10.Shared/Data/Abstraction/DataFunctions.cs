using System;
using System.Data.Entity;

namespace e10.Shared.Data.Abstraction
{
    public class DataFunctions
    {
        [DbFunction("Edm", "DiffDays")]
        public static int? DiffDays2(DateTime? date1, DateTime? date2)
        {
            throw new NotSupportedException("Default date diff not working");
        }
    }
}