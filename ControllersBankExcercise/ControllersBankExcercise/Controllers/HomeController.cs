using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;

namespace ControllersBankExcercise.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank");
        }

        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            var account = new
            {
                accountNumber = 1001,
                accountHolderName = "Example Name",
                currentBalance = 5000
            };
            return Json(account);
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("report.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber:int}")]
        public IActionResult GetBalance()
        {
            
            if(Request.RouteValues.ContainsKey("accountNumber"))
            {
                string? accountNumber = Request.RouteValues["accountNumber"].ToString();
                if (!string.IsNullOrEmpty(accountNumber))
                {
                    if(accountNumber == "1001")
                    {
                        return Content("5000");
                    }
                    else
                    {
                        return BadRequest("Account Number should be 1001");
                    }
                }
                else
                {
                    return BadRequest("Account Number should be 1001");
                }
            }
            else
            {
                return NotFound("Account Number should be supplied");
            }
        }
    }
}
