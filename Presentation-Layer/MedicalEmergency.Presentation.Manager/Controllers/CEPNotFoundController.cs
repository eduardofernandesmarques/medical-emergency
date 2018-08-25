using PagedList;
using MedicalEmergency.Domain.Interfaces.Repositories.Leads;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Models.CEP;
using MedicalEmergency.Presentation.Manager.Models.CEPNotFound;
using System.Linq;
using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    public class CEPNotFoundController : Controller
    {
        private readonly ICEPNotFoundRepository _CEPNotFoundRepository;

        public CEPNotFoundController(ICEPNotFoundRepository CEPNotFoundRepository)
        {
            _CEPNotFoundRepository = CEPNotFoundRepository;
        }

        // GET: Accounts
        public ActionResult Index(int? page, CEPNotFoundSearchModel cepNotFoundSearchModel)
        {
            var list = _CEPNotFoundRepository.GetAll().Where(x => !x.Found).GroupBy(x => x.CEP).Select(y => new { CEP = y.Key, Count = y.Count()}).ToList();

            if (cepNotFoundSearchModel.CEP != null)
                list.Where(x => x.CEP.Contains(cepNotFoundSearchModel.CEP));

            var result = list.Select(x => new CEPNotFoundViewModel() { CEP = x.CEP, Count = x.Count }).OrderBy(x => x.Count).ToList();

            int pageSize = 10;

            return View(result.ToPagedList(page ?? 1, pageSize));
        }
    }
}
