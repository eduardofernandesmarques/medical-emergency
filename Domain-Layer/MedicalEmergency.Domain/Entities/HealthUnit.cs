using MedicalEmergency.Domain.Entities.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalEmergency.Domain.Entities
{
    public class HealthUnit : Entity
    {
        [Display(Name = "Tipo Instituição")]
        public int? InstitutionTypeID { get; set; }

        public virtual InstitutionType Institution { get; set; }

        [Display(Name = "Tipo Emergência")]
        public int? EmergencyTypeID { get; set; }

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

        [NotMapped]
        public double Distance { get; set; }

        [NotMapped]
        public string DistanceDescription { get; set; }
    }
}
