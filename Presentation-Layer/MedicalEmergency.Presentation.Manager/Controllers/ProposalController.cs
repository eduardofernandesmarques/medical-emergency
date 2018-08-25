using AutoMapper;
using PagedList;
using MedicalEmergency.Domain.Entities.Leads;
using MedicalEmergency.Domain.Interfaces.Repositories.Leads;
using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Domain.Interfaces.Repositories.Types;
using MedicalEmergency.Domain.Utilities;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Helpers;
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
using Entity = MedicalEmergency.Domain.Entities.Manager;
using WebControls = System.Web.UI.WebControls;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    public class ProposalController : Controller
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IBankAccountTypeRepository _bankAccountTypeRepository;
        private readonly IBankTypeRepository _bankTypeRepository;
        private readonly ICivilStatusTypeRepository _civilStatusTypeRepository;
        private readonly IClientTypeRepository _clientTypeRepository;
        private readonly IContractTypeRepository _contractTypeRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IEffectivenessTypeRepository _effectivenessTypeRepository;
        private readonly IEMISTypeRepository _emisTypeRepository;
        private readonly IEmitterCardTypeRepository _emitterCardTypeRepository;
        private readonly IInsuranceTypeRepository _insuranceTypeRepository;
        private readonly IMailTypeRepository _mailTypeRepository;
        private readonly IMidiaTypeRepository _midiaTypeRepository;
        private readonly INationalityTypeRepository _nationalityTypeRepository;
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IProfessionalClassTypeRepository _professionalClassTypeRepository;
        private readonly IResidenceTypeRepository _residenceTypeRepository;
        private readonly IScholarityTypeRepository _scholarityTypeRepository;
        private readonly ISexTypeRepository _sexTypeRepository;
        private readonly IUFTypeRepository _ufTypeRepository;
        private readonly IProposalRepository _proposalRepository;
        private readonly IRelationshipTypeRepository _relationshipTypeRepository;
        private readonly IObjectiveLoanTypeRepository _objectiveLoanTypesRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IProfileRepository _profileRepository;

        private IList<int> _storeID = new List<int>();
        private IList<int> _storekeeperID = new List<int>();

        public ProposalController(IAccountTypeRepository accountTypeRepository,
           IBankAccountTypeRepository bankAccountTypeRepository,
           IBankTypeRepository bankTypeRepository,
           ICivilStatusTypeRepository civilStatusTypeRepository,
           IClientTypeRepository clientTypeRepository,
           IContractTypeRepository contractTypeRepository,
           IDocumentTypeRepository documentTypeRepository,
           IEffectivenessTypeRepository effectivenessTypeRepository,
           IEMISTypeRepository emisTypeRepository,
           IEmitterCardTypeRepository emitterCardTypeRepository,
           IInsuranceTypeRepository insuranceTypeRepository,
           IMailTypeRepository mailTypeRepository,
           IMidiaTypeRepository midiaTypeRepository,
           INationalityTypeRepository nationalityTypeRepository,
           IPhoneTypeRepository phoneTypeRepository,
           IProfessionalClassTypeRepository professionalClassTypeRepository,
           IResidenceTypeRepository residenceTypeRepository,
           IScholarityTypeRepository scholarityTypeRepository,
           ISexTypeRepository sexTypeRepository,
           IUFTypeRepository ufTypeRepository,
           IRelationshipTypeRepository relationshipTypeRepository,
           IProposalRepository proposalRepository,
           IObjectiveLoanTypeRepository objectiveLoanTypesRepository,
           IOfferRepository offerRepository,
           IStoreRepository storeRepository,
           IProfileRepository profileRespository)
        {
            _accountTypeRepository = accountTypeRepository;
            _bankAccountTypeRepository = bankAccountTypeRepository;
            _bankTypeRepository = bankTypeRepository;
            _civilStatusTypeRepository = civilStatusTypeRepository;
            _bankAccountTypeRepository = bankAccountTypeRepository;
            _bankTypeRepository = bankTypeRepository;
            _clientTypeRepository = clientTypeRepository;
            _contractTypeRepository = contractTypeRepository;
            _documentTypeRepository = documentTypeRepository;
            _effectivenessTypeRepository = effectivenessTypeRepository;
            _emisTypeRepository = emisTypeRepository;
            _emitterCardTypeRepository = emitterCardTypeRepository;
            _insuranceTypeRepository = insuranceTypeRepository;
            _mailTypeRepository = mailTypeRepository;
            _midiaTypeRepository = midiaTypeRepository;
            _nationalityTypeRepository = nationalityTypeRepository;
            _phoneTypeRepository = phoneTypeRepository;
            _professionalClassTypeRepository = professionalClassTypeRepository;
            _residenceTypeRepository = residenceTypeRepository;
            _scholarityTypeRepository = scholarityTypeRepository;
            _sexTypeRepository = sexTypeRepository;
            _ufTypeRepository = ufTypeRepository;
            _relationshipTypeRepository = relationshipTypeRepository;
            _proposalRepository = proposalRepository;
            _objectiveLoanTypesRepository = objectiveLoanTypesRepository;
            _offerRepository = offerRepository;
            _storeRepository = storeRepository;
            _profileRepository = profileRespository;
        }

        public IList<ProposalViewModel> SearchBy(IList<ProposalViewModel> list, ProposalSearchModel search)
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
                            var property = typeof(ProposalViewModel).GetProperty(pi.Name);
                            list = list.Where(x => property.GetValue(x, null).ToString() == value).ToList();
                        }
                    }
                    else if (pi.PropertyType.IsEnum)
                    {
                        var property = typeof(ProposalViewModel).GetProperty(pi.Name);
                        var status = Enum.Parse(pi.PropertyType, pi.GetValue(search).ToString()) as Enum;

                        int enumValue = Convert.ToInt32(status);

                        if (enumValue > 0 && !string.IsNullOrEmpty(status.ToString()))
                            list = list.Where(x => property.GetValue(x, null).ToString().Contains(status.ToDescriptionString().ToUpper())).ToList();
                    }
                }
            }

            return list;
        }

        public IList<ProposalViewModel> OrderBy(string sortOrder, bool? asc, IList<ProposalViewModel> proposals)
        {
            ViewBag.nameSortParm = string.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;

            var property = typeof(ProposalViewModel).GetProperty(ViewBag.nameSortParm);

            if (property == null)
            {
                var nameSortParm = Helpers.ReflectionHelper.ReturnNamePropertyByDisplayName(typeof(ProposalViewModel), ViewBag.nameSortParm);
                property = typeof(ProposalViewModel).GetProperty(nameSortParm);
            }

            asc = asc ?? true;
            SortDirection sortDirection = asc == true ? SortDirection.Ascending : SortDirection.Descending;

            ViewBag.sortOrder = sortOrder;
            ViewBag.asc = asc.Value;

            if (sortDirection == SortDirection.Descending)
                return proposals = proposals.OrderByDescending(x => property.GetValue(x)).ToList();
            else
                return proposals = proposals.OrderBy(x => property.GetValue(x)).ToList();
        }

        public IPagedList<ProposalViewModel> Pagination(IList<ProposalViewModel> list, int? page, string sortOrder, ProposalSearchModel search, ProposalSearchModel currentFilter)
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
        public ActionResult Index(int? page, string sortOrder, bool? asc, int? status, ProposalSearchModel search, ProposalSearchModel currentSearchFilter)
        {
            if (search != null && search.Status != 0)
            {
                ViewBag.currentFilter = search;
                ViewBag.status = (int)search.Status;
            }

            if (Session != null && Session["Profile"] != null)
            {
                var profile = _profileRepository.GetById(((Entity.Profile)Session["Profile"]).ID);

                _storeID = profile.ProfileStores.Select(x => x.Store.StoreID).ToList();
                _storekeeperID = profile.ProfileStores.Select(x => x.Store.StorekeeperID).ToList();
            }

            IList<Proposal> proposals;

            if (_storeID.Count > 0 && _storekeeperID.Count > 0)
                proposals = _proposalRepository.GetAll(x => _storeID.Contains(x.StoreID) && _storekeeperID.Contains(x.StorekeeperID)).ToList();
            else
                proposals = _proposalRepository.GetAll().ToList();

            var proposalsViewModel = Mapper.Map<IList<Proposal>, IList<ProposalViewModel>>(proposals);

            proposalsViewModel = SearchBy(proposalsViewModel, search);
            proposalsViewModel = OrderBy(sortOrder, asc, proposalsViewModel);

            var pageList = Pagination(proposalsViewModel, page, sortOrder, search, currentSearchFilter);

            return View(pageList);
        }

        // GET: Proposals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Proposal proposal = _proposalRepository.GetById(id);

            _proposalRepository.LoadReferences(proposal);

            if (proposal == null)
                return HttpNotFound();

            return View(Mapper.Map<Proposal, ProposalViewModel>(proposal));
        }

        public void SetViewBag()
        {
            ViewBag.AccountTypeID = new SelectList(_accountTypeRepository.GetAll(), "ID", "Code");
            ViewBag.BankID = new SelectList(_bankTypeRepository.GetAll(), "ID", "Code");
            ViewBag.BankAccountTypeID = new SelectList(_bankAccountTypeRepository.GetAll(), "ID", "Code");
            ViewBag.BirthdayUFID = new SelectList(_ufTypeRepository.GetAll(), "ID", "Value");
            ViewBag.CivilStatusID = new SelectList(_civilStatusTypeRepository.GetAll(), "ID", "Code");
            ViewBag.CompanyUFID = new SelectList(_ufTypeRepository.GetAll(), "ID", "Value");
            ViewBag.DocumentTypeID = new SelectList(_documentTypeRepository.GetAll(), "ID", "Code");
            ViewBag.NatiolalityID = new SelectList(_nationalityTypeRepository.GetAll(), "ID", "Code");
            ViewBag.ObjectiveLoanID = new SelectList(_objectiveLoanTypesRepository.GetAll(), "ID", "Code");
            ViewBag.OfferID = new SelectList(_offerRepository.GetAll(), "ID", "OfferID");
            ViewBag.ProfessionalClassID = new SelectList(_professionalClassTypeRepository.GetAll(), "ID", "Code");
            ViewBag.ReferenceRelationshipID = new SelectList(_relationshipTypeRepository.GetAll(), "ID", "Code");
            ViewBag.ResidencialUFID = new SelectList(_ufTypeRepository.GetAll(), "ID", "Value");
            ViewBag.ScholarityID = new SelectList(_scholarityTypeRepository.GetAll(), "ID", "Code");
            ViewBag.SexID = new SelectList(_sexTypeRepository.GetAll(), "ID", "Code");
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
        public ActionResult Create(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                _proposalRepository.Add(proposal);

                return RedirectToAction("Index");
            }

            SetViewBag();

            return View(proposal);
        }

        // GET: Proposals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var proposal = _proposalRepository.GetById(id);

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
        public ActionResult Edit(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                _proposalRepository.Update(proposal);

                return RedirectToAction("Index");
            }

            SetViewBag();

            return View(proposal);
        }

        // GET: Proposals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var proposal = _proposalRepository.GetById(id);

            if (proposal == null)
                return HttpNotFound();

            return View(proposal);
        }

        // POST: Proposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _proposalRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ExportToExcel(string cpf, string proposalID, string statusID, string storeID)
        {
            if (Session != null && Session["Profile"] != null)
            {
                var profile = _profileRepository.GetById(((Entity.Profile)Session["Profile"]).ID);

                _storeID = profile.ProfileStores.Select(x => x.Store.StoreID).ToList();
                _storekeeperID = profile.ProfileStores.Select(x => x.Store.StorekeeperID).ToList();
            }

            IList<Proposal> proposals;

            if (_storeID != null && _storeID.Count() > 0 && _storekeeperID != null && _storekeeperID.Count() > 0)
                proposals = _proposalRepository.GetAll(x => _storeID.Contains(x.StoreID) && _storekeeperID.Contains(x.StorekeeperID)).ToList();
            else
                proposals = _proposalRepository.GetAll().ToList();

            proposals = FilterProposals(proposals, cpf, proposalID, statusID, storeID);

            var gridView = new WebControls.GridView();

            gridView.DataSource = Mapper.Map<IList<Proposal>, IList<ProposalExportViewModel>>(proposals);
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
            string[] list = { "ProposalID", "OfferID", "StoreID" };

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

            var ColumnValues = DataAnnotationsColumnName.GetPropertiesName(typeof(Proposal)).ToArray();

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

        public IList<Proposal> FilterProposals(IList<Proposal> list, string cpf, string proposalID, string statusID, string storeID)
        {
            if (!string.IsNullOrEmpty(cpf))
                list = list.Where(x => x.CPF == cpf).ToList();
            if (!string.IsNullOrEmpty(proposalID))
                list = list.Where(x => x.ProposalID == Convert.ToInt64(proposalID)).ToList();
            if (!string.IsNullOrEmpty(storeID))
                list = list.Where(x => x.StoreID == Convert.ToInt32(storeID)).ToList();
            if (!string.IsNullOrEmpty(statusID) && statusID != "0")
                list = list.Where(x => x.Status.Contains(statusID)).ToList();

            return list;
        }

        [HttpPost]
        public ActionResult ExportToCSV(string cpf, string proposalID, string statusID, string storeID)
        {
            if (Session != null && Session["Profile"] != null)
            {
                var profile = _profileRepository.GetById(((Entity.Profile)Session["Profile"]).ID);

                _storeID = profile.ProfileStores.Select(x => x.Store.StoreID).ToList();
                _storekeeperID = profile.ProfileStores.Select(x => x.Store.StorekeeperID).ToList();
            }

            IList<Proposal> proposals;

            if (_storeID != null && _storeID.Count() > 0 && _storekeeperID != null && _storekeeperID.Count() > 0)
                proposals = _proposalRepository.GetAll(x => _storeID.Contains(x.StoreID) && _storekeeperID.Contains(x.StorekeeperID)).ToList();
            else
                proposals = _proposalRepository.GetAll().ToList();

            proposals = FilterProposals(proposals, cpf, proposalID, statusID, storeID);

            StringWriter objStringWriter = new StringWriter();

            objStringWriter.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}",
                                        "Proposta",
                                        "Nome",
                                        "CPF",
                                        "Data Nascimento",
                                        "Telefone",
                                        "Email",
                                        "Renda Bruta",
                                        "Outras Rendas",
                                        "Valor Empréstimo",
                                        "Primeira Parcela",
                                        "Tipo Documento",
                                        "ID Oferta",
                                        "Sexo",
                                        "Status",
                                        "ID Loja",
                                        "ID Lojista",
                                        "Data Criação"));

            foreach (var item in proposals)
                objStringWriter.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}",
                                               item.ProposalID,
                                               item.Name.Replace(",", ""),
                                               item.CPF,
                                               item.Birthday,
                                               item.Phone,
                                               item.Email,
                                               item.GrossIncome,
                                               item.OtherIncomesValue,
                                               item.PurchaseValue,
                                               item.FirstInstallmentMaturity,
                                               item.DocumentType != null ? item.DocumentType.Description : "-----",
                                               item.OfferID,
                                               item.Sex != null ? item.Sex.Description : "-----",
                                               item.Status,
                                               item.StoreID,
                                               item.StorekeeperID,
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
