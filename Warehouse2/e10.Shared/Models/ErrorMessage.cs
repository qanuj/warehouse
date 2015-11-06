using Newtonsoft.Json;

namespace e10.Shared.Models
{
    public class ErrorMessage
    {
        public virtual string Message { get; set; }
        [JsonProperty(PropertyName = "error_description")]
        public virtual string ErrorDescription { get; set; }
        public virtual string Source { get; set; }
        public virtual string Error { get; set; }
        public virtual string StackTrace { get; set; }
    }
}