using ParkingWeb.Models.Login;
using ParkingWeb.services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;

        public LoginService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Task LoginAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}
