using DentalHospital.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPut("AddPermissibleCases")]
        public IActionResult AddPermissibleCases(int Cases)
        {
            int result = adminService.AddPermissibleCases(Cases);

            if (result == 1)
            {
                return Ok("Cases is added.");
            }

            return BadRequest("Failed to add Cases.");
        }
    }
}
