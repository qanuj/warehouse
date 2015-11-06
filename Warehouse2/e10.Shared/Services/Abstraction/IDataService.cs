using System;
using System.Linq;
using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface IDataService<TEditModel, in TCreateModel, in TDeleteModel>
    {
        bool Delete(TDeleteModel model);
        TEditModel Create(TCreateModel model);
        TEditModel Update(TEditModel model);
    }
    
}