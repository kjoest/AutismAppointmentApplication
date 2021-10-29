using AutismAppointmentApp.Models.PatientModels;
using AutismAppointmentApp.Services.PatientServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutismAppointmentApp.WebMVC.Controllers.PatientController
{
    public class PatientController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            var service = CreatePatientService();
            var model = service.GetAllPatients();
            return View(model);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreatePatientService();

            if (service.CreatePatient(model))
            {
                TempData["SaveResult"] = "The patient was added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The patient could not be added... Try again later.");
            return View(model);
        }

        // GET: Student/{id}
        public ActionResult Details(int id)
        {
            var service = CreatePatientService();
            var model = service.GetPatientById(id);

            if (model == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(model);
        }

        // GET: Student/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePatientService();
            var model = service.GetPatientById(id);

            return View(model);
        }

        // POST: Student/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePatientService();
            service.DeletePatient(id);
            TempData["SaveResult"] = "The patient was deleted successfully.";
            return RedirectToAction("Index");
        }

        //GET: Student/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreatePatientService();
            var entity = service.GetPatientById(id);
            var model =
                new PatientEdit
                {
                    PatientId = entity.PatientId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Gender = entity.Gender,
                    TherapyNeeded = entity.TherapyNeeded,
                    HasTherapy = entity.HasTherapy
                };

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.PatientId != id)
            {
                ModelState.AddModelError("", "ID does not match.");
                return View(model);
            }

            var service = CreatePatientService();

            if (service.UpdatePatient(model))
            {
                TempData["SaveResult"] = "The patient was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The patient could not be updated... Try again later.");
            return View(model);
        }

        private PatientService CreatePatientService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PatientService(userId);
            return service;
        }
    }
}