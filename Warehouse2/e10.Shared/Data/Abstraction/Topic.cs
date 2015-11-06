using System.Collections.Generic;

namespace e10.Shared.Data.Abstraction
{
    public class Topic : Entity
    {
        public string Name { get; set; }
        public IList<Post> Posts { get; set; }
    }
}