using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface IDictionaryDataService<TEditModel, in TCreateModel, in TDeleteModel> : IDataService<TEditModel, TCreateModel, TDeleteModel>
        where TCreateModel : DictionaryViewModel
        where TEditModel : DictionaryEditViewModel
        where TDeleteModel : IdModel
    {
    }
}