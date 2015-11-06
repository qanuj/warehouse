using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface ISystemCoreService : IService
    {
        string Upgrade();
        EnumList Enums(string space, string filter);
    }
}