using System;

namespace e10.Shared.Data.Abstraction
{
    public interface IEntityFunctions
    {
        int? DiffDays2(DateTime? date1, DateTime? date2);
    }
}