namespace e10.Shared.Providers
{
    public class DoNotReplyAte10EmailConfigProvider : IEmailConfigProvider
    {
        public string From => "donotreply@e10.in";
        public string Name => "Do Not Reply";
        public string Server => "smtp.gmail.com";
        public int Port => 587;
        public bool IsGmail => true;
        public string Password => "bj%YkV5I5}HlW?Yj";
        public string SendGridApiKey => "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";
    }
}