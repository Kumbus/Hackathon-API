using Application.Dtos.UserDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

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
        private readonly IConfiguration _configuration;
        private readonly IHexIdRepository _hexIdRepository;

        public UsersService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IHexIdRepository hexIdRepository )
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _hexIdRepository= hexIdRepository;
        }

        public async Task<IdentityResult> AddUser(UserRegistrationDto registrationDto)
        {
            if (registrationDto == null)
                throw new InvalidCredentialsException();

            var user = _mapper.Map<User>(registrationDto);

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

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

            await Task.CompletedTask;
        }

        public async Task<string> GetUser(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new InvalidCredentialsException();
            }

            Random random = new Random();
            var bytes = new Byte[30];
            random.NextBytes(bytes);

            var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
            var hexStr = String.Concat(hexArray);

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
    }
}
