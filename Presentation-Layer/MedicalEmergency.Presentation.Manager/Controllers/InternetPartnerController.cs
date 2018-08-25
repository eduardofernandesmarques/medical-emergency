using MedicalEmergency.Domain.Interfaces.Repositories;
using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Presentation.Manager.Utilities;
using System.Net;
using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    public class InternetPartnerController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IInternetPartnerRepository _internetPartnerRepository;
        private readonly IInternetPartnerLocationRepository _internetPartnerLocationRepository;

        public InternetPartnerController(IStoreRepository storeRepository, IInternetPartnerRepository internetPartnerRepository, IInternetPartnerLocationRepository internetPartnerLocationRepository)
        {
            _storeRepository = storeRepository;
            _internetPartnerRepository = internetPartnerRepository;
            _internetPartnerLocationRepository = internetPartnerLocationRepository;
        }


        // GET: Store
        [HttpGet]
        public ActionResult UpdateStores()
        {
            string  message = InternetPartnerUtil.GenerateScriptSync(_internetPartnerRepository, _internetPartnerLocationRepository);

            return message == null ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpStatusCodeResult(500, message); ;
        }
    }
}
