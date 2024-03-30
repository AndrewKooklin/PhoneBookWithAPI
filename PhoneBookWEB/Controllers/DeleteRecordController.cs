using Microsoft.AspNetCore.Mvc;
using PhoneBookWEB.Domain;

namespace PhoneBookWEB.Controllers
{
    public class DeleteRecordController : Controller
    {
        private readonly DataManager _dataManager;

        public DeleteRecordController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpDelete]
        [HttpPatch]
        [HttpPost]
        [HttpGet]
        public IActionResult DeleteRecord(int id)
        {
            _dataManager.PhoneBookRecords.DeletePhoneBookRecord(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
