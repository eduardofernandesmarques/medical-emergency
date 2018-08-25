using System;
using System.ComponentModel.DataAnnotations;
using Entity = MedicalEmergency.Domain.Entities.Manager;

namespace MedicalEmergency.Presentation.Manager.Models.StoreProfile
{
    public class StoreProfileViewModel
    {
        public int ID { get; set; } 

        [Display(Name = "Lojas")]
        public int StoreID { get; set; }

        [Display(Name = "Perfis")]
        public int ProfileID { get; set; }

        public Entity.Store Store { get; set; }
        public Entity.Profile Profile { get; set; }

        [Display(Name = "Criado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        [Display(Name = "Atualizado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Updated { get; set; }

        [Display(Name = "Ativo")]
        public bool? Active { get; set; }
    }
}