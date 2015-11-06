using System.Data;

namespace e10.Shared.Data.Abstraction
{
    public abstract class Visit : Entity
    {
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Browser { get; set; }
        public string Referer { get; set; }
        public string OperatingSystem { get; set; }
        public bool IsMobile { get; set; }
        public string Visitor { get; set; }
    }

    public class ActorVisit : Visit
    {
        public Actor Actor { get; set; }
        public int ActorId { get; set; }
    }
}