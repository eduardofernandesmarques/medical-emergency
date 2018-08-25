using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Domain.Interfaces.Repositories;

namespace MedicalEmergency.Infrastructure.Data.Repository
{
    public class SpecialityRepository : Repository<Specialty>, ISpecialityRepository
    {
    }
}
