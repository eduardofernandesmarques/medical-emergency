using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Domain.Interfaces.Repositories;
using MedicalEmergency.Infrastructure.Data.Repository;
using MedicalEmergency.Presentation.Manager.Models.Home;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

using System.Web.Http;
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
        public ActionResult Index(PositionViewModel position)
        {
            var healthUnits = _healthUnitRepository.GetAll();

            if (position != null)
            {
                double latitude = ((Math.PI * Convert.ToDouble(position.Latitude, CultureInfo.InvariantCulture)) / 180);
                double longitude = ((Math.PI * Convert.ToDouble(position.Longitude, CultureInfo.InvariantCulture)) / 180);

                var list = healthUnits
                            .Select(x => new HealthUnit
                            {
                                Name = x.Name,
                                Phone = x.Phone,
                                LinkEN = x.LinkEN,
                                LinkPT = x.LinkPT,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                                Address = x.Address,
                                SpecialtiesEN = x.SpecialtiesEN,
                                SpecialtiesES = x.SpecialtiesES,
                                SpecialtiesPT = x.SpecialtiesPT,
                                Distance = (3939 * Math.Acos(
                                                            Math.Cos(latitude) * Math.Cos((Math.PI * Convert.ToDouble(x.Latitude, CultureInfo.InvariantCulture)) / 180) *
                                                            Math.Cos(((Math.PI * Convert.ToDouble(x.Longitude, CultureInfo.InvariantCulture)) / 180) - longitude) +
                                                            Math.Sin(latitude) * Math.Sin(((Math.PI * Convert.ToDouble(x.Latitude, CultureInfo.InvariantCulture)) / 180)))),
                            }).OrderBy(x => x.Distance).ToList();

                foreach (var item in list)
                    item.DistanceDescription = item.Distance < 1 ? (item.Distance * 1000) + "metros" : item.Distance + "km";

                return View(healthUnits);
            }

            return View();
        }
    }
}