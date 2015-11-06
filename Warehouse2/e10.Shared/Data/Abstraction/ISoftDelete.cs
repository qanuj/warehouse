using System;

namespace e10.Shared.Data.Abstraction
{
    public interface ISoftDelete
    {
        DateTime? Deleted { get; set; }
        bool IsDeleted { get; set; }
        string DeletedBy { get; set; }
    }
}