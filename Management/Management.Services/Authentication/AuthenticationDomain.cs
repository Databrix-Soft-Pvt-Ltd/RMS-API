using Management.Model.RMSEntity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.DBModels;
using TwoWayCommunication.Model.Enums;
using TwoWayCommunication.Model.RequestModel;
using TwoWayCommunication.Model.ResponseModel;

namespace TwoWayCommunication.Domain.Authentication
{
    public class AuthenticationDomain : IAuthenticationDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private IConfiguration _config;
        public AuthenticationDomain(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _config = config;
        } 
        public async Task<AuthenticationResponseModel> Login(AuthenticationRequestModel authenticationRequestModel)
        {
            AuthenticationResponseModel authenticationResponseModel = new AuthenticationResponseModel();
          //  var user = await _unitOfWork.UserRepository.FirstOrDefault(x => x.IsActive == true && x.EmailId.ToLower() == authenticationRequestModel.EmailId.ToLower() && x.Password.ToLower() == authenticationRequestModel.Password.ToLower());
            var user = await _unitOfWork.UserRepository.FirstOrDefault(x => x.IsActive == true && x.EmailId.ToLower() == authenticationRequestModel.EmailId.ToLower() &&   x.Password.ToLower() == authenticationRequestModel.Password.ToLower());

            if (user != null)
            {
               // authenticationResponseModel.RoleID = user.RoleID;
                authenticationResponseModel.UserId = user.UserId;
                authenticationResponseModel.UserName = user.UserName;
                authenticationResponseModel.IsAdmin = (int)user.UserType;   //await IsAdmin(user.UserId); 
                authenticationResponseModel.StatusCode = 200;
                authenticationResponseModel.StatusMessage = "Login Successfully...";
                authenticationResponseModel.Token = GenerateJSONWebToken(user); 
            }
            return authenticationResponseModel;
        }

        public async Task<HashSet<string>> LoginValidation(AuthenticationRequestModel authenticationRequestModel)
        {
            var userExists = await _unitOfWork.UserRepository.Any(x => x.IsActive == true && x.Password.ToLower() == authenticationRequestModel.Password.ToLower() && x.Password.ToLower() == authenticationRequestModel.Password.ToLower());
            if (!userExists)
            {
                validationMessage.Add("User not found");
            }
            return validationMessage;
        }

        //private async Task<bool> IsAdmin(long userId)
        //{
        //    return await _unitOfWork.UserRoleRepository.Any(x => x.UserId == userId && x.RoleId == (int)RoleEnum.Admin);
        //}

        private string GenerateJSONWebToken(UserMaster userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailId),
                        new Claim("UserId", userInfo.UserId.ToString())
                    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public interface IAuthenticationDomain
    {
        Task<HashSet<string>> LoginValidation(AuthenticationRequestModel authenticationRequestModel);
        Task<AuthenticationResponseModel> Login(AuthenticationRequestModel authenticationRequestModel);
    }
}
