using System.Collections.Generic;

namespace MedicalEmergency.Domain.Entities
{
    public class Specialty : Entity
    {
        public string Description { get; set; }
        public virtual IList<HealthUnitSpeciality> HealthUnitSpeciality { get; set; }
    }
}
