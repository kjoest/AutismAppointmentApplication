using AutismAppointmentApp.Models.TherapistModels;
using AutismAppointmentApp.Services.TherapistServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutismAppointmentApp.WebMVC.Controllers.TherapistController
{
    [Authorize]
    public class TherapistController : Controller
    {
        private readonly ITherapistService _service;

        public TherapistController(ITherapistService service)
        {
            _service = service;
        }

        // GET: Therapist
        public ActionResult Index()
        {
            var model = _service.GetAllTherapists(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TherapistCreate model, string path, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_service.CreateTherapist(model, path))
            {
                string rootedPath;
                string fileName;

                if(file != null)
                {
                    fileName = Path.GetFileName(file.FileName);
                    path = "Content/img/" + fileName;

                    rootedPath = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                }

                TempData["SaveResult"] = "The therapist was added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The therapist could not be added... Try again later.");
            return View("Index");
        }

        public ActionResult Details(int id, string path)
        {
            var model = _service.GetTherapistById(id, path, User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult Edit(int id, string path)
        {
            var entity = _service.GetTherapistById(id, path, User.Identity.GetUserId());
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

            model.UserId = User.Identity.GetUserId();

            if (_service.UpdateTherapist(model))
            {
                TempData["SaveResult"] = "The therapist was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The therapist could not be updated... Try again later. ");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id, string path)
        {
            var model = _service.GetTherapistById(id, path, User.Identity.GetUserId());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            _service.DeleteTherapist(id, User.Identity.GetUserId());
            TempData["SaveResult"] = "The therapist was deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}