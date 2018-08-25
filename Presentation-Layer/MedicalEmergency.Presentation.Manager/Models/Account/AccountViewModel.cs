using MedicalEmergency.Domain.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Entity = MedicalEmergency.Domain.Entities.Manager;

namespace MedicalEmergency.Presentation.Manager.Models.Account
{
    public class AccountViewModel
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

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Display(Name = "Usuário")]
        public string User { get; set; }

        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Perfil")]
        public int ProfileID { get; set; }

        public Entity.Profile Profile { get; set; }

        public void EncryptPassword()
        {
            Password = Encrypt.GetMd5Hash(MD5.Create(), Password);
        }
    }
}