using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using todo_api.Dtos.Account;
using todo_api.Interfaces;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public Account(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try {
                if(!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var user = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if(result.Succeeded) {
                    var role = await _userManager.AddToRoleAsync(user, "User");

                    if(role.Succeeded) {
                        return Ok(new NewUserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.createToken(user)
                        });
                    }
                    else {
                        return BadRequest(role.Errors);
                    }
                }

                return BadRequest(result.Errors);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}