using PagedList;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Models.Proposal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;
using MedicalEmergency.Domain.Interfaces.Repositories;
using MedicalEmergency.Domain.Interfaces.Repositories.Types;
using MedicalEmergency.Infrastructure.Data.Repository;
using MedicalEmergency.Infrastructure.Data.Repository.Types;
using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Presentation.Manager.Helpers;
using MedicalEmergency.Presentation.Manager.Utilities;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    public class HealthUnitController : Controller
    {
        private readonly IHealthUnitRepository _healthUnityRepository;
        private readonly IEmergencyTypeRepository _emergencyTypeRepository;
        private readonly IInstitutionTypeRepository _institutionTypeRepository;

        private IList<int> _storeID = new List<int>();
        private IList<int> _storekeeperID = new List<int>();

        public HealthUnitController()
        {
            _healthUnityRepository = new HealthUnitRepository();
            _emergencyTypeRepository = new EmergencyTypeRepository();
            _institutionTypeRepository = new InstitutionTypeRepository();
        }

        public IList<HealthUnit> SearchBy(IList<HealthUnit> list, HealthUnitSearchModel search)
        {
            IList<HealthUnit> resultList = new List<HealthUnit>();

            if (search.IsAnyNotNullOrEmpty())
            {
                foreach (PropertyInfo pi in search.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(search);

                        if (!string.IsNullOrEmpty(value))
                        {
                            var property = typeof(HealthUnit).GetProperty(pi.Name);
                            resultList = resultList.Concat(list.Where(x => property.GetValue(x, null).ToString().Contains(value)).ToList()).ToList();
                        }
                    }
                    else if(pi.PropertyType == typeof(int))
                    {
                        int value = (int)pi.GetValue(search);

                        if (value != 0)
                        {
                            var property = typeof(HealthUnit).GetProperty(pi.Name);
                            resultList = resultList.Concat(list.Where(x => property.GetValue(x, null).ToString() == value.ToString()).ToList()).ToList();
                        }
                    }
                    else if (pi.PropertyType.IsEnum)
                    {
                        var property = typeof(HealthUnit).GetProperty(pi.Name);
                        var status = Enum.Parse(pi.PropertyType, pi.GetValue(search).ToString()) as Enum;

                        int enumValue = Convert.ToInt32(status);

                        if (enumValue > 0 && !string.IsNullOrEmpty(status.ToString())) ;
                            resultList = resultList.Concat(list.Where(x => property.GetValue(x, null).ToString().Contains(status.ToDescriptionString().ToUpper())).ToList()).ToList();
                    }
                }

                return resultList.Distinct().ToList();
            }

            return list.ToList();
        }

        public IList<HealthUnit> OrderBy(string sortOrder, bool? asc, IList<HealthUnit> healthUnits)
        {
            ViewBag.nameSortParm = string.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;

            var property = typeof(HealthUnit).GetProperty(ViewBag.nameSortParm);

            if (property == null)
            {
                var nameSortParm = Helpers.ReflectionHelper.ReturnNamePropertyByDisplayName(typeof(HealthUnit), ViewBag.nameSortParm);
                property = typeof(HealthUnit).GetProperty(nameSortParm);
            }

            asc = asc ?? true;
            SortDirection sortDirection = asc == true ? SortDirection.Ascending : SortDirection.Descending;

            ViewBag.sortOrder = sortOrder;
            ViewBag.asc = asc.Value;

            if (sortDirection == SortDirection.Descending)
                return healthUnits = healthUnits.OrderByDescending(x => property.GetValue(x)).ToList();
            else
                return healthUnits = healthUnits.OrderBy(x => property.GetValue(x)).ToList();
        }

        public IPagedList<HealthUnit> Pagination(IList<HealthUnit> list, int? page, string sortOrder, HealthUnitSearchModel search, HealthUnitSearchModel currentFilter)
        {
            ViewBag.currentSort = sortOrder;

            if (search.IsAnyNotNullOrEmpty())
                page = 1;
            else
                search = currentFilter;

            ViewBag.page = page ?? 1;

            int pageSize = 10;

            return list.ToPagedList(page ?? 1, pageSize);
        }

        // GET: HealthUnit
        public ActionResult Index(int? page, string sortOrder, bool? asc, int? institutionType, int? emergencyType, HealthUnitSearchModel search, HealthUnitSearchModel currentSearchFilter)
        {
            SetViewBag();

            if (search != null && !string.IsNullOrEmpty(search.Name))
            {
                ViewBag.currentFilter = search;
                ViewBag.Name = search.Name;
            }

            IList<HealthUnit> healthUnits = _healthUnityRepository.GetAll().ToList();

            healthUnits = SearchBy(healthUnits, search);
            healthUnits = OrderBy(sortOrder, asc, healthUnits);

            var pageList = Pagination(healthUnits, page, sortOrder, search, currentSearchFilter);

            return View(pageList);
        }

        public void SetViewBag()
        {
            SelectListItem selectListItem = new SelectListItem() { Value = "0", Text = "-----" };

            var emergencyTypeList = new SelectList(_emergencyTypeRepository.GetAll(), "ID", "Description").ToList();
            var institutionTypeList = new SelectList(_institutionTypeRepository.GetAll(), "ID", "Description").ToList();

            emergencyTypeList.Insert(0, selectListItem);
            institutionTypeList.Insert(0, selectListItem);

            ViewBag.EmergencyTypeID = emergencyTypeList;
            ViewBag.InstitutionTypeID = institutionTypeList;
        }

        // GET: HealthUnits/Create
        public ActionResult Create()
        {
            SetViewBag();

            return View();
        }

        // POST: HealthUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HealthUnit healthUnit)
        {
            if (ModelState.IsValid)
            {
                _healthUnityRepository.Add(healthUnit);

                return RedirectToAction("Index");
            }

            SetViewBag();

            return View(healthUnit);
        }

        // GET: HealthUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var healthUnit = _healthUnityRepository.GetById(id);

            if (healthUnit == null)
                return HttpNotFound();

            SetViewBag();

            return View(healthUnit);
        }

        // POST: HealthUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HealthUnit healthUnit)
        {
            if (ModelState.IsValid)
            {
                _healthUnityRepository.Update(healthUnit);

                return RedirectToAction("Index");
            }

            SetViewBag();

            return View(healthUnit);
        }

        // GET: HealthUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var healthUnit = _healthUnityRepository.GetById(id);

            if (healthUnit == null)
                return HttpNotFound();

            return View(healthUnit);
        }

        // POST: HealthUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _healthUnityRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }
    }
}
