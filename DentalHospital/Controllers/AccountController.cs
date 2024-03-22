using DentalHospital.Data;
using DentalHospital.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("StudentProfessorRegister")]   // Register for Student and Professor
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public async Task<IActionResult> StudentProfessorRegister(StudentProfessorRegisterDTO studentProfessorRegisterDTO)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = studentProfessorRegisterDTO.UserName;
                user.Email = studentProfessorRegisterDTO.Email;
                user.Clinic = studentProfessorRegisterDTO.Clinic;
                user.Role = studentProfessorRegisterDTO.Role;
                user.Round = studentProfessorRegisterDTO.Round;

               IdentityResult result = await userManager.CreateAsync(user, studentProfessorRegisterDTO.Password);

                IdentityResult roleResult = await userManager.AddToRoleAsync(user, user.Role);

                if(!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
                }

                if (result.Succeeded && roleResult.Succeeded)
                {
                    return Ok("عملية التسجيل ناجحه");
                }

                return BadRequest(ModelState);

            }
            return BadRequest(ModelState);
        }



        [HttpPost("ReceptionistRegister")]   //Register for Receptionist
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> ReceptionistRegister(ReceptionistRegisterDTO receptionistRegisterDTO)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = receptionistRegisterDTO.UserName;
                user.Email = receptionistRegisterDTO.Email;

                IdentityResult result = await userManager.CreateAsync(user, receptionistRegisterDTO.Password);

                IdentityResult roleResult = await userManager.AddToRoleAsync(user, "Receptionist");

                if(!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
                }

                if (result.Succeeded && roleResult.Succeeded)
                {
                    return Ok("عملية التسجيل ناجحه");
                }

                return BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == true)
            {
               ApplicationUser user = await userManager.FindByNameAsync(loginDTO.UserName);

                if (user != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

                    SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    bool isFound = await userManager.CheckPasswordAsync(user, loginDTO.Password);

                    if(isFound == true)
                    {
                        JwtSecurityToken token = new JwtSecurityToken(
                                issuer: configuration["JWT:Issuer"],
                                audience: configuration["JWT:Audience"],
                                claims: claims,
                                expires: DateTime.Now.AddMonths(1),
                                signingCredentials: credentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }

                    return Unauthorized("كلمة السر غلط يغالي/ة او لا يوجد حساب بهذا الايميل");
                }

                return Unauthorized("قد يكون الحساب غلط او هذا الحساب غير مسجل وفي هذه الحاله يجب عليك التسجيل اولا بهذا الحساب جدع حبيبي ");
            }
            return Unauthorized(ModelState);
        }


        [HttpPost("Logout")]   //in front end , we will remove token from client (browzer) 
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            return Ok("تمام طلعت");
        }

        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (ModelState.IsValid == true)
            {
                var user = await userManager.GetUserAsync(User);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                IdentityResult result = await userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, 
                    changePasswordDTO.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await signInManager.RefreshSignInAsync(user);

                return Ok("Password changed successfully.");

            }

            return BadRequest(ModelState);
        }
        

    }
}
