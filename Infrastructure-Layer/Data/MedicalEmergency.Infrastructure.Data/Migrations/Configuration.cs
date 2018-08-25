using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Domain.Entities.Manager;
using MedicalEmergency.Domain.Entities.Types;
using MedicalEmergency.Infrastructure.Data.Persistence;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MedicalEmergency.Infrastructure.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MedicalEmergencyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(MedicalEmergencyContext context)
        {
            DeleteData<HealthUnit>(context);
            DeleteData<EmergencyType>(context);
            DeleteData<InstitutionType>(context);
            DeleteData<Specialty>(context);

            context.EmergencyType.AddOrUpdate(
                new EmergencyType { Description = "0" },
                new EmergencyType { Description = "1" },
                new EmergencyType { Description = "2" },
                new EmergencyType { Description = "3" },
                new EmergencyType { Description = "4" },
                new EmergencyType { Description = "5" }
            );

            context.InstitutionType.AddOrUpdate(
                new InstitutionType { Description = "0" },
                new InstitutionType { Description = "1" },
                new InstitutionType { Description = "2" },
                new InstitutionType { Description = "3" },
                new InstitutionType { Description = "4" },
                new InstitutionType { Description = "5" },
                new InstitutionType { Description = "6" },
                new InstitutionType { Description = "7" },
                new InstitutionType { Description = "8" }
            );

            context.Specialty.AddOrUpdate(
                new Specialty { Description = "1" }
            );

            context.Account.AddOrUpdate(
                new Account { Login = "admin", Password = "teste" }
            );
        }

        private void DeleteData<T>(MedicalEmergencyContext context) where T : class
        {
            var entity = context.Set<T>().ToList();

            entity.ForEach(item => context.Set<T>().Remove(item));

            context.SaveChanges();
        }
    }
}
