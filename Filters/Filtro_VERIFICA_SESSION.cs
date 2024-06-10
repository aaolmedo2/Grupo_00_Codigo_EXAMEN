﻿using PyoyectoTest.Controllers;
using PyoyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PyoyectoTest.Filters
{
    public class Filtro_VERIFICA_SESSION : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var objUser = (ApplicationUser)HttpContext.Current.Session["User"];
            if (objUser == null)
            {
                if (filterContext.Controller is AccountController == false)
                {
                    filterContext.HttpContext.Response.RedirectToRoutePermanent("Default", new { controller = "Account", action = "Login" });

                }
            }
            else
            {
                if (filterContext.Controller is AccountController == true)
                {
                    filterContext.HttpContext.Response.RedirectToRoutePermanent("Default", new { controller = "Home", action = "Index" });

                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}