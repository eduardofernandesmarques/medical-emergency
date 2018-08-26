using MedicalEmergency.Domain.Entities.Types;
using System;
using System.Collections.Generic;

namespace MedicalEmergency.Domain.Entities
{
    public class HealthUnit : Entity
    {
        public int? InstitutionTypeID { get; set; }
        public virtual InstitutionType Institution { get; set; }
        public int? EmergencyTypeID { get; set; }
        public virtual EmergencyType Emergency { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public  string LinkPT { get; set; }
        public string LinkEN { get; set; }
        public virtual IList<Specialty> SpecialtiesPT { get; set; }
        public virtual IList<Specialty> SpecialtiesEN { get; set; }
        public virtual IList<Specialty> SpecialtiesES { get; set; }
    }
}
