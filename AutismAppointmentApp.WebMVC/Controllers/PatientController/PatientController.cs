using AutismAppointmentApp.Models.PatientModels;
using AutismAppointmentApp.Services.PatientServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutismAppointmentApp.WebMVC.Controllers.PatientController
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        // GET: Patient
        public ActionResult Index()
        {
            var model = _service.GetAllPatients(User.Identity.GetUserId());
            return View(model);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Patient/Create
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientCreate model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();

            string rootedPath;
            string path = " ";
            string fileName;

            if (file != null)
            {
                fileName = Path.GetFileName(file.FileName);
                path = "/Content/img/" + fileName;

                rootedPath = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                file.SaveAs(rootedPath);
            }

            if (_service.CreatePatient(model, path))
            {

                TempData["SaveResult"] = "The patient was added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The patient could not be added... Try again later.");
            return View(model);
        }

        // GET: Patient/{id}
        public ActionResult Details(int id)
        {
            var model = _service.GetPatientById(id, User.Identity.GetUserId());

            if (model == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(model);
        }

        // GET: Patient/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id, string path)
        {
            var model = _service.GetPatientById(id, User.Identity.GetUserId());
            return View(model);
        }

        // POST: Patient/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            _service.DeletePatient(id, User.Identity.GetUserId());
            TempData["SaveResult"] = "The patient was deleted successfully.";
            return RedirectToAction("Index");
        }

        //GET: Patient/Edit/{id}
        public ActionResult Edit(int id, string path)
        {
            var entity = _service.GetPatientById(id, User.Identity.GetUserId());
            var model =
                new PatientEdit
                {
                    PatientId = entity.PatientId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Gender = entity.Gender,
                    Address = entity.Address,
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

            model.UserId = User.Identity.GetUserId();

            if (_service.UpdatePatient(model))
            {
                TempData["SaveResult"] = "The patient was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The patient could not be updated... Try again later.");
            return View(model);
        }
    }
}