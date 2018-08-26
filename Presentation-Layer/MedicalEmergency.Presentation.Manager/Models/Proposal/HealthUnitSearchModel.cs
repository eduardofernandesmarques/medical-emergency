namespace MedicalEmergency.Presentation.Manager.Models.Proposal
{
    public class HealthUnitSearchModel
    {
        public int? EmergencyTypeID { get; set; }
        public string Name { get; set; }
        public int? InstitutionID { get; set; }
        public int? SpecialityID { get; set; }
    }
}
