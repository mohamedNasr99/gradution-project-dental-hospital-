using DentalHospital.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Receptionist")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService receptionistService;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            this.receptionistService = receptionistService;
        }

        [HttpPost("CheckPay")]
        public IActionResult CheckPay(string code)
        {
           bool result = receptionistService.CheckPay(code);

            if (result == false)
            {
                return BadRequest("There is no user with this code.");
            }
            
            return Ok("Ok, The payment process is successful.");
        }
    }
}
