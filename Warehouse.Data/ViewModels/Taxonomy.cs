using System;
using Warehouse.Core;

namespace Warehouse.Data.ViewModels
{
    public class TaxonomyViewModel : IEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
    }
}
