using System;

namespace Warehouse.Core
{
    public class Weather : Entity
    {                      
        public float Humidity { get; set; }
        public float Temprature { get; set; }
        public float Light { get; set; }
        public Taxonomy Category { get; set; }
        public int CategoryId { get; set; }

    }

    public abstract class Entity  : IEntity, IState, ISoftDelete
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime? Deleted { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
    }
}