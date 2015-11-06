using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface IPersonDataService<TEditModel, in TCreateModel, in TDeleteModel> : IDataService<TEditModel, TCreateModel, TDeleteModel>
        where TCreateModel : PersonViewModel
        where TEditModel : PersonEditViewModel
        where TDeleteModel : IdModel
    {
    }

}