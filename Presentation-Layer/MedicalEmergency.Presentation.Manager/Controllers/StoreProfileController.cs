using AutoMapper;
using PagedList;
using MedicalEmergency.Domain.Entities.Manager;
using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Models.StoreProfile;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    [Authorize(Roles = "Admin")]
    public class StoreProfileController : Controller
    {
        private readonly IStoreProfileRepository _storeProfileRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IProfileRepository _profileRepository;

        public StoreProfileController(IStoreProfileRepository storeProfileRepository, IStoreRepository storeRepository, IProfileRepository profileRepository)
        {
            _storeProfileRepository = storeProfileRepository;
            _storeRepository = storeRepository;
            _profileRepository = profileRepository;
        }


        // GET: StoreProfiles
        public ActionResult Index(int? page)
        {
            var list = Mapper.Map<IList<StoreProfile>, IList<StoreProfileViewModel>>(_storeProfileRepository.GetAll().ToList());

            int pageSize = 10;

            return View(list.ToPagedList(page ?? 1, pageSize));
        }

        // GET: StoreProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var storeProfile = Mapper.Map<StoreProfile, StoreProfileViewModel>(_storeProfileRepository.GetById(id));

            if (storeProfile == null)
                return HttpNotFound();
            
            return View(storeProfile);
        }

        // GET: StoreProfiles/Create
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name");
            ViewBag.StoreID = new SelectList(_storeRepository.GetAll(), "ID", "Name");

            return View();
        }

        // POST: StoreProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreProfileViewModel storeProfile)
        {
            if (ModelState.IsValid)
            {
                _storeProfileRepository.Add(Mapper.Map<StoreProfileViewModel, StoreProfile>(storeProfile));

                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name", storeProfile.ProfileID);
            ViewBag.StoreID = new SelectList(_storeRepository.GetAll(), "ID", "Name", storeProfile.StoreID);

            return View(storeProfile);
        }

        // GET: StoreProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var storeProfile = Mapper.Map<StoreProfile, StoreProfileViewModel>(_storeProfileRepository.GetById(id));

            if (storeProfile == null)
                return HttpNotFound();
            
            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name", storeProfile.ProfileID);
            ViewBag.StoreID = new SelectList(_storeRepository.GetAll(), "ID", "Name", storeProfile.StoreID);

            return View(storeProfile);
        }

        // POST: StoreProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreProfileViewModel storeProfile)
        {
            if (ModelState.IsValid)
            {
                _storeProfileRepository.Update(Mapper.Map<StoreProfileViewModel, StoreProfile>(storeProfile));

                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(_profileRepository.GetAll(), "ID", "Name", storeProfile.ProfileID);
            ViewBag.StoreID = new SelectList(_storeRepository.GetAll(), "ID", "Name", storeProfile.StoreID);
            return View(storeProfile);
        }

        // GET: StoreProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var storeProfile = Mapper.Map<StoreProfile, StoreProfileViewModel>(_storeProfileRepository.GetById(id));

            if (storeProfile == null)
                return HttpNotFound();
            
            return View(storeProfile);
        }

        // POST: StoreProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _storeProfileRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }
    }
}
