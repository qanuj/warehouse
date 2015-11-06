namespace e10.Shared.Models
{
    public class IdModel
    {
        public int Id { get; set; }
    }

    public class UserCodeViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
    }

    public class IdLabel<T>
    {
        public T Id { get; set; }
        public string Label { get; set; }
    }
}