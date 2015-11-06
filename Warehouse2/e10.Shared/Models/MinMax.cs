namespace e10.Shared.Models
{
    public class MinMax<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class MinMax : MinMax<int>
    {
    }
}