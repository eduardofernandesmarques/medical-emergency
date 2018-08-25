using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new DefaultPropertiesFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
