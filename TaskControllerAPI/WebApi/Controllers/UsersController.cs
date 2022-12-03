using Application.Dtos.UserDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public UsersController(IUsersService usersService, ITokenService tokenService, IConfiguration configuration) 
        {       
            _usersService = usersService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> UserRegister(UserRegistrationDto registrationDto)
        {

            var result = await _usersService.AddUser(registrationDto);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true }); ;
        }

        //[HttpPost("EmailConfirmation")]
        //public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        //{           
        //    var result = await _usersService.ConfirmEmail(email, token);
        //    if (!result.Succeeded)
        //    {
        //        var errors = result.Errors.Select(e => e.Description);

        //        return BadRequest(new { Errors = errors });
        //    }

        //    return Ok("Email confirmed");
        //}

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            await _usersService.ForgotPassword(forgotPasswordDto);
            return Ok("Email has been sent");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await _usersService.ResetPassword(resetPasswordDto);
   
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(DeleteAccountDto deleteAccountDto)
        {
            var result = await _usersService.DeleteAccount(deleteAccountDto);
            if (result.IsSuccessfulDelete)
                return Ok("Delete was succesful");

            return BadRequest();
        }


        [HttpPost]
        [Route("googleRegister")]
        public async Task<IActionResult> GoogleRegister(ExternalAuthDto externalAuthDto)
        {
            var token = await _usersService.GoogleAuthentication(externalAuthDto);

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto loginDto)
        {
            var hex = await _usersService.GetUser(loginDto);

            //var token = _tokenService.GenerateJWT(user, _configuration);

            return Ok(hex);
        }
    }
}
