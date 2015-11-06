namespace e10.Shared.Data.Abstraction
{
    public abstract class Person : Entity, IPerson
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string PictureUrl { get; set; }
    }

}
