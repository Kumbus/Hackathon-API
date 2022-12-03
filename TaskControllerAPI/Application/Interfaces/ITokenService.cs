using Application.Dtos.UserDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJWT(LoginResponseDto user, IConfiguration config);
    }
}
