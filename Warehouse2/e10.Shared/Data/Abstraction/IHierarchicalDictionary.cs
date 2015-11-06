using System.Collections.Generic;

namespace e10.Shared.Data.Abstraction
{
    public interface IHierarchicalDictionary<T> where T : Entity
    {
        T Parent { get; set; }
        int ParentId { get; set; }
        IList<T> Children { get; set; }
    }
}