using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.ResponseModel;

namespace Management.API
{
	public  class ValidateTokenExtension : IValidateTokenExtension
    {
        //static IAdminRepository<AdminEntity> _adminRepositary;
        private IConfiguration _config;
        private IUnitOfWork _unitOfWork;
        private IExceptionHandling _exceptionHandling;
        public ValidateTokenExtension(IConfiguration config, IExceptionHandling exceptionHandling, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _exceptionHandling = exceptionHandling;
        }
        static byte[] Base64UrlDecode(string input)
        {
            string base64 = input.Replace('-', '+').Replace('_', '/');
            while (base64.Length % 4 != 0)
            {
                base64 += '=';
            }
            return Convert.FromBase64String(base64);
        }
        public TokenValidation ValidateToken(string token)
		{
            TokenValidation validation = new TokenValidation();

            var tokenHandler = new JwtSecurityTokenHandler(); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            try
            {
                // Validate token format
                if (!tokenHandler.CanReadToken(token))
                {
                    validation.ValiedTokenRespose = "Invalid token format";
                    return validation;
                }
                // Decode token
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    validation.ValiedTokenRespose = "Invalid token";
                    return validation;
                }
                else
                {
                     //Validate token signature 

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    jwtToken = (JwtSecurityToken)validatedToken;
                    validation.UserID = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Azp)?.Value;
                    validation.RoleID = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)?.Value;
                    validation.EmailID = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;

                    validation.ValiedTokenRespose = "Valid token";

                }

            }
            catch (SecurityTokenValidationException)
            {
                validation.ValiedTokenRespose = "Token has expired";
            }
            catch (SecurityTokenException)
            {
                validation.ValiedTokenRespose = "Token is invalid";
            }
            catch (Exception ex)
            {
                validation.ValiedTokenRespose = "Invalid token";
                _exceptionHandling.LogError(ex);
                return validation;
            }
            return validation;
        }
         
    }
   
    public interface IValidateTokenExtension
    {
        TokenValidation ValidateToken(string token); 
    }
}
