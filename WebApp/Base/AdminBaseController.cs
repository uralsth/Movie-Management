using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SQLHelper;
using System.Reflection;
using System.Security.Claims;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Base
{
    public class AdminBaseController : Controller
    {
        public string GetUserName
        {
            get
            {
                return "admin@contentder.com";
                // return User.FindFirst(ClaimTypes.Name)?.Value;
            }

        }


        public void ShowActionMessage(string message, eMessageType messageType)
        { 
            TempData["SuccessMessage"] =message;
            TempData["MessageType"] = messageType.ToString();
        }
        public List<string> GetModelStateError()
        {
            IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors);
           return modelErrors.Select(e => e.ErrorMessage).ToList();
        }
    }
}
