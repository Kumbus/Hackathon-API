using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {

        }
    }

    public sealed class InvalidCredentialsException : BadRequestException
    {
        public InvalidCredentialsException() : base("Invalid credentials")
        {
        }
    }

    public sealed class UsernameTakenException : BadRequestException
    {
        public UsernameTakenException(string Username) : base($"Username {Username} is already taken.")
        {
        }
    }

}
