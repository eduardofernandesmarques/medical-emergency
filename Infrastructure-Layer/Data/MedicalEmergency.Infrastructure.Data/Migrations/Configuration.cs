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

            context.EmergencyType.AddOrUpdate(
                new EmergencyType { Description = "Hospital" },
                new EmergencyType { Description = "PA" }
            );

            context.InstitutionType.AddOrUpdate(
                new InstitutionType { Description = "Público" },
                new InstitutionType { Description = "Público/Privado" },
                new InstitutionType { Description = "Privado" }
            );

            Account account = new Account() { Login = "admin", Password = "admin" };
            account.EncryptPassword();

            context.Account.AddOrUpdate(
               account
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
