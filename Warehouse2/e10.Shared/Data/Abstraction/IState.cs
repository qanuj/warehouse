using System;

namespace e10.Shared.Data.Abstraction
{
    public interface IState
    {
        DateTime Created { get; set; }
        DateTime LastModified { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}