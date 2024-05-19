using Management.Model.RMSEntity;
using System.Security.Claims;
using Management.Model.ResponseModel;
using System.Reflection.Metadata;
using TwoWayCommunication.Core.UnitOfWork; 
using Management.Model.RequestModel;
using Microsoft.EntityFrameworkCore;
using static Management.Services.Masters.UserDomain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Management.Services.Masters
{
    public class UserDomain : IUserDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private readonly RMS_2024Context _dbContext;


        private HashSet<string> validationMessage = new HashSet<string>();
        public UserDomain(IUnitOfWork unitOfWork, RMS_2024Context dbContext)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllUserResponse>> GetAll()
        {
            var users = await _dbContext.UserMasters.FromSqlRaw("EXEC SP_GetAllUsers").ToListAsync();

            if (users != null && users.Any())
            {
                return users.Select(u => new GetAllUserResponse
                {
                    UserId = u.UserId,
                    RoleId = u.RoleId,
                    UserName = u.UserName,
                    EmailId = u.EmailId, 
                    MobileNo = u.MobileNo,
                    isActive = u.IsActive,
                });
            }
            else
            {
                // Handle case where no users are found
                return Enumerable.Empty<GetAllUserResponse>();
            }


        }
        public async Task<IEnumerable<GetAllUserResponse>> GetAllIsActiveUser()
        {
            var users = await _dbContext.UserMasters.FromSqlRaw("EXEC SP_GetAllUsersIsActive").ToListAsync();

            if (users != null && users.Any())
            {
                return users.Select(u => new GetAllUserResponse
                {
                    UserId = u.UserId,
                    RoleId = u.RoleId,
                    UserName = u.UserName,
                    EmailId = u.EmailId,
                    MobileNo = u.MobileNo
                });
            }
            else
            {
                // Handle case where no users are found
                return Enumerable.Empty<GetAllUserResponse>();
            }


        }

        public async Task<GetAllUserResponse> GetUserById(GetUserByIdRequest request)
        {
            var user = _unitOfWork.UserRepository.AsQueryable()
                .Where(r => r.UserId == request.UserId)
                .Select(u => new GetAllUserResponse
                {
                    UserId = u.UserId,
                    RoleId = u.RoleId,
                    UserName = u.UserName,
                    EmailId = u.EmailId,
                    MobileNo = u.MobileNo,
                })
                .FirstOrDefault();

            return user;
        }





        /*  public async Task<UserMaster> Get(int id)
          {
              return await _unitOfWork.UserRepository.FirstOrDefault(x => x.UserId == id);
          }
        */
        public async Task<HashSet<string>> AddValidation(AddUserRequestModel request, string Chk)
        {
            validationMessage = new HashSet<string>();

            if (Chk == "User")
            {
                bool isUserExists = await _unitOfWork.UserRepository.Any(x => x.EmailId.ToLower() == request.EmailId.ToLower());
                if (!isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            else
            {
                bool CheckRoleName = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (CheckRoleName)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            return validationMessage;
        }
        public async Task<string> Add(AddUserRequestModel request)
        {
          //  var usermaster = new UserMaster();
            try
            {
                string userName = request.UserName;
                string email = request.EmailId;
                string password = Encryption.Encrypt(request.Password);
                string mobileNo = request.MobileNo;
                long roleId = request.RoleId;
                int? userType = request.UserType;
                bool isActive = request.IsActive;
                string ipAddress = request.Ipaddress;
                int userId = request.ByUserId;

                var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                responseMessageParam.Direction = ParameterDirection.Output;

                var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_AddNewUser] @RoleId,@UserName,@Email,@Password, @MobileNo, @UserType, @IsActive, @Ipaddress,@UserId,@Response_Message OUTPUT",
                    new SqlParameter("@RoleId", roleId),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@MobileNo", mobileNo),
                    new SqlParameter("@UserType", userType),
                    new SqlParameter("@IsActive", isActive),
                    new SqlParameter("@Ipaddress", ipAddress),
                    new SqlParameter("@UserId", userId),responseMessageParam);

                string responseMessage = responseMessageParam.Value.ToString();
                Console.WriteLine("Response Message: " + responseMessage);
                return responseMessage;


            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occurred while processing your request.";
            }

        }
        public async Task<HashSet<string>> UpdateValidation(UpdateUserRequestModel request, string Chk)
        {

            validationMessage = new HashSet<string>();

            if (Chk == "User")
            {
                bool isUserExists = await _unitOfWork.UserRepository.Any(x => x.UserId == request.UserId);
                if (isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Chk == "email")
            {
                bool isUserExists = await _unitOfWork.UserRepository.Any(x => x.EmailId.ToLower() == request.EmailId.ToLower());
                if (isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Chk == "Role")
            {
                bool CheckRoleName = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (CheckRoleName)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            return validationMessage;

        }
        public async Task<string> Update(UpdateUserRequestModel request)
        {

            long id = request.UserId;
            string userName = request.UserName;
            string email = request.EmailId;
            string password = Encryption.Encrypt(request.Password);
            string mobileNo = request.MobileNo;
            long role = request.RoleId;
            int userType = request.UserType;
            bool isActive = request.IsActive;
            int userId = request.ByUserId;

            // Execute the stored procedure
            var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
            responseMessageParam.Direction = ParameterDirection.Output;

            var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_UpdateUser] @Id, @UserName, @Email, @Password, @MobileNo, @Role, @UserType, @IsActive, @UserId, @Response_Message OUTPUT",
                new SqlParameter("@Id", id),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password),
                new SqlParameter("@MobileNo", mobileNo),
                new SqlParameter("@Role", role),
                new SqlParameter("@UserType", userType),
                new SqlParameter("@IsActive", isActive),
                new SqlParameter("@UserId", userId),
                responseMessageParam);

            // Retrieve the output parameter value
            string responseMessage = responseMessageParam.Value.ToString();
            Console.WriteLine("Response Message: " + responseMessage);
            return responseMessage;
        }

        //public async Task<HashSet<string>> DeleteValidation(GetUserByIdRequest request)
        //{
        //    bool IsRecord = await _unitOfWork.UserRepository.Any(x => x.UserId == request.UserId);
        //    if (IsRecord)
        //        validationMessage.Add("1");
        //    return validationMessage;
        //}
        public async Task<string> Delete(GetUserByIdRequest id)
        {
            try
            {
                var messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC SP_DeleteUser @UserId, @Message OUTPUT",
                    new SqlParameter("@UserId", id.UserId), messageParameter);

                string message = messageParameter.Value.ToString();

                return message;
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
                return "An error occurred.";
            }
        } 
        public async Task UpdateUserIsActive(User_IsAction branchMapList)
        {

            var GlobalIsActive = _unitOfWork.UserRepository.AsQueryable().Where(x => x.UserId == branchMapList.UserID).FirstOrDefault();

            if (GlobalIsActive != null)
            {
                GlobalIsActive.IsActive = branchMapList.IsActive;
                _unitOfWork.UserRepository.Update(GlobalIsActive);
                await _unitOfWork.Commit();
            }
        }

        public interface IUserDomain
        {
            Task<IEnumerable<GetAllUserResponse>> GetAllIsActiveUser();
            Task<IEnumerable<GetAllUserResponse>> GetAll();
            Task<GetAllUserResponse> GetUserById(GetUserByIdRequest request);
            Task<string> Add(AddUserRequestModel request);
            Task<string> Update(UpdateUserRequestModel request);
            Task<HashSet<string>> AddValidation(AddUserRequestModel request, string Chk);
            Task<HashSet<string>> UpdateValidation(UpdateUserRequestModel request, string Chk);
          //  Task<HashSet<string>> DeleteValidation(GetUserByIdRequest request);
            Task<string> Delete(GetUserByIdRequest id);
            Task UpdateUserIsActive(User_IsAction branchMapList);
            // Task Delete(int id);
        }

    }
}
