using MedicalEmergency.Domain.Interfaces.Repositories;
using MedicalEmergency.Infrastructure.Data.Repository;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHealthUnitRepository _healthUnitRepository;
        public HomeController()
        {
            _healthUnitRepository = new HealthUnitRepository();
        }

        // GET: Home
        public ActionResult Index(string Latitude, string Longitude)
        {
            var list = _healthUnitRepository.GetAll();

            if (Latitude != null && Longitude != null)
            {
                double latitude = ((Math.PI * Convert.ToDouble(Latitude, CultureInfo.InvariantCulture)) / 180);
                double longitude = ((Math.PI * Convert.ToDouble(Longitude, CultureInfo.InvariantCulture)) / 180);

                list = _healthUnitRepository.DitanceReorder(latitude, longitude);

                return View(list);
            }

            return View(list);
        }
    }
}