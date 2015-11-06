using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class Social
    {
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Yahoo { get; set; }
        public string Google { get; set; }
        public string LinkedIn { get; set; }
        public string Rss { get; set; }
        public string WebSite { get; set; }
    }
}