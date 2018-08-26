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
            AccountRepository accountRepository = new AccountRepository();

            Roles = string.Join(",", accountRepository.GetAll(x => x.Active.Value).Select(x => x.Login).ToArray()); 

            return base.AuthorizeCore(httpContext);
        }
    }
}
