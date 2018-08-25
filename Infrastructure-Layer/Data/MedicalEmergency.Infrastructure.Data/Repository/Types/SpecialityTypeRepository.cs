using MedicalEmergency.Domain.Entities.Types;
using MedicalEmergency.Domain.Interfaces.Repositories.Types;

namespace MedicalEmergency.Infrastructure.Data.Repository.Types
{
    public class EmergencyTypeRepository : Repository<EmergencyType>, IEmergencyTypeRepository
    {
    }
}
