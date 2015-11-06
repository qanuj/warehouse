namespace e10.Shared.Data.Abstraction
{
    public interface IPerson
    {
        string FullName { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
    }
}