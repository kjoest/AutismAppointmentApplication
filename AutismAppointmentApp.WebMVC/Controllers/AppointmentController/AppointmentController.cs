using AutismAppointmentApp.Data;
using AutismAppointmentApp.Models.AppointmentModels;
using AutismAppointmentApp.Services.AppointmentServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutismAppointmentApp.WebMVC.Controllers.AppointmentController
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        // GET: Appointment
        public ActionResult Index()
        {
            var model = _service.GetAllAppointments(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult Create()
        {
            var viewModel = new AppointmentCreate();
            viewModel.Patients = _db.Patients.Select(student => new SelectListItem
            {
                Text = student.FirstName + " " + student.LastName,
                Value = student.PatientId.ToString()
            });

            viewModel.Therapists = _db.Therapists.Select(therapist => new SelectListItem
            {
                Text = therapist.FirstName + " " + therapist.LastName,
                Value = therapist.TherapistId.ToString()
            });

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserId = User.Identity.GetUserId();

            if (_service.CreateAppointment(model))
            {
                TempData["SaveResult"] = "Your appointment was scheduled.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Your appointment could not be scheduled... Try again later.");
            }

            var viewModel = new AppointmentCreate();
            viewModel.Patients = _db.Patients.Select(student => new SelectListItem
            {
                Text = student.FirstName + " " + student.LastName,
                Value = student.PatientId.ToString()
            });

            viewModel.Therapists = _db.Therapists.Select(therapist => new SelectListItem
            {
                Text = therapist.FirstName + " " + therapist.LastName,
                Value = therapist.TherapistId.ToString()
            });

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = _service.GetAppointmentById(id, User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var entity = _service.GetAppointmentById(id, User.Identity.GetUserId());

            var model = new AppointmentEdit
            {
                AppointmentId = entity.AppointmentId,
                DateOfAppointment = entity.DateOfAppointment,
                DetailOfAppointment = entity.DetailOfAppointment,
                TherapistId = entity.TherapistId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppointmentEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.AppointmentId != id)
            {
                ModelState.AddModelError("", "ID does not match.");
                return View(model);
            }

            model.UserId = User.Identity.GetUserId();

            if (_service.UpdateAppointment(model))
            {
                TempData["SaveResult"] = "Your appointment was changed.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your appointment could not be changed... Try again later.");

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var model = _service.GetAppointmentById(id, User.Identity.GetUserId());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            _service.DeleteAppointment(id, User.Identity.GetUserId());
            TempData["SaveResult"] = "Your appointment was canceled.";
            return RedirectToAction("Index");
        }
    }
}