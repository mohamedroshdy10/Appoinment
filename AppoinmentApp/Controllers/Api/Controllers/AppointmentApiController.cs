using AppoinmentApp.Models.ViewModels;
using AppoinmentApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppoinmentApp.Controllers.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppointmentApiController :ControllerBase
    {
        private readonly IAppointmentRepostery _repoAppoint;
        //private readonly HttpContextAccessor _httpContextAccessor;
        //private readonly string _user_Id;
        //private readonly string _role_Id;
        #region Fildes
        public AppointmentApiController(IAppointmentRepostery repoAppoint)
        {
            this._repoAppoint = repoAppoint;
           // this._httpContextAccessor = httpContextAccessor;
            //this._user_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //this._role_Id = User.FindFirstValue(ClaimTypes.Role);
            //_role_Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            //_user_Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        #endregion
        public IActionResult Index()
        {
            return Ok();
        }
        #region Ajax
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData([FromBody]AppointmentViewModel mdl)
        {
            var r = Request;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mdl.AdminId = userId;
            var data =  _repoAppoint.AddUpdate(mdl);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetClanderDate")]
        public async Task<IActionResult> GetClanderDate([FromQuery]string doctorId)
        {
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserRole = User.FindFirstValue(ClaimTypes.Role);

            if (UserRole == Helpers.Hepler.Patinet)
            {
                var data = await _repoAppoint.GetAppointmentsForPatient(UserID);
                return Ok(data);
            }
            else if(UserRole == Helpers.Hepler.Doctor)
            {
                var data = await _repoAppoint.GetAppointmentsForDoctor(UserID);
                return Ok(data);
            }
            else
            {
                var data = await _repoAppoint.GetAppointmentsForDoctor(doctorId);
                return Ok(data);
            }
        }
        #endregion



    }
}
