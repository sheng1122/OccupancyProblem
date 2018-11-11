using App.Contract.DTOs;
using App.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Web.Helpers
{
    public class UserSession
    {
        private static IServiceProvider _services;

        private class Key
        {
        }

        public static IServiceProvider Service
        {
            get { return _services; }
            set
            {
                if (_services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }

                _services = value;
            }
        }

        private static ISession session
        {
            get
            {
                IHttpContextAccessor httpContext = Service.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;

                HttpContext context = httpContext?.HttpContext;

                return context.Session;
            }
        }
    }
}
