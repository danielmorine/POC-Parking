using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity;
using ParkingWeb.Enums;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Login;
using ParkingWeb.services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingWeb.services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginService(ITokenService tokenService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

            if (!result.Succeeded)
            {
                throw new CustomExceptions("Email ou senha inválida");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            return _tokenService.GetToken(user);
        }

        public async Task CreateUserAsync(LoginModel model)
        {
            var applicationUser = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if (!result.Succeeded)
            {
                throw new CustomExceptions("Não foi possível criar o usuário");
            }
        }
    }
}
