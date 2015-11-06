using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class TaxConfig
    {
        public string Name { get; set; }
        public double Rate { get; set; }
    }
}