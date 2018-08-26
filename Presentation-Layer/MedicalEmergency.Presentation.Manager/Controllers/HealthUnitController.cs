using PagedList;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Models.Proposal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using WebControls = System.Web.UI.WebControls;
using MedicalEmergency.Domain.Interfaces.Repositories;
using MedicalEmergency.Domain.Interfaces.Repositories.Types;
using MedicalEmergency.Infrastructure.Data.Repository;
using MedicalEmergency.Infrastructure.Data.Repository.Types;
using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Presentation.Manager.Helpers;
using MedicalEmergency.Domain.Utilities;

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
            if (search.IsAnyNotNullOrEmpty())
            {
                foreach (PropertyInfo pi in search.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(search);

                        if (!string.IsNullOrEmpty(value) && (value.All(char.IsDigit) && Convert.ToInt64(value) > 0))
                        {
                            var property = typeof(HealthUnit).GetProperty(pi.Name);
                            list = list.Where(x => property.GetValue(x, null).ToString() == value).ToList();
                        }
                    }
                    else if (pi.PropertyType.IsEnum)
                    {
                        var property = typeof(HealthUnit).GetProperty(pi.Name);
                        var status = Enum.Parse(pi.PropertyType, pi.GetValue(search).ToString()) as Enum;

                        int enumValue = Convert.ToInt32(status);

                        //if (enumValue > 0 && !string.IsNullOrEmpty(status.ToString())) ;
                            //list = list.Where(x => property.GetValue(x, null).ToString().Contains(status.ToDescriptionString().ToUpper())).ToList();
                    }
                }
            }

            return list;
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

        // GET: Proposals
        public ActionResult Index(int? page, string sortOrder, bool? asc, int? status, HealthUnitSearchModel search, HealthUnitSearchModel currentSearchFilter)
        {
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

        // GET: Proposals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HealthUnit healthUnits = _healthUnityRepository.GetById(id);

            if (healthUnits == null)
                return HttpNotFound();

            return View(healthUnits);
        }

        public void SetViewBag()
        {
            ViewBag.EmergencyTypeID = new SelectList(_emergencyTypeRepository.GetAll(), "ID", "Description");
            ViewBag.InstitutionTypeID = new SelectList(_institutionTypeRepository.GetAll(), "ID", "Description");
        }

        // GET: Proposals/Create
        public ActionResult Create()
        {
            SetViewBag();

            return View();
        }

        // POST: Proposals/Create
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

        // GET: Proposals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var proposal = _healthUnityRepository.GetById(id);

            if (proposal == null)
                return HttpNotFound();

            SetViewBag();

            return View(proposal);
        }

        // POST: Proposals/Edit/5
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

        // GET: Proposals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var proposal = _healthUnityRepository.GetById(id);

            if (proposal == null)
                return HttpNotFound();

            return View(proposal);
        }

        // POST: Proposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _healthUnityRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ExportToExcel(string name, string healthUnitID, string emergencyTypeID, string institutionTypeID)
        {
            IList<HealthUnit> healthUnits = _healthUnityRepository.GetAll().ToList();
            healthUnits = FilterHealthUnits(healthUnits, name, healthUnitID, emergencyTypeID, institutionTypeID);

            var gridView = new WebControls.GridView();

            gridView.DataSource = healthUnits;
            gridView.DataBind();

            SetHeader(gridView);

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Export.csv");
            Response.ContentType = "text/csv";
            Response.Charset = "";

            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gridView.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

        public void SetHeaderGridView(WebControls.GridView gridView)
        {
            string[] list = { "HealthUnitID", "EmergencyTypeID", "InstitutionTypeID" };

            foreach (WebControls.TableCell item in gridView.HeaderRow.Cells)
            {
                if (item.Text.Contains("ID") && !list.Any(x => item.Text.Contains(x)))
                {
                    gridView.HeaderRow.Cells[gridView.HeaderRow.Cells.GetCellIndex(item)].Visible = false;

                    for (int i = 0; i < gridView.Rows.Count; i++)
                    {
                        WebControls.GridViewRow row = gridView.Rows[i];
                        row.Cells[gridView.HeaderRow.Cells.GetCellIndex(item)].Visible = false;
                    }
                }
            }

            var ColumnValues = DataAnnotationsColumnName.GetPropertiesName(typeof(HealthUnit)).ToArray();

            foreach (WebControls.TableCell item in gridView.HeaderRow.Cells)
            {
                if (item.Visible != false)
                    foreach (var value in ColumnValues)
                    {
                        item.Text = value;
                        ColumnValues = ColumnValues.Where(w => w != ColumnValues[Array.FindIndex(ColumnValues, x => x == value)]).ToArray();
                        break;
                    }
            }
        }

        public void SetHeader(WebControls.GridView gridView)
        {
            var ColumnValues = DataAnnotationsColumnName.GetPropertiesExportName(typeof(ProposalExportViewModel)).ToArray();

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells != null)
                foreach (WebControls.TableCell item in gridView.HeaderRow.Cells)
                {
                    if (item.Visible != false)
                        foreach (var value in ColumnValues)
                        {
                            item.Text = value;
                            ColumnValues = ColumnValues.Where(w => w != ColumnValues[Array.FindIndex(ColumnValues, x => x == value)]).ToArray();
                            break;
                        }
                }
        }

        public IList<HealthUnit> FilterHealthUnits(IList<HealthUnit> list, string name, string healthUnitID, string EmergencyTypeID, string InstitutionTypeID)
        {
            if (!string.IsNullOrEmpty(name))
                list = list.Where(x => x.Name == name).ToList();
            if (!string.IsNullOrEmpty(healthUnitID))
                list = list.Where(x => x.ID == Convert.ToInt64(healthUnitID)).ToList();
            if (!string.IsNullOrEmpty(InstitutionTypeID))
                list = list.Where(x => x.InstitutionTypeID == Convert.ToInt32(InstitutionTypeID)).ToList();
            if (!string.IsNullOrEmpty(EmergencyTypeID))
                list = list.Where(x => x.EmergencyTypeID == Convert.ToInt32(InstitutionTypeID)).ToList();

            return list;
        }

        [HttpPost]
        public ActionResult ExportToCSV(string name, string healthUnitID, string emergencyTypeID, string institutionTypeID)
        {
            IList<HealthUnit> healthUnits = _healthUnityRepository.GetAll().ToList();

            healthUnits = FilterHealthUnits(healthUnits, name, healthUnitID, emergencyTypeID, institutionTypeID);

            StringWriter objStringWriter = new StringWriter();

            objStringWriter.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}",
                                        "ID",
                                        "Nome",
                                        "Latitude",
                                        "Longitude",
                                        "LinkEN",
                                        "LinkPT",
                                        "Telefone",
                                        "Especialidades EN",
                                        "Especialidades ES",
                                        "Especialidades PT",
                                        "Tipo Documento",
                                        "ID Oferta",
                                        "Sexo",
                                        "Status",
                                        "ID Loja",
                                        "ID Lojista",
                                        "Data Criação"));

            foreach (var item in healthUnits)
                objStringWriter.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}",
                                               item.ID,
                                               item.Name.Replace(",", ""),
                                               item.Latitude,
                                               item.Longitude,
                                               item.LinkEN,
                                               item.LinkPT,
                                               item.Phone,
                                               item.SpecialtiesEN,
                                               item.SpecialtiesES,
                                               item.SpecialtiesPT,
                                               item.Created));

            UTF8Encoding utf8 = new UTF8Encoding(false);

            var result = utf8.GetPreamble().Concat(utf8.GetBytes(objStringWriter.ToString())).ToArray();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("Propostas.csv", Encoding.UTF8) + "");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = utf8;
            Response.Charset = "";
            Response.BinaryWrite(result);
            Response.Write(File(result, "application/csv", "Propostas.csv"));
            Response.Flush();
            Response.End();

            return View();
        }
    }
}
