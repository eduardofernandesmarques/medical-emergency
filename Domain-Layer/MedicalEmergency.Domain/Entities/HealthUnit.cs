using MedicalEmergency.Domain.Entities.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalEmergency.Domain.Entities
{
    public class HealthUnit : Entity
    {
        [Display(Name = "Tipo Institui��o")]
        public int? InstitutionTypeID { get; set; }

        public virtual InstitutionType Institution { get; set; }

        [Display(Name = "Tipo Emerg�ncia")]
        public int? EmergencyTypeID { get; set; }

        public virtual EmergencyType Emergency { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Endere�o")]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Display(Name = "Link Portugu�s")]
        public  string LinkPT { get; set; }

        [Display(Name = "Link Ingl�s")]
        public string LinkEN { get; set; }

        [Display(Name = "Link Espanhol")]
        public string LinkES { get; set; }

        [Display(Name = "Especialidades Portugu�s")]
        public string SpecialtiesPT { get; set; }

        [Display(Name = "Especialiades Ingl�s")]
        public string SpecialtiesEN { get; set; }

        [Display(Name = "Especialidades Espanhol")]
        public string SpecialtiesES { get; set; }

        [NotMapped]
        public double Distance { get; set; }

        [NotMapped]
        public string DistanceDescription { get; set; }
    }
}
