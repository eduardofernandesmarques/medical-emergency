using AutoMapper;
using PagedList;
using MedicalEmergency.Domain.Entities.Leads;
using MedicalEmergency.Domain.Interfaces.Repositories.Leads;
using MedicalEmergency.Presentation.Manager.Filters;
using MedicalEmergency.Presentation.Manager.Models.Offer;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MedicalEmergency.Presentation.Manager.Controllers
{
    [CustomAuthorize()]
    public class OfferController : Controller
    {
        private readonly IOfferRepository _offerRepository;

        public OfferController(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        // GET: Offers
        public ActionResult Index(int? page)
        {
            var list = Mapper.Map<IList<Offer>, IList<OfferViewModel>>(_offerRepository.GetAll().ToList());

            int pageSize = 10;

            return View(list.ToPagedList(page ?? 1, pageSize));
        }

        // GET: Offers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var offer = Mapper.Map<Offer, OfferViewModel>(_offerRepository.GetById(id));

            if (offer == null)
                return HttpNotFound();
            
            return View(offer);
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferViewModel offer)
        {
            if (ModelState.IsValid)
            {
                _offerRepository.Add(Mapper.Map<OfferViewModel, Offer>(offer));

                return RedirectToAction("Index");
            }

            return View(offer);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var offer = Mapper.Map<Offer, OfferViewModel>(_offerRepository.GetById(id));

            if (offer == null)
                return HttpNotFound();
            
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OfferViewModel offer)
        {
            if (ModelState.IsValid)
            {
                _offerRepository.Update(Mapper.Map<OfferViewModel, Offer>(offer));

                return RedirectToAction("Index");
            }

            return View(offer);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var offer = Mapper.Map<Offer, OfferViewModel>(_offerRepository.GetById(id));

            if (offer == null)
                return HttpNotFound();
            
            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _offerRepository.DeleteByID(id);

            return RedirectToAction("Index");
        }
    }
}
