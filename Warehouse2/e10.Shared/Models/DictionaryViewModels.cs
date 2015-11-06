namespace e10.Shared.Models
{
    public class CountryDictionaryEditViewModel : DictionaryEditViewModel
    {
        public int ISDCode { get; set; }
    }
    public class CountryDictionaryViewModel : CountryDictionaryEditViewModel { }
    public class CountryDictionaryCreateViewModel : CountryDictionaryEditViewModel { }
    public class CountryDeleteViewModel : IdModel { }
}