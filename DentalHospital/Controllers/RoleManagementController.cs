using DentalHospital.Data;
using DentalHospital.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class RoleManagementController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleManagementController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleDTO roleDTO)
        {
            if (ModelState.IsValid == true)
            {
                bool isFound = await roleManager.RoleExistsAsync(roleDTO.Name);
                if (isFound == true)
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
                else
                {
                    IdentityRole identityRole = new IdentityRole();
                    identityRole.Name = roleDTO.Name;
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return Ok("Role is added");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return BadRequest(ModelState);

        }
    }
}
