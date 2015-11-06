using System;
using System.Text;

namespace Warehouse.Core
{
    public interface IWarehouseService
    {
        Weather AddWeather(Weather weather);
        Taxonomy AddCategory(Taxonomy category);
        Weather FindWeather(int id);
    }
}
