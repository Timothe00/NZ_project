﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using New_Zealand.webApi.Models.DTO;

namespace New_Zealand.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager) 
        { 
            this._userManager = userManager;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult =  await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                //add role to this user
                if (registerRequestDto.Roles !=null && registerRequestDto.Roles.Any())
                {
                   identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                   if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
                }
                
            }
            return BadRequest("Something went wrong");
        }
    }
}
