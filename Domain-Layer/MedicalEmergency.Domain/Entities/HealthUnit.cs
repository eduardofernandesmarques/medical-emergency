using MedicalEmergency.Domain.Entities.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalEmergency.Domain.Entities
{
    public class HealthUnit : Entity
    {
        public int? InstitutionTypeID { get; set; }

        [Display(Name = "Tipo Instituição")]
        public virtual InstitutionType Institution { get; set; }

        public int? EmergencyTypeID { get; set; }

        [Display(Name = "Tipo Emergência")]
        public virtual EmergencyType Emergency { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Display(Name = "Link PT")]
        public  string LinkPT { get; set; }

        [Display(Name = "Link EN")]
        public string LinkEN { get; set; }

        [Display(Name = "Especialidades PT")]
        public string SpecialtiesPT { get; set; }

        [Display(Name = "Especialiades EN")]
        public string SpecialtiesEN { get; set; }

        [Display(Name = "Especialidades ESs")]
        public string SpecialtiesES { get; set; }
    }
}
