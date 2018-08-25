using AutoMapper;
using PagedList;
using Entity = MedicalEmergency.Domain.Entities.Manager;
using MedicalEmergency.Domain.Interfaces.Repositories.Manager;
using MedicalEmergency.Presentation.Manager.Models.Profile;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MedicalEmergency.Presentation.Manager.Filters;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    [Authorize(Roles = "Admin")]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // GET: Profiles
        public ActionResult Index(int? page)
        {
            var list = Mapper.Map<IList<Entity.Profile>, IList<ProfileViewModel>>(_profileRepository.GetAll().ToList());

            int pageSize = 10;

            return View(list.ToPagedList(page ?? 1, pageSize));
        }

        // GET: Profiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = Mapper.Map<Entity.Profile, ProfileViewModel>(_profileRepository.GetById(id));

            if (profile == null)
                return HttpNotFound();
            
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileRepository.Add(Mapper.Map<ProfileViewModel, Entity.Profile>(profile));

                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = Mapper.Map<Entity.Profile, ProfileViewModel>(_profileRepository.GetById(id));

            if (profile == null)
                return HttpNotFound();
            
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                _profileRepository.Update(Mapper.Map<ProfileViewModel, Entity.Profile>(profile));

                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = Mapper.Map<Entity.Profile, ProfileViewModel>(_profileRepository.GetById(id));

            if (profile == null)
                return HttpNotFound();
            
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _profileRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }
    }
}
