using System;
using System.Linq;
using e10.Shared.Models;  
using e10.Shared.Services.Abstraction;
using e10.Shared.Util; 

namespace e10.Shared.Services
{
    public abstract class SystemCoreService : ISystemCoreService
    {
        public abstract string Upgrade();   
        public EnumList Enums(string space,string filter)
        {
            var types = ReflectionHelper.GetEnumTypesInNamespace(space,filter);
            var vals = new EnumList();
            foreach (var type in types)
            {
                vals.Add(type.Name, Enum.GetNames(type).Select(x => new IdLabel<string> { Id = x, Label = x.ToUnCamel() }));
            }
            return vals;
        }
    }
}