using MedicalEmergency.Domain.Entities;
using System.Collections.Generic;

namespace MedicalEmergency.Domain.Interfaces.Repositories
{
    public interface IHealthUnitRepository : IRepository<HealthUnit>
    {
        IList<HealthUnit> DitanceReorder(double latitude, double longitude);
    }
}
