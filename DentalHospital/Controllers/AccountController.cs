using DentalHospital.Data;
using DentalHospital.DTOs;
using DentalHospital.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
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
        private readonly ApplicationDbContext dbContext;

        public AccountController(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }

        [HttpPost("StudentProfessorRegister")]   // Register for Student and Professor
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public async Task<IActionResult> StudentProfessorRegister(StudentProfessorRegisterDTO studentProfessorRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = studentProfessorRegisterDTO.UserName,
                    Email = studentProfessorRegisterDTO.Email,
                    Clinic = studentProfessorRegisterDTO.Clinic,
                    Role = studentProfessorRegisterDTO.Role,
                    Round = studentProfessorRegisterDTO.Round
                };

                IdentityResult result = await userManager.CreateAsync(user, studentProfessorRegisterDTO.Password);

                if (result.Succeeded)
                {
                    if (studentProfessorRegisterDTO.Role == "Student")
                    {
                        Student student = new Student
                        {
                            SSN = studentProfessorRegisterDTO.SSN,
                            Name = studentProfessorRegisterDTO.Name,
                            PhoneNumber = studentProfessorRegisterDTO.Phone,
                            BirthDate = studentProfessorRegisterDTO.BirthDate,
                            Gender = studentProfessorRegisterDTO.Gender,
                            UserId = user.Id
                        };

                        await dbContext.Students.AddAsync(student);
                        await dbContext.SaveChangesAsync();
                    }
                    else if (studentProfessorRegisterDTO.Role == "Professor")
                    {
                        Professor professor = new Professor
                        {
                            SSN = studentProfessorRegisterDTO.SSN,
                            Name = studentProfessorRegisterDTO.Name,
                            PhoneNumber = studentProfessorRegisterDTO.Phone,
                            BirthDate = studentProfessorRegisterDTO.BirthDate,
                            Gender = studentProfessorRegisterDTO.Gender,
                            UserId = user.Id
                        };

                        await dbContext.Professors.AddAsync(professor);
                        await dbContext.SaveChangesAsync();
                    }

                    IdentityResult roleResult = await userManager.AddToRoleAsync(user, user.Role);

                    if (roleResult.Succeeded)
                    {
                        return Ok("عملية التسجيل ناجحه");
                    }
                    else
                    {
                        // Optionally, delete the user if role assignment fails
                        await userManager.DeleteAsync(user);
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest(ModelState);
        }




        [HttpPost("ReceptionistRegister")]   //Register for Receptionist
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> ReceptionistRegister(ReceptionistRegisterDTO receptionistRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = receptionistRegisterDTO.UserName,
                    Email = receptionistRegisterDTO.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, receptionistRegisterDTO.Password);

                if (result.Succeeded)
                {
                    Receptionist receptionist = new Receptionist
                    {
                        Name = receptionistRegisterDTO.Name,
                        PhoneNumber = receptionistRegisterDTO.Phone,
                        BirthDate = receptionistRegisterDTO.BirthDate,
                        SSN = receptionistRegisterDTO.SSN,
                        userid = user.Id
                    };

                    await dbContext.Receptionists.AddAsync(receptionist);
                    await dbContext.SaveChangesAsync();

                    IdentityResult roleResult = await userManager.AddToRoleAsync(user, "Receptionist");

                    if (roleResult.Succeeded)
                    {
                        return Ok("عملية التسجيل ناجحه");
                    }
                    else
                    {
                        await userManager.DeleteAsync(user);
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
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

                        string Token = new JwtSecurityTokenHandler().WriteToken(token);

                        //Response.Cookies.Append("AccessToken", Token,
                        //    new CookieOptions
                        //    {
                        //        Expires = DateTimeOffset.UtcNow.AddMinutes(5),
                        //        HttpOnly = true,
                        //        IsEssential = true,
                        //        Secure = true,
                        //        SameSite = SameSiteMode.None
                        //    });
                        

                        return Ok(new
                        {
                            Token = Token,
                            Roles = roles,
                            Expiration = token.ValidTo
                        }) ;
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

        [HttpPatch("ChangePassword")]
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
