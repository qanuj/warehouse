using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core;
using Warehouse.Data;
using Warehouse.Data.ViewModels;

namespace Warehouse.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Weather> _weatherRepository;
        private readonly IRepository<Taxonomy> _categoryRepository;

        public WarehouseService(IRepository<Weather> weatheRepository, IRepository<Taxonomy> categoryRepository)
        {
            _weatherRepository = weatheRepository;
            _categoryRepository = categoryRepository;
        }

        public Weather AddWeather(Weather weather)
        {
            _weatherRepository.Create(weather);
            _weatherRepository.SaveChanges();
            return weather;
        }

        public Taxonomy AddCategory(Taxonomy category)
        {
            _categoryRepository.Create(category);
            _categoryRepository.SaveChanges();
            return category;
        }

        public Weather FindWeather(int id)
        {
            return _weatherRepository.ById(id);
        }
    }
}
