using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Infrastructure.Data.Repository.Manager;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IProfileRepository profileRepository = new ProfileRepository();

            Roles = string.Join(",", profileRepository.GetAll(x => x.Active.Value).Select(x => x.Name).ToArray()); 

            return base.AuthorizeCore(httpContext);
        }
    }
}
