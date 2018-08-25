namespace MedicalEmergency.Domain.Entities
{
    public class HealthUnitSpeciality : Entity
    {
        public int HealthUnitID { get; set; }
        public int SpecialityID { get; set; }
        public string Language { get; set; }
        public virtual HealthUnit HealthUnit { get; set; }
        public virtual Specialty Speciality { get; set; }
    }
}
