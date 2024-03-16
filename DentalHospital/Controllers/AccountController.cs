using DentalHospital.Data;
using DentalHospital.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentalHospital.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("StudentProfessorRegister")]   // Register for Student and Professor
        public async Task<IActionResult> StudentProfessorRegister(StudentProfessorRegisterDTO studentProfessorRegisterDTO)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = studentProfessorRegisterDTO.Name;
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
        public async Task<IActionResult> ReceptionistRegister(ReceptionistRegisterDTO receptionistRegisterDTO)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = receptionistRegisterDTO.Name;
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
        [Authorize(Roles = "Admin, Student, Professor, Receptionist")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == true)
            {
               ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.Email);

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
                                issuer: configuration["jWT:Issuer"],
                                audience: configuration["JWT:Audiance"],
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


        [HttpPost("Logout")]   //تسجيل الخروج 
        [Authorize(Roles ="Admin, Student, Professor, Receptionist")]
        public IActionResult Logout()
        {
            return Ok("تمام طلعت");
        }


    }
}
