using MedicalEmergency.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalEmergency.Presentation.Manager.Models.Proposal
{
    public class ProposalViewModel
    {
        public int ID { get; set; } //id

        [Display(Name = "ID da Proposta")]
        public long? ProposalID { get; set; } //id da proposta

        [Display(Name = "Loja")]
        public int StoreID { get; set; } //id da loja
        
        [Display(Name = "ID Lojista")]
        public int StorekeeperID { get; set; } //id do lojista

        [Display(Name = "Oferta")]
        public int? OfferID { get; set; } //id da oferta

        [Display(Name = "Nome")]
        public string Name { get; set; } //nome completo

        [Display(Name = "CPF")]
        public string CPF { get; set; } //cpf

        [Display(Name = "Data Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; } //data de nascimento

        [Display(Name = "Telefone")]
        public string Phone { get; set; } //telefone

        [Display(Name = "Email")]
        public string Email { get; set; } //email

        [Display(Name = "Renda Bruta")]
        public string GrossIncome { get; set; } //renda bruta

        [Display(Name = "Outras Rendas")]
        public string OtherIncomesValue { get; set; } //valor de outras rendas

        [Display(Name = "Emprestimo")]
        public string PurchaseValue { get; set; } //valor de empréstimo / compra

        [Display(Name = "Número Parcelas")]
        public int PlotsNumber { get; set; } //número de parcelas

        [Display(Name = "Primeira Parcela")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FirstInstallmentMaturity { get; set; } //data para 1ª parcela

        [Display(Name = "Sexo")]
        public string Sex { get; set; } //sexo

        [Display(Name = "Classe Profissional")]
        public string ProfessionalClass { get; set; } //classe profissional

        [Display(Name = "Motivo Empréstimo")]
        public string ObjectiveLoan { get; set; } //objetivo empréstimo

        #region Document

        [Display(Name = "Número Documento")]
        public string DocumentNumber { get; set; } //número do documento

        [Display(Name = "Tipo Documento")]
        public string DocumentType { get; set; } //tipo documento
        #endregion

        public string Status { get; set; } //status

        [Display(Name = "Criado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; } //criação

        [Display(Name = "Atualizado")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Updated { get; set; } //atualizado

        [Display(Name = "Ativo")]
        public bool? Active { get; set; } //ativo

        [Display(Name = "Hash")]
        public string Hash { get; set; } //hash do email

        [Display(Name = "Possui Talão")]
        public bool Bead { get; set; } //possui talão

        [Display(Name = "Acordo SCR")]
        public bool SCR { get; set; } //Li e concordo com SCR

        [Display(Name = "Tipo Seguro")]
        public string InsuranceType { get; set; } //tipo Seguro

        #region Complement

        [Display(Name = "Nome da Mãe")]
        public string MotherName { get; set; } //nome da mãe

        [Display(Name = "Cidade Nascimento")]
        public string BirthdayCity { get; set; } //cidade nascimento

        [Display(Name = "Estado Civil")]
        public string CivilStatus { get; set; }

        [Display(Name = "Escolaridade")]
        public string Scholarity { get; set; }

        [Display(Name = "Nacionalidade")]
        public string Natiolality { get; set; }

        [Display(Name = "Estado Nascimento")]
        public string BirthdayUF { get; set; }
        #endregion

        #region Document
        #endregion

        #region Residencial

        [Display(Name = "CEP")]
        public string CEP { get; set; } //cep residencial

        [Display(Name = "Endereço")]
        public string Address { get; set; } //endereço residencial

        [Display(Name = "Número")]
        public string AddressNumber { get; set; } //numero residencial

        [Display(Name = "Complemento")]
        public string Complement { get; set; } //complemento residencial

        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; } //bairro residencial

        [Display(Name = "Cidade")]
        public string ResidencialCity { get; set; } //cidade residencial

        [Display(Name = "Estado")]
        public string ResidencialUF { get; set; }
        #endregion

        #region Professional

        [Display(Name = "Nome Empresa")]
        public string CompanyName { get; set; } //nome da empresa

        [Display(Name = "Data Admissão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AdmissionDate { get; set; } //data de admissão

        [Display(Name = "CEP Empresa")]
        public string CompanyCEP { get; set; } //cep empresa

        [Display(Name = "Endereço Empresa")]
        public string CompanyAddress { get; set; } //endereço empresa

        [Display(Name = "Número Empresa")]
        public string CompanyNumber { get; set; } //número empresa

        [Display(Name = "Bairro Empresa")]
        public string CompanyNeighborhood { get; set; } //bairro empresa

        [Display(Name = "Cidade Empresa")]
        public string CompanyCity { get; set; } //cidade empresa

        [Display(Name = "Telefone Empresa")]
        public string CompanyPhone { get; set; } //telefone empresa

        [Display(Name = "Estado Empresa")]
        public string CompanyUF { get; set; }
        #endregion

        #region Reference

        [Display(Name = "Nome Referência")]
        public string ReferenceName { get; set; } //nome completo

        [Display(Name = "Telefone Referência")]
        public string ReferencePhone { get; set; } //telefone referência

        [Display(Name = "Relacionamento Referência")]
        public string ReferenceRelationship { get; set; }
        #endregion

        #region Bank

        [Display(Name = "Agência Conta")]
        public string AgencyCode { get; set; } //agência

        [Display(Name = "Código Conta")]
        public string AccountCode { get; set; } //conta

        [Display(Name = "Conta Banco")]
        public string BankAccount { get; set; }

        [Display(Name = "Conta")]
        public string Account { get; set; }

        [Display(Name = "Banco")]
        public string Bank { get; set; } //banco
        #endregion

        [Display(Name = "Status Email")]
        public int? EmailStatus { get; set; } //status email

        [Display(Name = "Status ")]
        public StatusDomain StatusDomain { get; set; } //status email
    }
}