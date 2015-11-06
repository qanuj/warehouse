using System.IO;

namespace e10.Shared.Models
{
    public class MessageAttachement
    {
        public string FilePath { get; set; }
        public Stream Stream { get; set; }
        public string Name { get; set; }
    }
}