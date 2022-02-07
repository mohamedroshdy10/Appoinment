using AppoinmentApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentApp.Services
{
    public interface IAppointmentRepostery
    {
        public Task<List<PatinetViewModel>> GetAllPatiens();

        public Task<List<DoctorViewModel>> GetAllDoctors();

        public ResulteViewModel<AppointmentViewModel>  AddUpdate(AppointmentViewModel mdl);

        public Task<List<AppointmentViewModel>> GetAppointmentsForDoctor(string dotorId);
        public Task<List<AppointmentViewModel>> GetAppointmentsForPatient(string patientId);

    }
}
