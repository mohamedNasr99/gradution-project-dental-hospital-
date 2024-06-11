using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public SuperAdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager
                                            ,ApplicationDbContext dbContext)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
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

        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(AddingAdminDTO addingAdminDTO)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.Email = addingAdminDTO.Email;
                user.UserName = addingAdminDTO.UserName;

                IdentityResult result = await userManager.CreateAsync(user, addingAdminDTO.Password);

                IdentityResult roleResult = await userManager.AddToRoleAsync(user, "Admin");


                if (result.Succeeded && roleResult.Succeeded)
                {
                    return Ok("The Assign and Create are successful.");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);

            }

            return BadRequest(ModelState);
        }

        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> RemoveRole(string RoleName)
        {
            if (RoleName != null)
            {
                IdentityRole role = await roleManager.FindByNameAsync(RoleName);

                if (role != null)
                {
                    IdentityResult result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        await dbContext.SaveChangesAsync();
                        return Ok("عمليه الحذف تم ي مدير");
                    }

                    return BadRequest("عمليه الحذف متمتش ي مدير");
                }

                return BadRequest("There is no role with this name or role is not found");
            }

            return BadRequest("RoleName field is null");
        }

    }
}
