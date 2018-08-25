using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Infrastructure.Data.Repository.Manager;
using System;
using System.Linq;
using System.Web.Security;

namespace MedicalEmergency.Presentation.Manager.Models
{
    public class Roles : RoleProvider
    {
        private readonly IProfileRepository _profileRepository;

        public Roles()
        {
            _profileRepository = new ProfileRepository();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string roles = _profileRepository.Get(x => x.Name == username).FirstOrDefault().Name;
            string[] retorno = { roles };

            return retorno;
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var account = _profileRepository.Get(x => x.Name == username).FirstOrDefault();

            return account.Name.Equals(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}