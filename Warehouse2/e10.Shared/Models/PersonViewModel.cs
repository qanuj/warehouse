namespace e10.Shared.Models
{
    public class PersonViewModel : PersonEditViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int? LocationId { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Twitter { get; set; }
        public string WebSite { get; set; }
        public string Rss { get; set; }
        public string LinkedIn { get; set; }
        public string Google { get; set; }
        public string Yahoo { get; set; }
        public string Facebook { get; set; }
        public string About { get; set; }
        public string AlternateNumber { get; set; }
        public string Profile { get; set; }
    }
}