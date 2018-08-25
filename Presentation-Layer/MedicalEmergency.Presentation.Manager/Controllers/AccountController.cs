using AutoMapper;
using PagedList;
using MedicalEmergency.Domain.Entities.Manager;
using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Domain.Utilities;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Models.Account;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;
using Entity = MedicalEmergency.Domain.Entities.Manager;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProfileRepository _profileRepository;

        public AccountController(IAccountRepository accountRepository, IProfileRepository profileRepository)
        {
            _accountRepository = accountRepository;
            _profileRepository = profileRepository;
        }

        // GET: Accounts
        public ActionResult Index(int? page)
        {
            var role = new Models.Roles();

            IList<Account> accounts;

            if (Session["Profile"] != null && role.IsUserInRole(((Entity.Profile)Session["Profile"]).Name, "Admin"))
                accounts = _accountRepository.GetAll(null, x => x.OrderBy(y => y.Profile.Name)).ToList();
            else
                accounts = _accountRepository.GetAll(null, x => x.OrderBy(y => y.Profile.Name)).Where(x => x.ID == ((Account)Session["Account"]).ID).ToList();

            var list = Mapper.Map<IList<Account>, IList<AccountViewModel>>(accounts);

            int pageSize = 10;

            return View(list.ToPagedList(page ?? 1, pageSize));
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var account = Mapper.Map<Account, AccountViewModel>(_accountRepository.GetById(id));

            if (account == null)
                return HttpNotFound();
            
            return View(account);
        }

        [Authorize(Roles = "Admin")]
        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name");

            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                account.EncryptPassword();

                _accountRepository.Add(Mapper.Map<AccountViewModel, Account>(account));

                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name", account.ProfileID);

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var account = Mapper.Map<Account, AccountViewModel>(_accountRepository.GetById(id));

            if (account == null)
                return HttpNotFound();
            
            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name", account.ProfileID);

            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                account.EncryptPassword();

                _accountRepository.Update(Mapper.Map<AccountViewModel, Account>(account));

                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name", account.ProfileID);

            return View(account);
        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var account = Mapper.Map<Account, AccountViewModel>(_accountRepository.GetById(id));

            if (account == null)
                return HttpNotFound();
            
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            _accountRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountViewModel account, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var accountInput = _accountRepository.Get(x => x.User == account.User).FirstOrDefault();

                if (accountInput != null)
                {
                    if (Equals(accountInput.Active, true))
                    {
                        if (Encrypt.VerifyMd5Hash(MD5.Create(), account.Password, accountInput.Password))
                        {
                            FormsAuthentication.SetAuthCookie(accountInput.Profile.Name, false);

                            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && returnUrl.StartsWith("/\\"))
                                return Redirect(returnUrl);

                            Session["Account"] = accountInput;

                            Session["Profile"] = _profileRepository.GetById(accountInput.ProfileID);

                            return RedirectToAction("Index", "Proposal");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Senha informada Inválida!!!");

                            return View(new AccountViewModel());
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");

                        return View(new AccountViewModel());
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-mail informado inválido!!!");

                    return View(new AccountViewModel());
                }
            }

            return View(account);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}
