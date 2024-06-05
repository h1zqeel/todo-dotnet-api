using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<User> _signInManager;
        public Account(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try {
                if(!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

                if(user == null) {
                    return Unauthorized("Invalid username or password");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if(!result.Succeeded) {
                    return Unauthorized("Invalid username or password");
                }
                
                return Ok(new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.createToken(user)
                });

                return BadRequest("Invalid username or password");
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
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