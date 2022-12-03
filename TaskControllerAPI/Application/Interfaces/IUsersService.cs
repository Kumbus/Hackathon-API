using Application.Dtos.UserDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
        public Task<IdentityResult> AddUser(UserRegistrationDto registrationDto);

        //public Task<IdentityResult> ConfirmEmail(string email, string token);

        public Task ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        public Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto);
        public Task<string> GetUser(UserLoginDto loginDto);
        public Task<DeleteAccountResponseDto> DeleteAccount(DeleteAccountDto deleteAccountDto);
        public Task<string> GoogleAuthentication(ExternalAuthDto externalAuthDto);


        //public Task<
    }
}
