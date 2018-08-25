using MedicalEmergency.Domain.Entities.Manager;
using MedicalEmergency.Domain.Interfaces.Repositories.Manager;

namespace MedicalEmergency.Infrastructure.Data.Repository.Manager
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
    }
}
