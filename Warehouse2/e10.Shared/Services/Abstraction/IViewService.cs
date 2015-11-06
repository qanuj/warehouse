namespace e10.Shared.Services.Abstraction
{
    public interface IViewService
    {
        void AddView(int id, string userAgent, string ipAddress);
    }
}