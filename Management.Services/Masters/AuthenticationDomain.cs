using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwoWayCommunication.Core.UnitOfWork; 
using TwoWayCommunication.Model.Enums;
using TwoWayCommunication.Model.RequestModel;
using TwoWayCommunication.Model.ResponseModel;

namespace Management.Services.Masters
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
            var user = await _unitOfWork.UserRepository.FirstOrDefault(x => x.IsActive == true && x.EmailId == authenticationRequestModel.EmailId && x.Password == Encryption.Encrypt(authenticationRequestModel.Password));

            bool isPasswordExpired = Encryption.IsPasswordExpired(user.EmailId);
            string UserPansss = Encryption.Decrypt(user.Password);
            if (isPasswordExpired)
            {
                authenticationResponseModel.StatusCode = 200;
                authenticationResponseModel.StatusMessage = "Password has been expired....";
                return authenticationResponseModel;

            }
            else
            {
                if (user != null)
                {
                    // authenticationResponseModel.RoleID = user.RoleID;
                    authenticationResponseModel.UserId = user.UserId;
                    authenticationResponseModel.RoleID = Convert.ToInt32(user.RoleId);
                    authenticationResponseModel.UserName = user.UserName;
                    authenticationResponseModel.UserType = (int)user.UserType;
                    authenticationResponseModel.IsActive = (bool)user.IsActive;   //await IsAdmin(user.UserId); 
                    authenticationResponseModel.StatusCode = 200;
                    authenticationResponseModel.StatusMessage = "Login has been Successfully...!!!";
                    authenticationResponseModel.Token = GenerateJSONWebToken(user);
                }
            }
            return authenticationResponseModel;
        }
        public async Task<HashSet<string>> LoginValidation(AuthenticationRequestModel authenticationRequestModel)
        {
           //string UserPansss = Encryption.Decrypt("g8YUaA5BjTTlbClX46TXXGmlATWWDiKJJgF8r2fnfBs=");


            var userExists = await _unitOfWork.UserRepository.Any(x => x.IsActive == true && x.EmailId.ToLower() == authenticationRequestModel.EmailId.ToLower() && x.Password.ToLower() == Encryption.Encrypt(authenticationRequestModel.Password));
            if (!userExists)
            {
                validationMessage.Add("Invalid EmailID & Password....!!");
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
              claims.ToArray(),
              expires: DateTime.Now.AddHours(30),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
    public interface IAuthenticationDomain
    {
        Task<HashSet<string>> LoginValidation(AuthenticationRequestModel authenticationRequestModel);
        Task<AuthenticationResponseModel> Login(AuthenticationRequestModel authenticationRequestModel);
    }
}
