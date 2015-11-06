namespace e10.Shared.Services.Abstraction
{
    public interface ISecuredService
    {
        string CurrentUserId { get; }
    }
}