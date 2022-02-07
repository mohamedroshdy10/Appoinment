using AppoinmentApp.Models.ViewModels;
using AppoinmentApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppoinmentApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepostery _repoAppointmet;
        #region Fildes
        public AppointmentController(IAppointmentRepostery repoAppointmet)
        {
            this._repoAppointmet = repoAppointmet;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            ViewBag.DoctorList = await _repoAppointmet.GetAllDoctors();
            ViewBag.PatinetList = await _repoAppointmet.GetAllPatiens();
            return View();
        }
        //#region AJax
        //[HttpPost]
        //[Route("[controller/Add]")]
        //public async Task<JsonResult> Add(AppointmentViewModel mdl)
        //{
        //    var x = Request;
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    mdl.AdminId = userId;
        //    return Json(await _repoAppointmet.AddUpdate(mdl));
        //}
        //#endregion
      

    }
}
