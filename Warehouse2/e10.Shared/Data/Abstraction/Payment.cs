namespace e10.Shared.Data.Abstraction
{
    public class Payment : Transaction
    {
        public string Gateway { get; set; }
        public string Capture { get; set; }
    }
}