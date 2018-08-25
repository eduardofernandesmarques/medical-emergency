using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalEmergency.Presentation.Manager.Models.CEP
{
    public class CEPNotFoundViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Atualizado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Updated { get; set; }

        [Display(Name = "Criado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        [Display(Name = "Ativo")]
        public bool? Active { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Display(Name = "Quantidade")]
        public int Count { get; set; }
    }
}