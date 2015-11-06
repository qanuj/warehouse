using System.Collections;
using System.Collections.Generic;

namespace Warehouse.Core
{
    public class Taxonomy  : Entity
    {
        public string Title { get; set; }
        public IList<Weather> Weathers { get; set; }
    }
}
