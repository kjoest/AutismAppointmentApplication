using AutismAppointmentApp.Models.TherapistModels;
using AutismAppointmentApp.Services.TherapistServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutismAppointmentApp.WebMVC.Controllers.TherapistController
{
    public class TherapistController : Controller
    {
        // GET: Therapist
        public ActionResult Index()
        {
            var service = CreateTherapistService();
            var model = service.GetAllTherapists();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TherapistCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateTherapistService();

            if (service.CreateTherapist(model))
            {
                TempData["SaveResult"] = "The therapist was added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The therapist could not be added... Try again later.");

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            var service = CreateTherapistService();
            var model = service.GetTherapistById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTherapistService();
            var entity = service.GetTherapistById(id);
            var model =
                new TherapistEdit
                {
                    TherapistId = entity.TherapistId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    TherapySpecialist = entity.TherapySpecialist,
                    IsCertified = entity.IsCertified
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TherapistEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.TherapistId != id)
            {
                ModelState.AddModelError("", "ID does not match.");
                return View(model);
            }

            var service = CreateTherapistService();

            if (service.UpdateTherapist(model))
            {
                TempData["SaveResult"] = "The therapist was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The therapist could not be updated... Try again later. ");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateTherapistService();
            var model = service.GetTherapistById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTherapistService();
            service.DeleteTherapist(id);
            TempData["SaveResult"] = "The therapist was deleted";
            return RedirectToAction("Index");
        }

        private TherapistService CreateTherapistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TherapistService(userId);
            return service;
        }
    }
}