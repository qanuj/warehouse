namespace e10.Shared.Data.Abstraction
{
    public abstract class Dictionary : Entity, IDictionary
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }

    public class Country : Dictionary
    {
        public int ISDCode { get; set; }
    }
}