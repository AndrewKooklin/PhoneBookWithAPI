using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
{
    public class CreateRecordController : Controller
    {
        private readonly DataManager _dataManager;

        public CreateRecordController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PhoneBookRecord model)
        {
            if (ModelState.IsValid)
            {
                _dataManager.PhoneBookRecords.SavePhoneBookRecord(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
