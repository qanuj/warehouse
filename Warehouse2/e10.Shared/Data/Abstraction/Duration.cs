using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class Duration
    {
        public int Years { get; set; }
        public int Months { get; set; }
    }
}