using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
{
    public class EditRecordController : Controller
    {
        private readonly DataManager _dataManager;

        public EditRecordController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PhoneBookRecord phoneBookRecord = _dataManager.PhoneBookRecords.GetPhoneBookRecordById(id);
            return View(phoneBookRecord);
        }

        [HttpPut]
        [HttpPost]
        [HttpDelete]
        public IActionResult Edit(PhoneBookRecord model)
        {
            if (ModelState.IsValid)
            {
                _dataManager.PhoneBookRecords.EditPhoneBookRecord(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
