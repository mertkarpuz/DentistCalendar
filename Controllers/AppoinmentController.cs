using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DentistCalendar.Data;
using DentistCalendar.Data.Entity;
using DentistCalendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentistCalendar.Controllers
{
    public class AppoinmentController : Controller
    {
        private ApplicationDbContext _context;
        public AppoinmentController(ApplicationDbContext context)
        {
            _context = context;
        }


        public JsonResult GetAppoinments()
        {
            var model = _context.Appointments
                .Include(x => x.User).Select(x => new AppointmentViewModel()
                {
                    Id = x.Id,
                    Dentist = x.User.Name + " " + x.User.Surname,
                    PatientName = x.PatientName,
                    PatientSurname = x.PatientSurname,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Color = x.User.Color,
                    UserId = x.UserId

                });
                return Json(model);
        }


        public JsonResult GetAppoinmentByDentist(string userId = "")
        {
            var model = _context.Appointments.Where(x => x.UserId == userId)
                .Include(x => x.User).Select(x => new AppointmentViewModel() {
                    Id =x.Id,
                    Dentist = x.User.Name,
                    PatientName = x.PatientName,
                    PatientSurname = x.PatientSurname,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Color = x.User.Color,
                    UserId = x.User.Id
                });

            return Json(model);
        }





        [HttpPost]
        public JsonResult AddOrUpdateAppoinment(AddOrUpdateAppoinmentModel model)
        {
            if (model.Id == 0)
            {
                Appointment entity = new Appointment()
                {
                    CreatedDate = DateTime.Now,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    PatientName = model.PatientName,
                    PatientSurname = model.PatientSurname,
                    Description = model.Description,
                    UserId = model.UserId

                };

                _context.Add(entity);
                _context.SaveChanges();

            }

            else
            {
                var entity = _context.Appointments.SingleOrDefault(x => x.Id == model.Id);
                if (entity == null)
                {
                    return Json("Güncellenecek veri bulunamadı!");
                }
                entity.UpdatedTime = DateTime.Now;
                entity.PatientName = model.PatientName;
                entity.PatientSurname = model.PatientSurname;
                entity.Description = model.Description;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.UserId = model.UserId;

                _context.Update(entity);
                _context.SaveChanges();
            }

            return Json("200");
        }




        public JsonResult DeleteAppoinment(int id = 0)
        {
            var entity = _context.Appointments.Single(x => x.Id == id);
            if (entity == null)
            {
                return Json("Kayıt Bulunamadı!");
            }
            _context.Remove(entity);
            _context.SaveChanges();
            return Json("200");
        }

    }
}
