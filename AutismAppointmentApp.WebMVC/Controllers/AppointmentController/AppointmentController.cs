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

        // GET: Appointment
        public ActionResult Index()
        {
            var service = CreateAppointmentService();
            var model = service.GetAllAppointments();
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

            var service = CreateAppointmentService();

            if (service.CreateAppointment(model))
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
            var service = CreateAppointmentService();
            var model = service.GetAppointmentById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateAppointmentService();
            var entity = service.GetAppointmentById(id);
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

            var service = CreateAppointmentService();

            if (service.UpdateAppointment(model))
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
            var service = CreateAppointmentService();
            var model = service.GetAppointmentById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CreateAppointmentService();
            service.DeleteAppointment(id);
            TempData["SaveResult"] = "Your appointment was canceled.";
            return RedirectToAction("Index");
        }

        private AppointmentService CreateAppointmentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AppointmentService(userId);
            return service;
        }
    }
}