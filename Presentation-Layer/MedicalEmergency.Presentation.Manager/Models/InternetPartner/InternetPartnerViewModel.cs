using LinqToExcel.Attributes;
using System;

namespace MedicalEmergency.Presentation.Manager.Models
{
    public class InternetPartnerImportViewModel
    {
        [ExcelColumn("Lojista")]
        public string StoreKeeperID { get; set; }

        [ExcelColumn("Loja")]
        public string StoreID { get; set; }

        [ExcelColumn("CNPJ")]
        public string CNPJ { get; set; }

        [ExcelColumn("Nome")]
        public string Name { get; set; }

        [ExcelColumn("Abreviatura")]
        public string Abbreviation { get; set; }

        [ExcelColumn("Telefone")]
        public string Phone { get; set; }

        [ExcelColumn("CEP")]
        public string Zipcode { get; set; }

        [ExcelColumn("NUMERO")]
        public string Number { get; set; }

        [ExcelColumn("LOGRADOURO")]
        public string Logradouro { get; set; }

        [ExcelColumn("BAIRRO")]
        public string Neighborhood { get; set; }

        [ExcelColumn("COMPLEMENTO")]
        public string Complement { get; set; }

        [ExcelColumn("CIDADE")]
        public string City { get; set; }

        [ExcelColumn("ESTADO")]
        public string State { get; set; }

        [ExcelColumn("Segmentacao")]
        public string Targeting { get; set; }

        [ExcelColumn("ATUALIZADO")]
        public DateTime? Update { get; set; }

        [ExcelColumn("LojaMedicalEmergencyGF")]
        public bool? MedicalEmergencyGFStore { get; set; }

        [ExcelColumn("CityID")]
        public int? CityID { get; set; }

        [ExcelColumn("WhatsApp")]
        public string WhatsApp { get; set; }

        [ExcelColumn("PartnerCode")]
        public string PartnerCode { get; set; }

        [ExcelColumn("UserCode")]
        public string UserCode { get; set; }

        [ExcelColumn("Password")]
        public string Password { get; set; }

        [ExcelColumn("Nome - Loja MedicalEmergency")]
        public string OwnStoreName { get; set; }
    }
}