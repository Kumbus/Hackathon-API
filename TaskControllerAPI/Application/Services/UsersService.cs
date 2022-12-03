using Application.Dtos.UserDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IHexIdRepository _hexIdRepository;

        public UsersService(IMapper mapper, UserManager<User> userManager, IEmailSender emailSender, ITokenService tokenService, IConfiguration configuration, IHexIdRepository hexIdRepository )
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _tokenService = tokenService;
            _configuration = configuration;
            _hexIdRepository= hexIdRepository;
        }

        public async Task<IdentityResult> AddUser(UserRegistrationDto registrationDto)
        {
            if (registrationDto == null)
                throw new InvalidCredentialsException();

            var user = _mapper.Map<User>(registrationDto);

            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            //if(result.Succeeded)
            //{
            //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    var param = new Dictionary<string, string?>
            //    {
            //        {"token", token },
            //        {"email", registrationDto.Email }
            //    };

            //    var callback = QueryHelpers.AddQueryString(registrationDto.ClientURI, param);
            //    var message = new Message(new string[] { user.Email }, "Email Confirmation token", callback);
            //    await _emailSender.SendEmailAsync(message);
            //}

            return result;
        }

        public async Task<IdentityResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new InvalidCredentialsException();

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);

            return confirmResult;
        }

        public async Task<DeleteAccountResponseDto> DeleteAccount(DeleteAccountDto deleteAccountDto)
        {
            var user = await _userManager.FindByIdAsync(deleteAccountDto.Id);
            if (user is null)
                throw new InvalidCredentialsException();

            var result = await _userManager.CheckPasswordAsync(user, deleteAccountDto.Password);
            if(result == false)
                return new DeleteAccountResponseDto { IsSuccessfulDelete = false };

            await _userManager.DeleteAsync(user);
            return new DeleteAccountResponseDto { IsSuccessfulDelete = true };
        }

        public async Task ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user is null)
                throw new InvalidCredentialsException();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };
            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback);
            await _emailSender.SendEmailAsync(message);

            await Task.CompletedTask;
        }

        public async Task<string> GetUser(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                //await _userManager.AccessFailedAsync(user);
                //if (await _userManager.IsLockedOutAsync(user))
                //{
                //    throw new AccountLockedOutException();
                //}
                throw new InvalidCredentialsException();
            }


            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //    throw new InvalidCredentialsException();

            //await _userManager.ResetAccessFailedCountAsync(user);

            Random random = new Random();
            var bytes = new Byte[30];
            random.NextBytes(bytes);

            var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
            var hexStr = String.Concat(hexArray);

            //var response = _mapper.Map<LoginResponseDto>(user);

            var hexId = new Identificator { HexIdentificator= hexStr, UserId = user.Id };
            await _hexIdRepository.AddHex(hexId);

            return hexStr;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if(user is null)
                throw new InvalidCredentialsException();

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            return result;
        }

        

        public async Task<string> GoogleAuthentication(ExternalAuthDto externalAuthDto)
        {
            var payload = await _tokenService.VerifyGoogleToken(externalAuthDto);
            //if (payload == null)
            //return BadRequest("Invalid External Authentication.");
            var info = new UserLoginInfo(externalAuthDto.Provider, payload.Subject, externalAuthDto.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new User (payload.Email);
                    await _userManager.CreateAsync(user);
                    //prepare and send an email for the email confirmation
                    
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                throw new BadExternalAuthenticationException();
            //check for the Locked out account

            var response = _mapper.Map<LoginResponseDto>(user);
            var token =  _tokenService.GenerateJWT(response, _configuration);
            return token;
        }
    }
}
