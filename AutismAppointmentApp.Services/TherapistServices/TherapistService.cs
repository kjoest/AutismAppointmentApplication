using AutismAppointmentApp.Data;
using AutismAppointmentApp.Data.TherapistData;
using AutismAppointmentApp.Models.TherapistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Services.TherapistServices
{
    public class TherapistService : ITherapistService
    {
        public bool CreateTherapist(TherapistCreate model, string path)
        {
            var entity = new Therapist()
            {
                OwnerId = Guid.Parse(model.UserId),
                FirstName = model.FirstName,
                LastName = model.LastName,
                TherapySpecialist = model.TherapySpecialist,
                IsCertified = model.IsCertified,
                CreatedUtc = DateTimeOffset.Now,
                ImagePath = path
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Therapists.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<TherapistListDetail> GetAllTherapists(string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Therapists
                    .Where(t => t.OwnerId == guid)
                    .Select(
                        t =>
                            new TherapistListDetail
                            {
                                TherapistId = t.TherapistId,
                                FirstName = t.FirstName,
                                LastName = t.LastName,
                                TherapySpecialist = t.TherapySpecialist,
                                IsCertified = t.IsCertified,
                                CreatedUtc = t.CreatedUtc,
                                ModifiedUtc = t.ModifiedUtc
                            });

                return query.ToArray();
            }
        }

        public TherapistDetail GetTherapistById(int id, string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Therapists
                    .Single(t => t.TherapistId == id && t.OwnerId == guid);
                return new TherapistDetail
                {
                    TherapistId = entity.TherapistId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    TherapySpecialist = entity.TherapySpecialist,
                    IsCertified = entity.IsCertified,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc,
                    ImagePath = entity.ImagePath
                };
            }
        }

        public bool UpdateTherapist(TherapistEdit model)
        {
            var guid = Guid.Parse(model.UserId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Therapists
                    .Single(t => t.TherapistId == model.TherapistId && t.OwnerId == guid);

                entity.TherapistId = model.TherapistId;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.TherapySpecialist = model.TherapySpecialist;
                entity.IsCertified = model.IsCertified;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteTherapist(int id, string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Therapists
                    .Single(t => t.TherapistId == id && t.OwnerId == guid);

                ctx.Therapists.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
