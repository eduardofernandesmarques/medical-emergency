using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MedicalEmergency.Infrastructure.Data.Repository
{
    public class HealthUnitRepository : Repository<HealthUnit>, IHealthUnitRepository
    {
        public IList<HealthUnit> DitanceReorder(double latitude, double longitude)
        {
            var list = GetAll()
                    .Select(x => new HealthUnit
                    {
                        Name = x.Name,
                        Phone = x.Phone,
                        LinkEN = x.LinkEN,
                        LinkPT = x.LinkPT,
                        Latitude = x.Latitude.Replace(",", "."),
                        Longitude = x.Longitude.Replace(",", "."),
                        Address = x.Address,
                        SpecialtiesEN = x.SpecialtiesEN,
                        SpecialtiesES = x.SpecialtiesES,
                        SpecialtiesPT = x.SpecialtiesPT,
                        Distance = (3939 * Math.Acos(
                                                       Math.Cos(latitude) * Math.Cos((Math.PI * Convert.ToDouble(x.Latitude.Replace(",", "."), CultureInfo.InvariantCulture)) / 180) *
                                                       Math.Cos(((Math.PI * Convert.ToDouble(x.Longitude.Replace(",", "."), CultureInfo.InvariantCulture)) / 180) - longitude) +
                                                       Math.Sin(latitude) * Math.Sin(((Math.PI * Convert.ToDouble(x.Latitude.Replace(",", "."), CultureInfo.InvariantCulture)) / 180))))
                    }).OrderBy(x => x.Distance).ToList();

            return list;
        }
    }
}
