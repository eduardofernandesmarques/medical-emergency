using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalEmergency.Presentation.Manager.Models.Offer
{
    public class OfferViewModel
    {
        public int ID { get; set; } //id

        [Display(Name = "ID Oferta")]
        public string OfferID { get; set; } //idOferta

        [Display(Name = "Origem")]
        public string OfferSource { get; set; } //origemOferta

        [Display(Name = "Primeira Parcela")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstInstallmentMaturityDate { get; set; } //dataVencimentoPrimeiraParcela

        [Display(Name = "Vencimento Última Parcela")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastInstallmentMaturityDate { get; set; } //dataVencimentoUltimaParcela

        [Display(Name = "CET Anual")]
        public string AnnualCET { get; set; } //cetAnual

        [Display(Name = "CET Mensal")]
        public string MonthlyCET { get; set; } //cetMensal

        [Display(Name = "Movimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MovimentDate { get; set; } //dataMovimento

        [Display(Name = "Código Plano")]
        public string PlanCode { get; set; } //codigoPlano

        [Display(Name = "Descrição Plano")]
        public string PlanDescription { get; set; } //descricaoPlano

        [Display(Name = "Código Produto")]
        public string ProductCode { get; set; } //codigoProduto

        [Display(Name = "Descrição Produto")]
        public string ProductDescription { get; set; } //descricaoProduto

        [Display(Name = "Emissão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? IssueDate { get; set; } //dataEmissao

        [Display(Name = "Índice Financiado")]
        public string FinancedIndex { get; set; } //financiadoIndice

        [Display(Name = "Seguro")]
        public string FinancedInsurance { get; set; } //finioFSeguro

        [Display(Name = "ID Produto Seguro")]
        public string ProductInsurenceID { get; set; } //idProdutoSeguro

        [Display(Name = "IOF")]
        public string IOFValue { get; set; } //valorIOF

        [Display(Name = "Prazo")]
        public string Term { get; set; } //prazo

        [Display(Name = "Valor Primeira Prestação")]
        public string FirstInstallmentValue { get; set; } //valorPrimeiraPrestacao

        [Display(Name = "Seguradora")]
        public string Insurer { get; set; } //seguradora

        [Display(Name = "Seguro")]
        public string Insurence { get; set; } //seguro

        [Display(Name = "Valor TAC")]
        public string TACValue { get; set; } //valorTAC

        [Display(Name = "Valor Taxa")]
        public string TaxValue { get; set; } //valorTaxa

        [Display(Name = "Taxa Anual")]
        public string AnnualTaxValue { get; set; } //valorTaxaAnual

        [Display(Name = "Valor Compra")]
        public string PurchaseValue { get; set; } //valorCompra

        [Display(Name = "Crédito")]
        public string CreditValue { get; set; } //valorCredito

        [Display(Name = "Valor Financiado")]
        public string FinancedValue { get; set; } //valorFinanciado

        [Display(Name = "Valor Liberado")]
        public string ReleasedValue { get; set; } //valorLiberado

        [Display(Name = "Valor Liquidar")]
        public string SettleValue { get; set; } //valorLiquidar

        [Display(Name = "Valor Parcela")]
        public string InstallmentValue { get; set; } //valorParcela

        [Display(Name = "Valor Seguro")]
        public string InsuranceValue { get; set; } //valorSeguro

        [Display(Name = "Taxa Inclusão")]
        public string InclusionTaxValue { get; set; } //valorTaxaInclusao

        [Display(Name = "Taxa Renovação")]
        public string RenewalTaxValue { get; set; } //valorTaxaRenovacao

        [Display(Name = "Total")]
        public string TotalValue { get; set; } //valorTotal

        [Display(Name = "Total PST")]
        public string TotalPSTValue { get; set; } //valorTotalPST

        [Display(Name = "Faixa Tarifa Inclusão")]
        public string ExpiredRangeRateInclusion { get; set; } //expiradaFaixaTarifaInclusao

        [Display(Name = "Faixa Retorno")]
        public string SuggestedTrackReturn { get; set; } //sugeridoFaixaRetorno

        [Display(Name = "Tipo Simulação")]
        public string SimulationType { get; set; } //tipoSimulacao

        [Display(Name = "IOF %")]
        public decimal IOFPercent { get; set; } //% IOF (Valor IOF / valorFinanciado)

        [Display(Name = "Tarifa Cadastro %")]
        public decimal RegisterRatePercent { get; set; } //% tarifa de cadastro (valorTaxaInclusao / valorFinanciado)

        [Display(Name = "Seguro Prestamista %")]
        public decimal LenderPercent { get; set; } //% seguro prestamista (valorSeguro / valorFinanciado)

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