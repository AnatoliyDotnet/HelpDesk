using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientWebApplication.BL;
using CommonDll;
using CommonDll.Entities;

namespace ClientWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            var operationResult = ClientProvider.Login(login, password);

            if (operationResult.IsValid)
            {
                var result = operationResult.Result.Id;
                return Json(new BasicAjaxResponse
                {
                    IsSuccess = true,
                    ResponseObject = result
                });

            }
            return Json(new BasicAjaxResponse
            {
                IsSuccess = false,
                ResponseObject = operationResult.ErrorsList.FirstOrDefault()
            });
        }

        [HttpPost]
        public ActionResult RegisterClient(string email, string password, string firstName, string lastName, 
            string isCompany, string subscription, string city, string house, string street, string flat)
        {
            var client = new Client
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                IsCompany = bool.Parse(isCompany),
                Subscription = (Subscription) int.Parse(subscription),
                Address = new ClientAddress
                {
                    City = city,
                    Street = street,
                    House = house,
                    Flat = int.Parse(flat)
                },
                Role = Role.Client
            };

            var operationResult = ClientProvider.Register(client);

            if (operationResult.IsValid)
            {
                var result = operationResult;
                return Json(new BasicAjaxResponse
                {
                    IsSuccess = true,
                    ResponseObject = result
                });

            }
            return Json(new BasicAjaxResponse
            {
                IsSuccess = false,
                ResponseObject = operationResult.ErrorsList.FirstOrDefault()
            });
        }
    }
}