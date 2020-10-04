using Infrastructure.Helpers;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity;
using ParkingWeb.Exceptions;
using ParkingWeb.Models.Register;
using ParkingWeb.services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ParkingWeb.services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly ICompanyRepository _companyRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RegisterService(UserManager<ApplicationUser> userManager, 
            IUserCompanyRepository userCompanyRepository, 
            ICompanyRepository companyRepository,
            IApplicationUserRepository applicationUserRepository,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userCompanyRepository = userCompanyRepository;
            _companyRepository = companyRepository;
            _applicationUserRepository = applicationUserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(RegisterModel model)
        {
            var cnpjHelper = new CNPJHelper();

            await Validate(model.CNPJ, cnpjHelper);

            model.CNPJ = cnpjHelper.Clean(model.CNPJ);

            if (await _applicationUserRepository.AnyAsync(x => x.Email.Equals(model.Email)))
            {
                throw new CustomExceptions("Este email já está cadatrado");
            }

            using (_unitOfWork)
            {
                var id = Guid.NewGuid();
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    CreatedDate = DateTimeOffset.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    AccessFailedCount = 0,
                    ID = id,
                    Id = id.ToString(),
                    NormalizedUserName = model.Email.ToUpper(),
                    NormalizedEmail = model.Email.ToUpper(),
                };
                
                var hasher = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);
                user.PasswordHash = hasher;

                await _applicationUserRepository.AddAsync(user);

                var company = await _companyRepository.FirstOrDefaultAsync(x => x.CNPJ.Equals(model.CNPJ));

                await _userCompanyRepository.AddAsync(new UserCompany { CompanyID = company.ID, UserID = id.ToString()  });
                await _userManager.AddToRoleAsync(user, "USER");

                await _unitOfWork.CommitAsync();
            }
        }

        private async Task Validate(string CNPJ, CNPJHelper helper)
        {
            if(!helper.IsCNPJ(CNPJ))
            {
                throw new CustomExceptions("CNPJ incorreto");
            } else if (!await _companyRepository.AnyAsync(x => x.CNPJ.Equals(helper.Clean(CNPJ))))
            {
                throw new CustomExceptions("CNPJ não encontrado na base de dados");
            }
        }
    }
}
