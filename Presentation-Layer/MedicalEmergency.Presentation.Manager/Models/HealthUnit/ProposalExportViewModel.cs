using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalEmergency.Presentation.Manager.Models.Proposal
{
    public class ProposalExportViewModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; } //id

        [Display(Name = "Codigo da Proposta")]
        public long? ProposalID { get; set; } //id da proposta

        [Display(Name = "Loja")]
        public int StoreID { get; set; } //id da 

        [Display(Name = "Lojista")]
        public int StorekeeperID { get; set; } //id da loja

        [Display(Name = "Oferta")]
        public int? OfferID { get; set; } //id da oferta

        [Display(Name = "Nome")]
        public string Name { get; set; } //nome completo

        [Display(Name = "CPF")]
        public string CPF { get; set; } //cpf

        [Display(Name = "Data de Nascimento")]
        public DateTime Birthday { get; set; } //data de nascimento

        [Display(Name = "Telefone")]
        public string Phone { get; set; } //telefone

        [Display(Name = "Email")]
        public string Email { get; set; } //email

        [Display(Name = "Renda Bruta")]
        public decimal GrossIncome { get; set; } //renda bruta

        [Display(Name = "Valor Outras Rendas")]
        public decimal OtherIncomesValue { get; set; } //valor de outras rendas

        [Display(Name = "Valor Emprestimo")]
        public decimal PurchaseValue { get; set; } //valor de empréstimo / compra

        [Display(Name = "Numero Parcelas")]
        public int PlotsNumber { get; set; } //número de parcelas

        [Display(Name = "Primeira Parcela")]
        public DateTime FirstInstallmentMaturity { get; set; } //data para 1ª parcela

        [Display(Name = "Possui Talao")]
        public bool Bead { get; set; } //possui talão

        [Display(Name = "Concordo SCR")]
        public bool SCR { get; set; } //Li e concordo com SCR

        [Display(Name = "Sexo")]
        public string Sex { get; set; }

        [Display(Name = "Classe Profissional")]
        public string ProfessionalClass { get; set; }

        [Display(Name = "Motivo Emprestimo")]
        public string ObjectiveLoan { get; set; }

        #region Complement
        [Display(Name = "Nome Mae")]
        public string MotherName { get; set; } //nome da mãe

        [Display(Name = "Cidade Nascimento")]
        public string BirthdayCity { get; set; } //cidade nascimento

        [Display(Name = "Status Civil")]
        public string CivilStatus { get; set; }

        [Display(Name = "Escolaridade")]
        public string Scholarity { get; set; }

        [Display(Name = "Nacionalidade")]
        public string Natiolality { get; set; }

        [Display(Name = "UF Nascimento")]
        public string BirthdayUF { get; set; }
        #endregion 

        #region Document

        [Display(Name = "Numero Documento")]
        public string DocumentNumber { get; set; } //número do documento

        [Display(Name = "Tipo Documento")]
        public string DocumentType { get; set; }
        #endregion

        #region Residencial
        [Display(Name = "CEP")]
        public string CEP { get; set; } //cep residencial

        [Display(Name = "Endereco")]
        public string Address { get; set; } //endereço residencial

        [Display(Name = "Numero Endereco")]
        public string AddressNumber { get; set; } //numero residencial

        [Display(Name = "Complemento")]
        public string Complement { get; set; } //complemento residencial

        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; } //bairro residencial

        [Display(Name = "Cidade Residencia")]
        public string ResidencialCity { get; set; } //cidade residencial

        [Display(Name = "UF Residencia")]
        public string ResidencialUF { get; set; }
        #endregion

        #region Professional
        [Display(Name = "Nome Empresa")]
        public string CompanyName { get; set; } //nome da empresa

        [Display(Name = "Data Admissao")]
        public DateTime? AdmissionDate { get; set; } //data de admissão

        [Display(Name = "CEP Empresa")]
        public string CompanyCEP { get; set; } //cep empresa

        [Display(Name = "Endereco Empresa")]
        public string CompanyAddress { get; set; } //endereço empresa

        [Display(Name = "Numero Empresa")]
        public string CompanyNumber { get; set; } //número empresa

        [Display(Name = "Bairro Empresa")]
        public string CompanyNeighborhood { get; set; } //bairro empresa

        [Display(Name = "Cidade Empresa")]
        public string CompanyCity { get; set; } //cidade empresa

        [Display(Name = "Telefone Empresa")]
        public string CompanyPhone { get; set; } //telefone empresa

        [Display(Name = "UF Empresa")]
        public string CompanyUF { get; set; }
        #endregion

        #region Reference
        [Display(Name = "Nome Referencia")]
        public string ReferenceName { get; set; } //nome completo

        [Display(Name = "Telefone Referencia")]
        public string ReferencePhone { get; set; } //telefone referência

        [Display(Name = "Relacionamento")]
        public string ReferenceRelationship { get; set; }
        #endregion

        #region Bank
        [Display(Name = "Agencia")]
        public string AgencyCode { get; set; } //agência

        [Display(Name = "Numero Conta")]
        public string AccountCode { get; set; } //conta

        [Display(Name = "Conta Banco")]
        public string BankAccount { get; set; }

        [Display(Name = "Conta")]
        public string Account { get; set; }

        [Display(Name = "Banco")]
        public string Bank { get; set; }
        #endregion

        [Display(Name = "Status")]
        public string Status { get; set; } //status

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