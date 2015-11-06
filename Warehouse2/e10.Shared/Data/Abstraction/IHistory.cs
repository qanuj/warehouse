using System;

namespace e10.Shared.Data.Abstraction
{
    public interface IHistory
    {
        DateTime? Acted { get; set; }
        string ActedBy { get; set; }
    }
}