using api.multitracks.com.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.multitracks.com.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ApiResult(IServiceResponse sr)
        {
            return Ok(sr);
        }

        protected IActionResult ApiResult(IServiceResponse sr, string message)
        {
            if (sr.Status)
            {
                sr.Message = message;
            }

            return Ok(sr);
        }

        protected IActionResult Plain(string msg)
        {
            return Ok(msg);
        }
    }
}
