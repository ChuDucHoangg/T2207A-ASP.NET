using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using T2207A_MVC.Entities;
using T2207A_MVC.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2207A_MVC.Controllers
{
    public class WebController : Controller
    {


        public IActionResult List() //neu muon mot trang nua
        {
            return View();
        }

       
    }
}

