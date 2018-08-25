using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalEmergency.Presentation.Manager.Models.Profile
{
    public class ProfileViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Criado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        [Display(Name = "Atualizado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Updated { get; set; }

        [Display(Name = "Ativo")]
        public bool? Active { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Nível")]
        public int Level { get; set; }
    }
}