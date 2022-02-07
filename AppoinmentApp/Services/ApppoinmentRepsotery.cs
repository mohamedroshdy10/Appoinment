using AppoinmentApp.Data;
using AppoinmentApp.Models;
using AppoinmentApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentApp.Services
{
    public class ApppoinmentRepsotery : IAppointmentRepostery
    {
        private readonly AppDbContext _context;

        public ApppoinmentRepsotery(AppDbContext context)
        {
            this._context = context;
        }

        public ResulteViewModel<AppointmentViewModel> AddUpdate(AppointmentViewModel mdl)
        {
            ResulteViewModel<AppointmentViewModel> resulte = new ResulteViewModel<AppointmentViewModel>()
            {

            };
            var startdate = DateTime.Parse(mdl.StartDate);
            var min = Convert.ToDouble(mdl.Duration);
            var enddate = Convert.ToDateTime(mdl.EndDate).AddMinutes(min);


            if (mdl.Id != null && mdl.Id > 0)
            {  //Update
                var entitiy = _context.Appointments.Find(mdl.Id);
                if (entitiy != null)
                {
                    entitiy.Title = mdl.Title;
                    entitiy.Description = mdl.Description;
                    entitiy.Duration = mdl.Duration;
                    entitiy.StartDate = startdate;
                    entitiy.EndDate = enddate;
                    entitiy.AdminId = mdl.AdminId;
                    entitiy.DoctorId = mdl.DoctorId;
                    entitiy.PatientId = mdl.DoctorId;
                    entitiy.IsDoctorApproved = mdl.IsDoctorApproved;
                    var Update = _context.Appointments.Update(entitiy);
                    if (Update != null)
                    {
                        resulte.ResulteEnum = ResulteEnum.Success;
                        resulte.message = "Updated Success";
                        resulte.Data = mdl;
                        resulte.StatusCode = 200;
                        _context.SaveChanges();

                    }
                    else
                    {
                        resulte.ResulteEnum = ResulteEnum.Error;
                        resulte.message = "Update Faliad";
                        resulte.Data = mdl;
                        resulte.StatusCode = 500;
                    }

                    return resulte;
                }
                else
                {
                    resulte.ResulteEnum = ResulteEnum.Error;
                    resulte.message = "Update Faliad";
                    resulte.Data = mdl;
                    resulte.StatusCode = 500;
                }


            }
            else
            {
                //Insert
                Appointment appointment = new Appointment
                {
                    Title = mdl.Title,
                    Description = mdl.Description,
                    Duration = mdl.Duration,
                    StartDate = startdate,
                    EndDate = enddate,
                    AdminId = mdl.AdminId,
                    DoctorId = mdl.DoctorId,
                    PatientId = mdl.DoctorId,
                    IsDoctorApproved = mdl.IsDoctorApproved,
                };
                var insert = _context.Appointments.Add(appointment);
                if (insert != null)
                {
                    resulte.ResulteEnum = ResulteEnum.Success;
                    resulte.message = "Saved Success";
                    resulte.Data = mdl;
                    resulte.StatusCode = 200;
                    _context.SaveChanges();

                }
                else
                {
                    resulte.ResulteEnum = ResulteEnum.Error;
                    resulte.message = "Saved Faliad";
                    resulte.Data = mdl;
                    resulte.StatusCode = 500;
                }
            }
            return resulte;
        }

        public async Task<List<DoctorViewModel>> GetAllDoctors()
        {
            //var doctors = await _context.Users.Select(x => new DoctorViewModel
            //{
            //     DoctorEmail=x.Email,
            //     DoctorName=x.FullName,
            //     Id=x.Id
            //}).ToListAsync();
            //return doctors;

            var allDoctros = (from user in _context.Users
                              join user_roles in _context.UserRoles on user.Id equals user_roles.UserId
                              join roles in _context.Roles.Where(x => x.Name == Helpers.Hepler.Doctor) on user_roles.RoleId equals roles.Id
                              select new DoctorViewModel
                              {
                                  Email = user.Email,
                                  Name = user.FullName,
                                  Id = user.Id
                              }
                              );
            return await allDoctros.ToListAsync();
        }

        public async Task<List<PatinetViewModel>> GetAllPatiens()
        {
            var allDoctros = (from user in _context.Users
                              join user_roles in _context.UserRoles on user.Id equals user_roles.UserId
                              join roles in _context.Roles.Where(x => x.Name == Helpers.Hepler.Doctor) on user_roles.RoleId equals roles.Id
                              select new PatinetViewModel
                              {
                                  Email = user.Email,
                                  Name = user.FullName,
                                  Id = user.Id
                              }
                              );
            return await allDoctros.ToListAsync();
        }

        public async Task<List<AppointmentViewModel>> GetAppointmentsForDoctor(string dotorId)
        {
            var data = await _context.Appointments.Where(x => x.DoctorId == dotorId).Select(xx => new AppointmentViewModel
            {
                Title = xx.Title,
                Description = xx.Description,
                Id = xx.Id,
                StartDate = xx.StartDate.ToString("dd/MMMM/yyyy hh:tt"),
                Duration = xx.Duration,
                EndDate = xx.EndDate.ToString("dd/MMMM/yyyy hh:tt"),
                IsDoctorApproved = xx.IsDoctorApproved

            }).ToListAsync();
            return data;
        }

        public async Task<List<AppointmentViewModel>> GetAppointmentsForPatient(string patientId)
        {

            var data = await _context.Appointments.Where(x => x.PatientId == patientId).Select(xx => new AppointmentViewModel
            {
                Title = xx.Title,
                Description = xx.Description,
                Id = xx.Id,
                StartDate = xx.StartDate.ToString("dd/MMMM/yyyy hh:tt"),
                Duration = xx.Duration,
                EndDate = xx.EndDate.ToString("dd/MMMM/yyyy hh:tt"),
                IsDoctorApproved=xx.IsDoctorApproved
            }).ToListAsync();
            return data;
        }
    }
}
