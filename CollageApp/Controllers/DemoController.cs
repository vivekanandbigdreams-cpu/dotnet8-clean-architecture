using CollageApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IMyLogger _mylogger;

        // 1. Strongly coupled/tightly coupled
        //public DemoController()
        //{
        //    _mylogger = new LogToFile();
        //}

         //2. Loosely coupled
        public DemoController(IMyLogger mylogger)
        {
            _mylogger = mylogger;
        }


        [HttpGet]
        public ActionResult Index()
        {
            _mylogger.Log("Index method started");

            return Ok(0);
        }


    }
}
