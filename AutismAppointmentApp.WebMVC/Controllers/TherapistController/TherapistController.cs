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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TherapistCreate model, HttpPostedFileBase file)
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
                path = "Content/img/" + fileName;

                rootedPath = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                file.SaveAs(rootedPath);
            }

            if (_service.CreateTherapist(model, path))
            {
                TempData["SaveResult"] = "The therapist was added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The therapist could not be added... Try again later.");
            return View("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _service.GetTherapistById(id, User.Identity.GetUserId());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = _service.GetTherapistById(id, User.Identity.GetUserId());
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var model = _service.GetTherapistById(id, User.Identity.GetUserId());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
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