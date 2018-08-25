using MedicalEmergency.Domain.Enums;

namespace MedicalEmergency.Presentation.Manager.Models.Proposal
{
    public class ProposalSearchModel
    {
        public string StoreID { get; set; }
        public string CPF { get; set; }
        public string ProposalID { get; set; }
        public StatusDomain Status { get; set; }
    }
}
