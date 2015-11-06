using System.Configuration;

namespace e10.Shared.Models
{
    public class CountLabel<T>
    {     
        public T Count { get; set; }
        public string Label { get; set; }
    }
}